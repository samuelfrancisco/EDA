
$.fn.extend({
    eguruCarousel: function(configuracao) {

        //configuraveis
        this.botaoProximo = configuracao && configuracao.hasOwnProperty('avancar') ? configuracao.avancar : true;
        this.botaoAnterior = configuracao && configuracao.hasOwnProperty('voltar') ? configuracao.voltar : true;
        this.auto = configuracao && configuracao.hasOwnProperty('auto') ? configuracao.auto : true;
        this.velocidade = configuracao && configuracao.hasOwnProperty('velocidade') ? configuracao.velocidade : 3000;
        this.easing = configuracao && configuracao.hasOwnProperty('easing') ? configuracao.easing : 'linear';
        this.quantItens = configuracao && configuracao.hasOwnProperty('quantItens') ? configuracao.quantItens : 1;
        this.quantItensOriginal = this.quantItens;
        this.velocidadeTrasicao = configuracao && configuracao.hasOwnProperty('velocidadeTrasicao') ? configuracao.velocidadeTrasicao : "0.5";
        this.loops = configuracao && configuracao.hasOwnProperty('loops') ? configuracao.loops : true;
        this.espacamento = configuracao && configuracao.hasOwnProperty('espacamento') ? configuracao.espacamento : 0;
        this.larguraMinima = configuracao && configuracao.hasOwnProperty('larguraMinima') ? configuracao.larguraMinima : 220;

        //constantes
        this.classeDoCarousel = "carousel";
        this.classeDoWrapperDosItens = "items-wrapper";
        this.classeDosItensDoCarousel = "item-carousel";

        //variaveis
        this.interval = null;
        this.marginLeftStatic = 0;

        var carousel = this;
        carousel.$element = $(carousel[0]);

        carousel.inicializar = function () {
            carousel.width = carousel.$element.width();
            if ((carousel.quantItens * carousel.larguraMinima) + ((carousel.quantItens - 1) * carousel.espacamento) > carousel.width) {
                carousel.quantItens = parseInt(carousel.width / carousel.larguraMinima, 10);
            }

            carousel.configuraCarousel();

            $(window).resize(carousel.resize);

            carousel.$element.bind(carousel.dispose)

            if (carousel.auto) {
                carousel.interval = setInterval(carousel.moverCarouselEsquerda, carousel.velocidade);
                $(carousel.$element).hover(function() {
                    clearInterval(carousel.interval)
                }, function () {
                    carousel.interval = setInterval(carousel.moverCarouselEsquerda, carousel.velocidade);
                })
            }

            $(carousel.$element).on('click', '.next',carousel.moverCarouselEsquerda);
            $(carousel.$element).on('click', '.prev',carousel.moverCarouselDireita);
        }

        carousel.moverCarouselDireita = function (nav) {
            carousel.moverCarousel(nav, 'direita');
        };

        carousel.moverCarouselEsquerda = function (nav) {
            carousel.moverCarousel(nav, 'esquerda');
        };

        carousel.dispose = function () {
            clearInterval(carousel.interval);

            $(window).off("resize", carousel.resize);
        };  

        carousel.modificaHtml = function () {
            //adiciona classe padrão ao carousel
            carousel.$element.addClass(carousel.classeDoCarousel);

            //adiciona classe padrão aos itens do carousel
            carousel.$element.children().addClass(carousel.classeDosItensDoCarousel);

            //cria wrapper
            var div = document.createElement("div");
            div.innerHTML = carousel.$element.html();
            //copia itens no final para carrousel ficar infinito
            var carouselItens = carousel.$element.find("." + carousel.classeDosItensDoCarousel);

            if (carousel.quantItens > carouselItens.length) {
                carousel.botaoAnterior = false;
                carousel.botaoProximo = false;
            }
            else {
                if (carousel.loops) {
                    for (var i = 0; i < carousel.quantItens; i++) {
                        div.appendChild(carouselItens[i]);
                    }
                }
            }
            div.className = carousel.classeDoWrapperDosItens;
            carousel.$element.html(div);

            //seta transition do wrapper
            carousel.$element.find('.' + carousel.classeDoWrapperDosItens).css('transition', carousel.velocidadeTrasicao + 's all ' + carousel.easing);
            
            //ajusta tamanho dos itens
            carousel.setItensSize();

            //cria botões
            if (carousel.botaoAnterior) {
                var button = document.createElement("span");
                $(button).addClass("next");
                $(button).addClass(carousel.botaoAnterior);
                

                var icon = document.createElement('i');
                icon.className = "fa fa-chevron-right"

                button.appendChild(icon);

                carousel.$element[0].appendChild(button)
            }
            if (carousel.botaoProximo) {
                var button = document.createElement("span");
                $(button).addClass("prev");
                $(button).addClass(carousel.botaoAnterior);

                var icon = document.createElement('i');
                icon.className = "fa fa-chevron-left"

                button.appendChild(icon);

                carousel.$element[0].appendChild(button)
            }
        };

        carousel.setItensSize = function () {
            div = carousel.$element.find('.' + carousel.classeDoWrapperDosItens);
            var carouselWrapperBoundingRect = carousel.$element[0].getBoundingClientRect()
            var divWidth = (carouselWrapperBoundingRect.width ? carouselWrapperBoundingRect.width : carouselWrapperBoundingRect.right - carouselWrapperBoundingRect.left);
            var newWidth = (divWidth - ((carousel.quantItens - 1) * carousel.espacamento)) / carousel.quantItens + 'px'
            div.children().css('width', newWidth);
            div.children().css('margin-right', carousel.espacamento);
        }

        carousel.configuraCarousel = function () {
            carousel.modificaHtml();
            var parent = carousel.$element;
            var div = $(parent).find("." + carousel.classeDoWrapperDosItens);
            var lis = $(parent).find("." + carousel.classeDosItensDoCarousel);
            var qtdLi = lis.length;

            var liWidth = $(lis[0]).width();
            var parentWidth = $(parent[0]).width();
            
            $(div).css('margin-left', '0px');
            carousel.marginLeftStatic = 0;
        };

        carousel.resize = function () {
            var parent = carousel.$element;

            carousel.width = parent.width();
            if ((carousel.quantItensOriginal * carousel.larguraMinima) + ((carousel.quantItensOriginal - 1) * carousel.espacamento) > carousel.width) {
                carousel.quantItens= parseInt(carousel.width / carousel.larguraMinima, 10);
            }

            var parentBoundingRect = parent[0].getBoundingClientRect();
            var parentWidth = parentBoundingRect.width ? parentBoundingRect.width : parentBoundingRect.right - parentBoundingRect.left;

            var div = $(parent).find("." + carousel.classeDoWrapperDosItens);
            var lis = $(parent).find("." + carousel.classeDosItensDoCarousel);
            var qtdLi = lis.length;
            var liBoundingRect = lis[0].getBoundingClientRect();
            var liWidth = (liBoundingRect.width ? liBoundingRect.width : liBoundingRect.right - liBoundingRect.left) + carousel.espacamento;

            if (parentWidth / carousel.quantItens != liWidth - (carousel.espacamento / qtdLi)) {
                carousel.setItensSize();
                liBoundingRect = lis[0].getBoundingClientRect();
                liWidth = (liBoundingRect.width ? liBoundingRect.width : liBoundingRect.right - liBoundingRect.left) + carousel.espacamento;
            }

            carousel.setItensSize();
        }

        carousel.moverCarousel = function (nav, sentido) {

            var parent = carousel.$element;

            var parentBoundingRect = parent[0].getBoundingClientRect();
            var parentWidth = parentBoundingRect.width ? parentBoundingRect.width : parentBoundingRect.right - parentBoundingRect.left;

            var div = $(parent).find("." + carousel.classeDoWrapperDosItens);
            var lis = $(parent).find("." + carousel.classeDosItensDoCarousel);
            var qtdLi = lis.length;
            var liBoundingRect = lis[0].getBoundingClientRect();
            var liWidth = (liBoundingRect.width ? liBoundingRect.width : liBoundingRect.right - liBoundingRect.left) + carousel.espacamento;

            if (parentWidth / carousel.quantItens != liWidth - (carousel.espacamento / qtdLi)) {
                carousel.setItensSize();
                liBoundingRect = lis[0].getBoundingClientRect();
                liWidth = (liBoundingRect.width ? liBoundingRect.width : liBoundingRect.right - liBoundingRect.left) + carousel.espacamento;
            }

            var marginLeft = carousel.marginLeftStatic;

            if (sentido == 'direita') {
                if (marginLeft <= -liWidth) {
                    $(div[0]).css('margin-left', marginLeft + liWidth);
                    carousel.marginLeftStatic += liWidth;
                } else {
                    //posicao inicial
                    carousel.$element.find('.' + carousel.classeDoWrapperDosItens).css('transition', 'none');
                    carousel.marginLeftStatic = -(qtdLi - carousel.quantItens) * liWidth;
                    $(div[0]).css('margin-left', carousel.marginLeftStatic.toString() + 'px');

                    //movimento
                    setTimeout(function () {
                        carousel.$element.find('.' + carousel.classeDoWrapperDosItens).css('transition', carousel.velocidadeTrasicao + 's all ' + carousel.easing);
                        $(div[0]).css('margin-left', carousel.marginLeftStatic + liWidth);
                        carousel.marginLeftStatic += liWidth;
                    }, 50);
                }
            }
            else {
                if ((liWidth * qtdLi) - carousel.espacamento > parentWidth - marginLeft) {
                    $(div[0]).css('margin-left', marginLeft - liWidth);
                    carousel.marginLeftStatic -= liWidth;
                } else {
                    //posicao inicial
                    carousel.$element.find('.' + carousel.classeDoWrapperDosItens).css('transition', 'none');
                    carousel.marginLeftStatic = 0;
                    $(div[0]).css('margin-left', '0px');

                    //movimento
                    setTimeout(function () {
                        carousel.$element.find('.' + carousel.classeDoWrapperDosItens).css('transition', carousel.velocidadeTrasicao + 's all ' + carousel.easing);
                        $(div[0]).css('margin-left', -liWidth);
                        carousel.marginLeftStatic -= liWidth;
                    }, 50);
                }
            }
        };

        carousel.inicializar();
    }
});

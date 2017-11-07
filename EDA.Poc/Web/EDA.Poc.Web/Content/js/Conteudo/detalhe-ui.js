(function ($, container) {

    var $container = $(container),
        $ui,
        IdDoConteudo = $("[data-id-conteudo]").data("id-conteudo");

    var ui = {
        consultas: {
            conteudoDetalhes: "conteudoDetalhes",
        },
        comandos: {
            comprar: "comprar",
            avaliar: "avaliar",
        },

        inicializar: function () {
            $ui = $(this);

            configurarUi();
            configurarHandlers();
        }
    };

    //queries

    function configurarUi() {
        $container.find(".section-competencias .loading").remove();
                
        calculaTamanhoConsolidadoAvaliacoes();

        formatarCampos();
    }

    function configurarHandlers() {

        //UI

        $container.on('click', '[role=modal] .close-modal', function (e) {
            $(this).parents('[role=modal]').removeClass('active');
            e.preventDefault();
            e.stopPropagation();
        });

        $container.on('click', '.comprar', function () {
            $('[role=modal].modal-compra').addClass('active');
            $('html, body').animate({
                scrollTop: 0
            }, 400);
        });

        $container.on('click', '.toggle-preview', function () {
            //Todo
            $('[role=modal].modal-preview').addClass('active');
        });

        $container.on('keyup', '#qtd-licencas', function () {
            var valor = parseInt($(this).val(), 10);

            if (isNaN(valor)) {
                var text = "X " + $($('[data-preco]')[0]).text() + " = 0,00/mês";
                $(".preco").text(text);
            } else {
                $('[data-range]').each(function (index, element) {
                    var range = $(element).data('range').toString().split('|');
                    if (valor >= range[0] && (!range[1] || valor <= range[1])) {
                        var $dataPreco = $(element).find('[data-preco]');
                        var preco = $dataPreco.data('preco').replace(',', '.');
                        var precoLabel = $dataPreco.text();
                        var text = "X " + precoLabel + " = " + (valor * preco).toFixed(2).replace('.', ',') + "/mês";
                        $(".preco").text(text);
                    }
                });
            }
        });

        $container.on({
            mouseenter: function () {
                var dataAvaliacao = $(this).data("avaliacao");
                $('[data-toggle-avaliacao]').removeClass('active');
                $("[data-avaliacao]").each(function (index, element) {
                    $(element).removeClass('active');
                    $(element).removeClass('hovered');
                    if ($(element).data('avaliacao') <= dataAvaliacao) {
                        $(element).addClass('hovered');
                    }
                });
                $('[data-toggle-avaliacao=' + dataAvaliacao + ']').addClass('active');
            },
            mouseleave: function () {
                var dataAvaliacao = $(this).parents('.avalie').data("avaliacao");
                $('[data-toggle-avaliacao]').removeClass('active');
                $("[data-avaliacao]").each(function (index, element) {
                    $(element).removeClass('hovered');
                    if ($(element).data('avaliacao') <= dataAvaliacao) {
                        $(element).addClass('active');
                    }
                });
                var selectedAvaliacao = $(this).parents('.avalie').data('avaliacao');
                $('[data-toggle-avaliacao=' + selectedAvaliacao + ']').addClass('active');
            }
        }, ".estrela");

        $container.on("click", ".estrela", function () {
            var dataAvaliacao = $(this).data("avaliacao");
            $(this).parents('.avalie').data('avaliacao', dataAvaliacao);
            $(this).parents('form').find('[name=Avaliacao]').val(dataAvaliacao);
            $("[data-avaliacao]").each(function (index, element) {
                $(element).removeClass('active');
                if ($(element).data('avaliacao') <= dataAvaliacao) {
                    $(element).addClass('active');
                }
            });
            $('[data-toggle-avaliacao=' + dataAvaliacao + ']').addClass('active');
        });

        //Comando

        $container.on('click', '.modal-compra [type=submit]', function (evt) {
            var form = $(this).parents('form')
            form.validate();

            if (form.valid()){
                var formSerialized = form.serialize();

                $ui.trigger(ui.comandos.comprar, {
                    callback: function (retorno) {
                        if (retorno.Sucesso) {
                            $(form).parents('[role=modal]').removeClass('active');
                            mostraAlerta("Parabéns","Ordem de compra registrado com sucesso, e colocado em processamento, em cerca de uma hora estará disponível para acesso no LMS ");
                        } else {
                            form.valid();
                            mostraAlerta("Atenção","Erro ao registrar a ordem de compra");
                        }
                    },
                    form: formSerialized
                });
            }
            

            evt.preventDefault();
            evt.stopPropagation();
        });

        $container.on('click', '.minha-avaliacao [type=submit]', function (evt) {
            var form = $(this).parents('form')
            form.validate();

            if (form.valid()) {
                var formSerialized = form.serialize();

                $ui.trigger(ui.comandos.avaliar, {
                    callback: function (retorno) {
                        if (retorno.Sucesso) {
                            $container.find(".consolidado-resenha").html(MultipartialGetValue(retorno.Data, "ConsolidadoAvaliacoes"));
                            $container.find(".resenhas").html(MultipartialGetValue(retorno.Data, "Resenhas"));
                            $container.find(".minha-avaliacao").html(MultipartialGetValue(retorno.Data, "MinhaAvaliacao"));
                            $container.find(".detalhes").html(MultipartialGetValue(retorno.Data, "Detalhes"));

                            calculaTamanhoConsolidadoAvaliacoes(MultipartialGetValue(retorno.Data, "ConsolidadoAvaliacoes"));

                            formatarCampoEscreverAvaliacao();

                            atualizaIconesDeAvaliacoes();
                        } else {
                            form.valid();
                            alert("Erro ao submeter avaliação");
                        }
                    },
                    form: formSerialized
                });
            }

            evt.preventDefault();
            evt.stopPropagation();
        });
    }

    //helpers

    function formatarCampos() {
        $('.carousel').eguruCarousel({ quantItens: 3, auto: false, espacamento: 20 });

        $('#data-inicial').pickadate({
            editable: false,
            formatSubmit: "yyyy/mm/dd",
            hiddenName: true,
            min: Date.now()
        });

        $("#qtd-licencas").numeric();
        $("#qtd-meses").numeric();

        formatarCampoEscreverAvaliacao();

        atualizaIconesDeAvaliacoes();
    }

    function calculaTamanhoConsolidadoAvaliacoes() {
        var max = 0;
        $("[data-graph-bar]").each(function (index, element) {
            if (parseInt($(element).data('graph-bar')) > max) {
                max = parseInt($(element).data('graph-bar'));
            };
        });

        var maxWidth = $(".consolidado-resenha ul").width() - ($(".consolidado-resenha ul li span").width() + parseInt($(".consolidado-resenha ul li div").css('margin-left'), 10));

        $("[data-graph-bar]").each(function (index, element) {
            var elementWidth = (parseInt($(element).data("graph-bar"), 10) / max) * maxWidth;
            $(element).width(elementWidth);
        });
    }

    function formatarCampoEscreverAvaliacao() {
        var dataAvaliacao = $('.avalie').data('avaliacao');
        $("[data-avaliacao]").each(function (index, element) {
            $(element).removeClass('active');
            if ($(element).data('avaliacao') <= dataAvaliacao) {
                $(element).addClass('active');
            }
        });
        $('[data-toggle-avaliacao=' + dataAvaliacao + ']').addClass('active');
    }

    //exports

    window.eguru = (window.eguru || {});
    window.eguru.marketPlace = (window.eguru.marketPlace || {});
    window.eguru.marketPlace.conteudoDetalhes = {};

    window.eguru.marketPlace.conteudoDetalhes.ui = ui;
})($, window.document)

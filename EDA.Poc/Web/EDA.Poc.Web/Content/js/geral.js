$(function ($) {
    function MultipartialGetValue(views, key) {
        return views.filter(function (a) { return a.name == key })[0].html;
    }

    setTimeout(function () {
        $(".tooltip").removeClass('active');
    }, 10000);

    function mostraAlerta(title, msg, callback) {
        
        var $container = $('.alert-default');
        $container.show();

        $container.find('.title').text(title);
        $container.find('.msg').text(msg);
        $container.off();
        $container.on('click', 'button', function () {
            if(callback)
                callback();
            $container.hide();
        });
    }

    function atualizaIconesDeAvaliacoes(container) {
        $((container ? container : "") + ' [data-avaliacao]').each(function (index, element) {

            var avaliacao = $(element).data('avaliacao');

            avaliacao = avaliacao.toString().replace('.', ',').replace('-',',');
            var valores = avaliacao.split(',');

            var novaClasse = "av-{0}";

            if (valores[1] != undefined) {
                if (valores[1][0] > 7) {
                    valores[0] = parseInt(valores[0], 10);
                    valores[0]++;
                    novaClasse = novaClasse.replace("{0}", valores[0].toString());
                } else if (valores[1][0] < 3) {
                    novaClasse = novaClasse.replace("{0}", valores[0]);
                } else {
                    valores[1] = 5;
                    novaClasse = novaClasse.replace("{0}", valores[0] + "-" + valores[1]);
                }
            } else {
                novaClasse = novaClasse.replace("{0}", valores[0]);
            }

            $(element).addClass(novaClasse);
        });
    }

    $(document).on('click', 'input[data-toggle]', function () {
        var sectionWithToggle = $(this).parents('.with-toggle')[0];
        var $toggleContainer = $(sectionWithToggle).find('.toggle-container[data-toggle=' + $(this).data('toggle') + ']');
        $(sectionWithToggle).find('.toggle-container').removeClass('active');
        $(sectionWithToggle).find('.toggle-container input').val('');
        $(sectionWithToggle).find('.toggle-container input').addClass('ignore');
        $toggleContainer.addClass('active');
        $toggleContainer.find('input').removeClass('ignore');
    });

    window.mostraAlerta = mostraAlerta;
    window.MultipartialGetValue = MultipartialGetValue;
    window.atualizaIconesDeAvaliacoes = atualizaIconesDeAvaliacoes;
}(jQuery));





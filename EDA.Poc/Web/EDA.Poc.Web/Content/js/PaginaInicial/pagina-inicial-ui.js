(function ($, container) {

    var $container = $(container),
        $ui;

    var ui = {
        consultas: {
            paginaInicial: "paginaInicial",
        },

        inicializar: function () {
            $ui = $(this);

            buscarDados();
            configurarHandlers();
        }
    };

    function buscarDados() {
        buscarPaginaInicial();
    }

    function buscarPaginaInicial() {
        $ui.trigger(ui.consultas.paginaInicial, {
            callback: function (retorno) {
                $container.find(".lancamentos").html(MultipartialGetValue(retorno.Data, "Lancamentos"));
                $container.find(".destaques").html(MultipartialGetValue(retorno.Data, "DestaquesDaSemana"));
                $container.find(".recomendados-equipe").html(MultipartialGetValue(retorno.Data, "RecomendadosParaSuaEquipe"));
                $container.find(".recomendados").html(MultipartialGetValue(retorno.Data, "RecomendadosParaSuaEmpresa"));
                $container.find(".mais-populares").html(MultipartialGetValue(retorno.Data, "MaisPopulares"));

                $('.carousel').eguruCarousel();

                atualizaIconesDeAvaliacoes();
            }
        });
    }

    function configurarHandlers() {
        $container.on('click', '[data-toggle]', function () {
            var sectionWithToggle = $(this).parents('section.with-toggle')[0];
            var $toggleContainer = $(sectionWithToggle).find('.toggle-container[data-toggle=' + $(this).data('toggle') + ']');
            $(sectionWithToggle).find('.toggle-container').removeClass('active');
            $toggleContainer.addClass('active');
        });
    }

    window.eguru = (window.eguru || {});
    window.eguru.marketPlace = (window.eguru.marketPlace || {});
    window.eguru.marketPlace.paginaInicial = {};
    window.eguru.marketPlace.paginaInicial.ui = ui;

})($, window.document)

(function ($, container) {

    var $container = $(container),
        $ui;

    var ui = {
        consultas: {
            detalhesDasLicencas: "detalhesDasLicencas",
            detalhesDasLicencasFiltradas: "detalhesDasLicencasFiltradas"
        },

        inicializar: function () {
            $ui = $(this);

            buscarDados();
            configurarHandlers();
        }
    };

    function buscarDados() {
        buscarDetalhes();
    }

    function buscarDetalhes() {
        $ui.trigger(ui.consultas.detalhesDasLicencas, {
            callback: function (retorno) {
                $container.find(".lista-licencas").html(MultipartialGetValue(retorno.Data, "ListaLicencas"));
            }
        });
    }

    function configurarHandlers() {
        $('.calendario').pickadate({
            editable: false,
            formatSubmit: "yyyy/mm/dd",
            hiddenName: true
        });

        $container.on('click', '.filtros button[type=submit]', function (e) {
            var form = $(this).parents('form')
            form.validate();

            if (form.valid()) {
                var formSerialized = form.serialize();

                $ui.trigger(ui.consultas.detalhesDasLicencasFiltradas, {
                    callback: function (retorno) {
                        $container.find(".lista-licencas").html(MultipartialGetValue(retorno.Data, "ListaLicencas"));
                    },
                    form: formSerialized
                });
                e.preventDefault();
                e.stopPropagation();
            }
        });
    }

    window.eguru = (window.eguru || {});
    window.eguru.marketPlace = (window.eguru.marketPlace || {});
    window.eguru.marketPlace.meuPerfil = {};
    window.eguru.marketPlace.meuPerfil.ui = ui;

})($, window.document)

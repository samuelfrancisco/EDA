(function ($, ui) {

    var $ui = $(ui);

    $ui
        .bind(ui.consultas.paginaInicial, function (tipo, dados) {
            var url = window.eguru.marketPlace.baseUrl + "PaginaInicial/PaginaInicial";

            $.get(url)
                .done(function (retorno) {
                    if (dados.callback)
                        dados.callback(retorno);
                });
        })

    $(function() {
        ui.inicializar();
    });
    
})($, window.eguru.marketPlace.paginaInicial.ui)
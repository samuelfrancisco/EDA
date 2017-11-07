(function ($, ui) {

    var $ui = $(ui);

    $ui
        .bind(ui.consultas.detalhesDasLicencas, function (tipo, dados) {
            var url = window.eguru.marketPlace.baseUrl + "Licencas/ObterDetalhesDasLicencas";

            $.get(url)
                .done(function (retorno) {
                    if (dados.callback)
                        dados.callback(retorno);
                });
        })
        .bind(ui.consultas.detalhesDasLicencasFiltradas, function (tipo, dados) {
            var url = window.eguru.marketPlace.baseUrl + "Licencas/ObterDetalhesDasLicencasComFiltros";

            $.get(url, dados.form)
                .done(function (retorno) {
                    if (dados.callback)
                        dados.callback(retorno);
                });
        })
        
    $(function() {
        ui.inicializar();
    });
    
})($, window.eguru.marketPlace.meuPerfil.ui)
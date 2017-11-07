(function ($, ui) {

    var $ui = $(ui);

    $ui
        .bind(ui.comandos.comprar, function (tipo, dados) {
            var url = window.eguru.marketPlace.baseUrl + "Conteudo/Comprar";

            $.post(url, dados.form)
                .done(function (retorno) {
                    if (dados.callback)
                        dados.callback(retorno);
                });
        })
        .bind(ui.comandos.avaliar, function (tipo, dados) {
            var url = window.eguru.marketPlace.baseUrl + "Conteudo/Avaliar";

            $.post(url, dados.form)
                .done(function (retorno) {
                    if (dados.callback)
                        dados.callback(retorno);
                });
        });

    $(function() {
        ui.inicializar();
    });
    
})($, window.eguru.marketPlace.conteudoDetalhes.ui)
namespace EDA.Poc.Pagamentos.Domain.Enums
{
    public enum ProcessoDeCompraStatus
    {
        NaoIniciado = 1,
        AguardandoGeracaoCobrancaDaCompraParaOCliente = 2,
        AguardandoRegistroDaLicencaParaOCliente = 3,
        LicensaRegistradaParaOCliente = 4
    }
}
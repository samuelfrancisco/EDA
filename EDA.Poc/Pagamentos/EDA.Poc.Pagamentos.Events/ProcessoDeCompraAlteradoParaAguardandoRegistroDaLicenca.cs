using System;
using EDA.Poc.Infraestrutura.EventSourcing;

namespace EDA.Poc.Pagamentos.Events
{
    public class ProcessoDeCompraAlteradoParaAguardandoRegistroDaLicenca : VersionedEvent
    {
        public DateTime DataDeGeracaoDaCobrancaDaCompra { get; set; }
    }
}
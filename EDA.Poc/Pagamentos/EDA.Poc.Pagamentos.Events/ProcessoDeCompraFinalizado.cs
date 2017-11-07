using System;
using EDA.Poc.Infraestrutura.EventSourcing;

namespace EDA.Poc.Pagamentos.Events
{
    public class ProcessoDeCompraFinalizado : VersionedEvent
    {
        public DateTime DataDoRegistroDaLicenca { get; set; }
    }
}
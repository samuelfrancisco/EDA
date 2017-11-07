using EDA.Poc.Infraestrutura.EventSourcing;
using System;

namespace EDA.Poc.Pagamentos.Events
{
    public class ProcessoDeCobrancaRecorrenteIniciado : VersionedEvent
    {
        public Guid IdDaCobrancaRecorrente { get; set; }
        public Guid IdDaCobranca { get; set; }
        public Guid IdDaOrdemDeCompra { get; set; }
        public Guid IdDoUsuario { get; set; }
        public Guid IdDoCliente { get; set; }
        public Guid IdDoConteudo { get; set; }
        public DateTime DataDaCompra { get; set; }
        public decimal ValorRecorrente { get; set; }
        public DateTime DataDaPrimeiraCobranca { get; set; }
    }
}

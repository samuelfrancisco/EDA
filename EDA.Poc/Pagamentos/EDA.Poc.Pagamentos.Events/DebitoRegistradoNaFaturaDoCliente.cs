using System;
using EDA.Poc.Infraestrutura.EventSourcing;
using EDA.Poc.Infraestrutura.Utils;

namespace EDA.Poc.Pagamentos.Events
{
    public class DebitoRegistradoNaFaturaDoCliente : VersionedEvent
    {
        public Guid IdDoDebito { get; set; }
        public Guid IdDaCobranca { get; set; }
        public Guid IdDaOrdemDeCompra { get; set; }
        public Guid IdDoProcessoDeCompra { get; set; }
        public Guid IdDoUsuario { get; set; }
        public Guid IdDoCliente { get; set; }
        public Guid IdDoConteudo { get; set; }
        public DateTime DataDaCompra { get; set; }
        public decimal Valor { get; set; }
        public Mes MesDaFatura { get; set; }
    }
}
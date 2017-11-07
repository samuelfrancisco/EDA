using System;
using EDA.Poc.Infraestrutura.EventSourcing;
using EDA.Poc.Pagamentos.Domain.Enums;

namespace EDA.Poc.Pagamentos.Events
{
    public class ProcessoDeCompraIniciado : VersionedEvent
    {
        public Guid IdDaOrdemDeCompra { get; set; }
        public Guid IdDoUsuario { get; set; }
        public Guid IdDoCliente { get; set; }
        public Guid IdDoConteudo { get; set; }
        public TipoDeLicenciamento TipoDeLicenciamento { get; set; }
        public int? QuantidadeDeUsuarios { get; set; }
        public DateTime DisponibilizarAPartirDe { get; set; }
        public TipoDeValidade TipoDeValidade { get; set; }
        public int? QuantidadeDeMeses { get; set; }
        public FormaDePagamento FormaDePagamento { get; set; }
        public decimal PrecoDoConteudo { get; set; }
        public decimal ValorMensalDaCompra { get; set; }
        public ProcessoDeCompraIniciadoPromocao Promocao { get; set; }
        public DateTime DataDeCriacaoDaOrdemDeCompra { get; set; }
        public DateTime DataDaCompra { get; set; }
    }
}
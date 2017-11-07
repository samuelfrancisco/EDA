using System;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Pagamentos.Domain.Enums;

namespace EDA.Poc.Pagamentos.Domain
{
    public class OrdemDeCompraMemento : IMemento<OrdemDeCompra>, IMemento<IEventSourced>
    {
        public Guid Id { get; set; }
        public long Version { get; set; }        
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
        public OrdemDeCompraPromocao Promocao { get; set; }
        public DateTime CriadaEm { get; set; }
        public DateTime? ConcluidaEm { get; set; }
        public DateTime DataDaCompra { get; set; }
        public OrdemDeCompraStatus Status { get; set; }        
    }
}
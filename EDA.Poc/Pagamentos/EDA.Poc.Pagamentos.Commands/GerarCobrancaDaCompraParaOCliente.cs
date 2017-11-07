using System;
using EDA.Poc.Infraestrutura.Messaging;
using EDA.Poc.Pagamentos.Domain.Enums;

namespace EDA.Poc.Pagamentos.Commands
{
    public class GerarCobrancaDaCompraParaOCliente : Command
    {
        public Guid IdDaOrdemDeCompra { get; set; }
        public Guid IdDoProcessoDeCompra { get; set; }
        public Guid IdDoUsuario { get; set; }
        public Guid IdDoCliente { get; set; }
        public Guid IdDoConteudo { get; set; }
        public FormaDePagamento FormaDePagamento { get; set; }
        public TipoDeLicenciamento TipoDeLicenciamento { get; set; }
        public int? QuantidadeDeUsuarios { get; set; }
        public DateTime DisponibilizarAPartirDe { get; set; }
        public TipoDeValidade TipoDeValidade { get; set; }
        public int? QuantidadeDeMeses { get; set; }
        public decimal ValorMensalDaCompra { get; set; }
        public DateTime DataDaCompra { get; set; }
    }
}
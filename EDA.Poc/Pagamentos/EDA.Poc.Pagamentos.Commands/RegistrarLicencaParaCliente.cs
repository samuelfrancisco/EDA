using System;
using EDA.Poc.Infraestrutura.Messaging;
using EDA.Poc.Pagamentos.Domain.Enums;

namespace EDA.Poc.Pagamentos.Commands
{
    public class RegistrarLicencaParaCliente : Command
    {
        public Guid IdDoProcessoDeCompra { get; set; }
        public Guid IdDaOrdemDeCompra { get; set; }
        public Guid IdDoUsuario { get; set; }
        public Guid IdDoCliente { get; set; }
        public Guid IdDoConteudo { get; set; }
        public TipoDeLicenciamento TipoDeLicenciamento { get; set; }
        public int? QuantidadeDeUsuarios { get; set; }
        public DateTime DisponibilizarAPartirDe { get; set; }
        public TipoDeValidade TipoDeValidade { get; set; }
        public int? QuantidadeDeMeses { get; set; }
        public DateTime DataDaCompra { get; set; }
        public decimal ValorMensalDaCompra { get; set; }
    }
}
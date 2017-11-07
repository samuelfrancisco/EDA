using System;
using EDA.Poc.Infraestrutura.Messaging;
using EDA.Poc.Pagamentos.Domain.Enums;

namespace EDA.Poc.Pagamentos.Commands
{
    public class ComprarConteudo : Command
    {
        public Guid IdDoUsuario { get; set; }
        public Guid IdDoCliente { get; set; }
        public Guid IdDoConteudo { get; set; }
        public TipoDeLicenciamento TipoDeLicenciamento { get; set; }
        public int? QuantidadeDeUsuarios { get; set; }
        public DateTime DisponibilizarAPartirDe { get; set; }
        public TipoDeValidade TipoDeValidade { get; set; }
        public int? QuantidadeDeMeses { get; set; }
        public FormaDePagamento FormaDePagamento { get; set; }
    }
}

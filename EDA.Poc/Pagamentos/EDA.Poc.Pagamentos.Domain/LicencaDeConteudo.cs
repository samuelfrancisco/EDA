using System;
using EDA.Poc.Pagamentos.Domain.Enums;

namespace EDA.Poc.Pagamentos.Domain
{
    public class LicencaDeConteudo
    {
        public Guid Id { get; private set; }
        public Guid IdDaOrdemDeCompra { get; set; }
        public Guid IdDoUsuario { get; private set; }
        public Guid IdDoCliente { get; private set; }
        public Guid IdDoConteudo { get; private set; }
        public TipoDeLicenciamento TipoDeLicenciamento { get; private set; }
        public int? QuantidadeDeUsuarios { get; private set; }
        public DateTime DisponibilizarAPartirDe { get; private set; }
        public TipoDeValidade TipoDeValidade { get; private set; }
        public int? QuantidadeDeMeses { get; private set; }
        public DateTime AdiquiridaEm { get; private set; }
        public DateTime RegistradaEm { get; private set; }

        public LicencaDeConteudo(Guid id, Guid idDaOrdemDeCompra, Guid idDoUsuario, Guid idDoCliente, Guid idDoConteudo,
                                 TipoDeLicenciamento tipoDeLicenciamento, int? quantidadeDeUsuarios,
                                 DateTime disponibilizarAPartirDe, TipoDeValidade tipoDeValidade,
                                 int? quantidadeDeMeses, DateTime adiquiridaEm, DateTime registradaEm)
        {
            Id = id;
            IdDaOrdemDeCompra = idDaOrdemDeCompra;
            IdDoUsuario = idDoUsuario;
            IdDoCliente = idDoCliente;
            IdDoConteudo = idDoConteudo;
            TipoDeLicenciamento = tipoDeLicenciamento;
            QuantidadeDeUsuarios = quantidadeDeUsuarios;
            DisponibilizarAPartirDe = disponibilizarAPartirDe;
            TipoDeValidade = tipoDeValidade;
            QuantidadeDeMeses = quantidadeDeMeses;
            AdiquiridaEm = adiquiridaEm;
            RegistradaEm = registradaEm;
        }
    }
}
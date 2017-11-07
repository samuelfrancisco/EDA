using System;
using System.Collections.Generic;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeLicencas
{
    public class DetalhesDaLicenca
    {
        public DetalhesDaLicencaId Id
        {
            get
            {
                return new DetalhesDaLicencaId
                {
                    IdDaLicenca = IdDaLicenca,
                };
            }
        }

        public Guid IdDaLicenca { get; set; }

        public Guid IdDoCliente { get; set; }

        public Guid? IdDaEmpresa { get; set; }

        public Guid? IdDoUsuario { get; set; }

        public Guid IdDoConteudo { get; set; }

        public int? QuantidadeDeUsuarios { get; set; }

        public decimal Custo { get; set; }

        public bool RenovacaoAutomatica { get; set; }

        public string NomeDoUsuario { get; set; }

        public DateTime AdquiridaEm { get; set; }

        public DateTime InicioDaUtilizacao { get; set; }

        public DateTime? FimDaUtilizacao { get; set; }

        public List<DetalhesDoConteudo> DetalhesDoConteudo { get; set; }

        public string RenovacaoAutomaticaLabel
        {
            get
            {
                //TODO Substituir por resource
                return RenovacaoAutomatica ? "Automática" : "Não automática";
            }
        }

        public string CustoLabel
        {
            get
            {
                //TODO Substituir por resource
                return string.Format("R$ {0}", Custo.ToString("F2"));
            }
        }

        public string AdquiridaEmLabel
        {
            get
            {
                return AdquiridaEm.ToShortDateString();
            }
        }

        public string PeriodoDaUtilizacaoLabel
        {
            get
            {
                //TODO Substituir por resource
                return FimDaUtilizacao.HasValue
                    ? string.Format("{0} a {1}", InicioDaUtilizacao.ToShortDateString(), FimDaUtilizacao.Value.ToShortDateString())
                    : string.Format("A partir de {0}", InicioDaUtilizacao.ToShortDateString());

            }
        }
    }
}

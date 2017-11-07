using System;

namespace EDA.Poc.Pagamentos.ReadModel.WebAPI
{
    public class Licenca
    {
        public LicencaId Id
        {
            get
            {
                return new LicencaId
                {
                    IdDoCliente = IdDoCliente,
                    SiglaDoIdioma = SiglaDoIdioma,
                    IdDaLicenca = IdDaLicenca,
                    IdDoConteudo = IdDoConteudo
                };
            }
        }

        public Guid IdDoCliente { get; set; }

        public string SiglaDoIdioma { get; set; }

        public Guid IdDaLicenca { get; set; }

        public Guid IdDoConteudo { get; set; }

        public int? QuantidadeDeUsuarios { get; set; }

        public string Titulo { get; set; }

        public string Objetivo { get; set; }

        public string Descricao { get; set; }

        public string Conteudo { get; set; }

        public string CargaHoraria { get; set; }

        public string ImagemIlustrativa { get; set; }

        public string Banner { get; set; }

        public string Link { get; set; }

        public DateTime DataDeInicio { get; set; }

        public int? QuantidadeDeMeses { get; set; }
    }
}

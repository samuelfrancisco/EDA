using System;

namespace EDA.Poc.Web.ViewModels
{
    public class FiltroDeLicencasViewModel
    {
        public Guid IdDoCliente { get; set; }

        public Guid? IdDaEmpresa { get; set; }

        public Guid? IdDoUsuario { get; set; }

        public bool? RenovacaoAutomatica { get; set; }

        public DateTime? DataDeInicioDeUtilizacao { get; set; }

        public DateTime? DataDeFimDeUtilizacao { get; set; }

        public string Categoria { get; set; }

        public string PalavraChave { get; set; }
    }
}
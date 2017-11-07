using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Poc.Pagamentos.Consultas.PaginaDeLicencas
{
    public class DetalhesDaLicencaFiltros
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

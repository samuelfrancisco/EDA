using System.Threading.Tasks;

namespace EDA.Poc.Pagamentos.Consultas.PaginaInicial
{
    public interface IServicoPaginaInicial
    {
        Task<ReadModel.PaginaInicial.PaginaInicial> ObterAPaginaInicial();
    }
}

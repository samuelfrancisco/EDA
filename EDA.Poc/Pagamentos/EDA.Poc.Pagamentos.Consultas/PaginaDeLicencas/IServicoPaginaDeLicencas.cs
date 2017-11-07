using System;
using System.Threading.Tasks;

namespace EDA.Poc.Pagamentos.Consultas.PaginaDeLicencas
{
    public interface IServicoPaginaDeLicencas
    {
        Task<ReadModel.PaginaDeLicencas.PaginaDeLicencas> ObterAPaginaDeLicencasDoCliente(Guid idDoCliente);
        Task<ReadModel.PaginaDeLicencas.PaginaDeLicencas> ObterAPaginaDeLicencasDaEmpresa(Guid idDaEmpresa);
        Task<ReadModel.PaginaDeLicencas.PaginaDeLicencas> ObterAPaginaDeLicencasDoUsuario(Guid idDoUsuario);
    }
}

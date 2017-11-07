using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDA.Poc.Pagamentos.Consultas.PaginaDeLicencas
{
    public interface IServicoDetalhesDaLicenca
    {
        Task<List<ReadModel.PaginaDeLicencas.DetalhesDaLicenca>> ObterOsDetalhesDasLicencasDoCliente(Guid idDoCliente);
        Task<List<ReadModel.PaginaDeLicencas.DetalhesDaLicenca>> ObterOsDetalhesDasLicencasDoCliente(DetalhesDaLicencaFiltros filtros);
        Task<List<ReadModel.PaginaDeLicencas.DetalhesDaLicenca>> ObterOsDetalhesDasLicencasDaEmpresa(Guid idDaEmpresa);
        Task<List<ReadModel.PaginaDeLicencas.DetalhesDaLicenca>> ObterOsDetalhesDasLicencasDaEmpresa(DetalhesDaLicencaFiltros filtros);
        Task<List<ReadModel.PaginaDeLicencas.DetalhesDaLicenca>> ObterOsDetalhesDasLicencasDoUsuario(Guid idDoUsuario);
        Task<List<ReadModel.PaginaDeLicencas.DetalhesDaLicenca>> ObterOsDetalhesDasLicencasDoUsuario(DetalhesDaLicencaFiltros filtros);
        Task Adicionar(ReadModel.PaginaDeLicencas.DetalhesDaLicenca detalhesDaLicenca);
    }
}
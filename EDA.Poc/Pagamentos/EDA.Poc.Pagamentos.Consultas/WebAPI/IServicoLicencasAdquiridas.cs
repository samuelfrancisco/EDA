using EDA.Poc.Pagamentos.ReadModel.WebAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDA.Poc.Pagamentos.Consultas.WebAPI
{
    public interface IServicoLicencasAdquiridas
    {
        Task<List<Licenca>> ObterLicencas(Guid idDoCliente, string siglaDoIdioma);
        Task<Licenca> ObterLicencaPeloId(Guid idDaLicenca);
        Task Adicionar(Licenca licenca);
    }
}

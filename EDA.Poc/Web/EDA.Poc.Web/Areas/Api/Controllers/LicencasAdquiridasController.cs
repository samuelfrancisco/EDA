using EDA.Poc.Pagamentos.Consultas.WebAPI;
using EDA.Poc.Pagamentos.ReadModel.WebAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace EDA.Poc.Web.Areas.Api.Controllers
{
    public class LicencasAdquiridasController : ApiController
    {
        private readonly IServicoLicencasAdquiridas _servicoLicencasAdquiridas;

        public LicencasAdquiridasController(IServicoLicencasAdquiridas servicoLicencasAdquiridas)
        {
            _servicoLicencasAdquiridas = servicoLicencasAdquiridas;
        }

        public async Task<List<Licenca>> Get()
        {
            var idDoCliente = Guid.Parse("76f91d5c-f602-4823-9086-c8d7678e9557");

            return await _servicoLicencasAdquiridas.ObterLicencas(idDoCliente, "pt-BR");
        }

        public async Task<Licenca> Get(Guid id)
        {
            return await _servicoLicencasAdquiridas.ObterLicencaPeloId(id);
        }
    }
}
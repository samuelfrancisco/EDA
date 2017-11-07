using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using EDA.Poc.Pagamentos.Consultas.PaginaDeLicencas;
using EDA.Poc.Web.ViewModels;
using EDA.Poc.Web.Extensions.MvcExtensions;

namespace EDA.Poc.Web.Controllers
{
    public class LicencasController : Controller
    {
        private readonly IServicoPaginaDeLicencas _servicoPaginaDeLicencas;
        private readonly IServicoDetalhesDaLicenca _servicoDetalhesDaLicenca;

        public LicencasController(IServicoPaginaDeLicencas servicoPaginaDeLicencas,
            IServicoDetalhesDaLicenca servicoDetalhesDaLicenca)
        {
            _servicoPaginaDeLicencas = servicoPaginaDeLicencas;
            _servicoDetalhesDaLicenca = servicoDetalhesDaLicenca;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var idDoCliente = Guid.Parse("76f91d5c-f602-4823-9086-c8d7678e9557");

            var model = await _servicoPaginaDeLicencas.ObterAPaginaDeLicencasDoCliente(idDoCliente).ConfigureAwait(false);

            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> ObterDetalhesDasLicencas()
        {
            var idDoCliente = Guid.Parse("76f91d5c-f602-4823-9086-c8d7678e9557");

            var detalhesDasLicencas = await _servicoDetalhesDaLicenca.ObterOsDetalhesDasLicencasDoCliente(idDoCliente).ConfigureAwait(false);

            var result = new MultipartialResult(this);

            result.AddView("_ListaLicencas", "ListaLicencas", detalhesDasLicencas);

            return result;
        }

        [HttpGet]
        public async Task<JsonResult> ObterDetalhesDasLicencasComFiltros(FiltroDeLicencasViewModel filtros)
        {
            var idDoCliente = Guid.Parse("76f91d5c-f602-4823-9086-c8d7678e9557");

            var detalhesDasLicencas = await _servicoDetalhesDaLicenca
                                                .ObterOsDetalhesDasLicencasDoCliente(new DetalhesDaLicencaFiltros
                                                                                         {
                                                                                             Categoria = filtros.Categoria,
                                                                                             DataDeFimDeUtilizacao = filtros.DataDeFimDeUtilizacao,
                                                                                             DataDeInicioDeUtilizacao = filtros.DataDeInicioDeUtilizacao,
                                                                                             IdDoCliente = idDoCliente,
                                                                                             PalavraChave = filtros.PalavraChave,
                                                                                             RenovacaoAutomatica = filtros.RenovacaoAutomatica
                                                                                         })
                                                .ConfigureAwait(false);

            var result = new MultipartialResult(this);

            result.AddView("_ListaLicencas", "ListaLicencas", detalhesDasLicencas);

            return result;
        }
    }
}
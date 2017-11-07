using System.Threading.Tasks;
using EDA.Poc.Pagamentos.Consultas.PaginaInicial;
using EDA.Poc.Web.Extensions.MvcExtensions;
using System.Web.Mvc;

namespace EDA.Poc.Web.Controllers
{
    public class PaginaInicialController : Controller
    {
        private readonly IServicoPaginaInicial _servicoPaginaInicial;

        public PaginaInicialController(IServicoPaginaInicial servicoPaginaInicial)
        {
            _servicoPaginaInicial = servicoPaginaInicial;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> PaginaInicial()
        {
            var paginaInicial = await _servicoPaginaInicial.ObterAPaginaInicial().ConfigureAwait(false);

            var result = new MultipartialResult(this);

            result.AddView("_ListaLancamentos", "Lancamentos", paginaInicial.Lancamentos);
            result.AddView("_ListaMaisPopulares", "MaisPopulares", paginaInicial.MaisPopulares);
            result.AddView("_ListaDestaquesDaSemana", "DestaquesDaSemana", paginaInicial.DestaquesDaSemana);
            result.AddView("_ListaRecomendados", "RecomendadosParaOCliente", paginaInicial.RecomendadosParaOCliente);
            result.AddView("_ListaRecomendados", "RecomendadosParaSuaEmpresa", paginaInicial.RecomendadosParaSuaEmpresa);
            result.AddView("_ListaRecomendados", "RecomendadosParaSuaEquipe", paginaInicial.RecomendadosParaSuaEquipe);

            return result;
        }
    }
}

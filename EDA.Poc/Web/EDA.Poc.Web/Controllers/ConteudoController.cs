namespace EDA.Poc.Web.Controllers
{
    using Pagamentos.Consultas.PaginaDeDetalhesDoConteudo;
    using Extensions.MvcExtensions;
    using Infraestrutura.Images.Services.Interfaces;
    using Infraestrutura.Messaging.Interfaces;
    using Pagamentos.Commands;
    using Pagamentos.Domain.Enums;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ViewModels;

    public class ConteudoController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IServicoPaginaDeDetalhesDoConteudo _servicoPaginaDeDetalhesDoConteudo;
        private readonly IImagePathService _servicoImagem;

        public ConteudoController(ICommandBus commandBus, IServicoPaginaDeDetalhesDoConteudo servicoPaginaDeDetalhesDoConteudo,
            IImagePathService servicoImagem)
        {
            _commandBus = commandBus;
            _servicoPaginaDeDetalhesDoConteudo = servicoPaginaDeDetalhesDoConteudo;
            _servicoImagem = servicoImagem;
        }

        [HttpGet]
        public async Task<ActionResult> Detalhes(Guid idDoConteudo)
        {
            ViewBag.IdDoConteudo = idDoConteudo;

            var idDoCliente = Guid.Parse("76f91d5c-f602-4823-9086-c8d7678e9557");

            var idDoUsuario = Guid.Parse("a59e8e09-6669-4412-aa7d-9a3b2e82adb1");

            var paginaDeDetalhes = await _servicoPaginaDeDetalhesDoConteudo.ObterAPaginaDeDetalhesDoConteudo(idDoConteudo).ConfigureAwait(false);

            return View(paginaDeDetalhes);
        }

        [HttpPost]
        public async Task<JsonResult> Comprar(DadosDeCompraViewModel dadosCompra)
        {
            if (ModelState.IsValid)
            {
                var idDoCliente = Guid.Parse("76f91d5c-f602-4823-9086-c8d7678e9557");

                var idDoUsuario = Guid.Parse("a59e8e09-6669-4412-aa7d-9a3b2e82adb1");

                var comando = new ComprarConteudo
                                  {
                                      IdDoUsuario = idDoUsuario,
                                      IdDoCliente = idDoCliente,
                                      IdDoConteudo = dadosCompra.IdDoConteudo,
                                      TipoDeLicenciamento = dadosCompra.TipoDeLicenciamento,
                                      QuantidadeDeUsuarios = dadosCompra.QuantidadeDeUsuarios,
                                      DisponibilizarAPartirDe = dadosCompra.DisponibilizarAPartirDe,
                                      TipoDeValidade = dadosCompra.TipoDeValidade,
                                      QuantidadeDeMeses = dadosCompra.QuantidadeDeMesesDeValidade,
                                      FormaDePagamento = FormaDePagamento.CobrancaDireta
                                  };

                await _commandBus.SendOne(comando).ConfigureAwait(false);

                return Json(new { Sucesso = true });
            }

            return Json(new { Sucesso = false });
        }

        [HttpPost]
        public async Task<JsonResult> Avaliar(AvaliarViewModel minhaAvaliacao)
        {
            if (ModelState.IsValid)
            {
                var idDoCliente = Guid.Parse("76f91d5c-f602-4823-9086-c8d7678e9557");

                var idDoUsuario = minhaAvaliacao.IdDoUsuario;

                var comando = new AvaliarConteudo
                                  {
                                      IdDoCliente = idDoCliente,
                                      IdDoUsuario = idDoUsuario,
                                      IdDoConteudo = minhaAvaliacao.IdDoConteudo,
                                      Avaliacao = minhaAvaliacao.Avaliacao,
                                      Conteudo = minhaAvaliacao.Conteudo
                                  };

                await _commandBus.SendOne(comando).ConfigureAwait(false);

                var result = await AguardarAtualizacaoDoReadModelDasAvaliacoes(comando).ConfigureAwait(false);

                return result;
            }

            return Json(new { Sucesso = false });
        }

        private Task<JsonResult> AguardarAtualizacaoDoReadModelDasAvaliacoes(AvaliarConteudo comando)
        {
            return Task.Run(async () =>
                                      {
                                          var cancelationTokenSource = new CancellationTokenSource();
                                          const string idioma = "pt-BR";

                                          var consultarReadModel = Task.Run(async () =>
                                                                                      {
                                                                                          while (true)
                                                                                          {
                                                                                              if (cancelationTokenSource.Token.IsCancellationRequested)
                                                                                                  return null;

                                                                                              var cultureInfo = Thread.CurrentThread.CurrentCulture;

                                                                                              var paginaDeDetalhesDoConteudo = await _servicoPaginaDeDetalhesDoConteudo
                                                                                                                                         .ObterAPaginaDeDetalhesDoConteudo(comando.IdDoConteudo)
                                                                                                                                         .ConfigureAwait(false);

                                                                                              if (paginaDeDetalhesDoConteudo.AvaliacoesRealizadas.Any(x => x.IdDoComandoOriginador == comando.Id))
                                                                                              {
                                                                                                  var multipartialResult = new MultipartialResult(this);

                                                                                                  var minhaAvaliacao = paginaDeDetalhesDoConteudo.Resenhas.SingleOrDefault(x => x.IdDoUsuario == comando.IdDoUsuario);

                                                                                                  multipartialResult.AddView("_ConsolidadoAvaliacoes", "ConsolidadoAvaliacoes", paginaDeDetalhesDoConteudo.ConsolidadoAvaliacao);
                                                                                                  multipartialResult.AddView("_MinhaAvaliacao", "MinhaAvaliacao", new AvaliarViewModel(minhaAvaliacao));
                                                                                                  multipartialResult.AddView("_ListaResenhas", "Resenhas", paginaDeDetalhesDoConteudo.Resenhas);
                                                                                                  multipartialResult.AddView("_Detalhes", "Detalhes", paginaDeDetalhesDoConteudo.Detalhes.Single(x => x.SiglaDoIdioma == idioma));

                                                                                                  return multipartialResult;
                                                                                              }

                                                                                              await Task.Delay(500);
                                                                                          }

                                                                                      }, cancelationTokenSource.Token);

                                          var timeOut = Task.Run(() => Task.Delay(5000));

                                          var completedTask = await Task.WhenAny(consultarReadModel, timeOut).ConfigureAwait(false);

                                          if (completedTask == timeOut)
                                          {
                                              cancelationTokenSource.Cancel();

                                              return new JsonResult
                                                         {
                                                             Data = new
                                                                        {
                                                                            Sucesso = false
                                                                        }
                                                         };
                                          }

                                          return await consultarReadModel;
                                      });
        }
    }
}



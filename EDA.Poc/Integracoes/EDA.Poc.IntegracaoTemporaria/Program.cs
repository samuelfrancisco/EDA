using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.Messaging.Exceptions;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;
using EDA.Poc.IntegracaoTemporaria.Conteudos;
using EDA.Poc.IntegracaoTemporaria.Events;
using EDA.Poc.Pagamentos.Consultas.IntegracaoTemporaria;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeLicencas;
using EDA.Poc.Pagamentos.ReadModel.PaginaInicial;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using EDA.Poc.Pagamentos.Commands;
using EDA.Poc.Pagamentos.ReadModel.Usuarios;

namespace EDA.Poc.IntegracaoTemporaria
{
    class Program
    {
        private static readonly string ReadModelDatabase = ConfigurationManager.AppSettings["ReadModelDatabase"];
        private static readonly string MongoConnectionString = ConfigurationManager.ConnectionStrings["MongoIntegracaoTemporariaEventStore"].ConnectionString;
        private static readonly string MongoReadModelConnectionString = ConfigurationManager.ConnectionStrings["MongoReadModelStore"].ConnectionString;
        private const string DatabaseName = "MarketPlaceIntegracaoTemporariaEventStore";
        private const string ReadModelDatabaseName = "MarketPlaceReadModelStore";
        private static ContainerBootstrapper _containerBootstrapper;
        private static ICommandBus _commandBus;
        private static IEventBus _eventBus;
        private static MongoClient _mongoClient;
        private static MongoServer _mongoServer;
        private static MongoDatabase _mongoDatabase;
        private static MongoClient _mongoReadModelClient;
        private static MongoServer _mongoReadModelServer;
        private static MongoDatabase _mongoReadModelDatabase;

        private static readonly AReliquiaIndiana AReliquiaIndiana = new AReliquiaIndiana();
        private static readonly BusinessGame EGuruTalent = new BusinessGame();
        private static readonly Crencas Crencas = new Crencas();
        private static readonly EntendendoAsCrencas EntendendoAsCrencas = new EntendendoAsCrencas();
        private static readonly ManejandoObjecoes ManejandoObjecoes = new ManejandoObjecoes();
        private static readonly EstilosDeNegociacao EstilosDeNegociacao = new EstilosDeNegociacao();
        private static readonly LojaDosHerois LojaDosHerois = new LojaDosHerois();
        private static readonly Os4CantosDoMundo Os4CantosDoMundo = new Os4CantosDoMundo();
        private static readonly TCM TCM = new TCM();
        private static readonly SuperFive SuperFive = new SuperFive();
        private static readonly AncorasDeCarreira AncorasDeCarreira = new AncorasDeCarreira();
        private static readonly MultiplasInteligencias MultiplasInteligencias = new MultiplasInteligencias();
        private static readonly SobNovaLideranca SobNovaLideranca = new SobNovaLideranca();
        private static readonly LeaderNoir LeaderNoir = new LeaderNoir();
        private static readonly OLiderDoJogo OLiderDoJogo = new OLiderDoJogo();
        private static readonly Objecoes Objecoes = new Objecoes();
        private static readonly ClienteCarente ClienteCarente = new ClienteCarente();
        private static readonly GameEspelho GameEspelho = new GameEspelho();
        private static readonly VendemosSolucoes VendemosSolucoes = new VendemosSolucoes();
        private static readonly SolucaoEProblema SolucaoEProblema = new SolucaoEProblema();
        private static readonly ResponsaveisPorResultados ResponsaveisPorResultados = new ResponsaveisPorResultados();

        static void Main(string[] args)
        {
            _containerBootstrapper = ContainerBootstrapper.Bootstrap();

            _eventBus = _containerBootstrapper.Container.Resolve<IEventBus>();
            _commandBus = _containerBootstrapper.Container.Resolve<ICommandBus>();

            _mongoClient = new MongoClient(MongoConnectionString);
            _mongoServer = _mongoClient.GetServer();
            _mongoDatabase = _mongoServer.GetDatabase(DatabaseName);

            _mongoReadModelClient = new MongoClient(MongoReadModelConnectionString);
            _mongoReadModelServer = _mongoReadModelClient.GetServer();
            _mongoReadModelDatabase = _mongoReadModelServer.GetDatabase(ReadModelDatabaseName);

            while (true)
            {
                Console.WriteLine("Deseja excluir todos os bancos? [S] Sim ou [N] Não");

                var key = Console.ReadKey();

                if (key.KeyChar == 's' || key.KeyChar == 'S')
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Iniciando exclusão dos bancos");
                    Console.WriteLine();

                    ApagarTodosOsBancos().Wait();

                    Console.WriteLine("Bancos excluídos com sucesso");
                    Console.WriteLine();
                    break;
                }

                if (key.KeyChar == 'n' || key.KeyChar == 'N')
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    break;
                }
            }

            if (ReadModelDatabase == "MongoDb")
            {
                Console.WriteLine("Iniciando cadastro de dados");
                Console.WriteLine();

                CadastrarConteudosNoMongo().Wait();

                Console.WriteLine("Dados cadastrados com sucesso");
                Console.WriteLine();
            }

            Console.WriteLine("Iniciando sincronização dos dados");
            Console.WriteLine();

            Task.WaitAll(new[] { SincronizarClientes(), SincronizarConteudos() });

            Console.WriteLine("Dados sincronizados com sucesso");
            Console.WriteLine();

            Console.WriteLine("Adicionando comentários");
            Console.WriteLine();

            Task.WaitAll(new[] { AdicionarComentarios() });

            Console.WriteLine("Comentários adicionados com sucesso");
            Console.WriteLine();

            Console.WriteLine("Pressione enter para encerrar");
            Console.ReadLine();
        }

        private static Task ApagarTodosOsBancos()
        {
            return Task.Run(() =>
                                {
                                    var databaseNames = _mongoServer.GetDatabaseNames();

                                    foreach (var databaseName in databaseNames.Where(x => x.Contains("MarketPlace")))
                                    {
                                        _mongoServer.DropDatabase(databaseName);
                                    }
                                });
        }

        private static Task SincronizarClientes()
        {
            return Task.Run(async () =>
                                      {
                                          var clientesJaCadastrados = await ObterClientesCadastrados().ConfigureAwait(false);

                                          if (clientesJaCadastrados.Any(x => x == Constantes.IdDoCliente)) return;

                                          var evento = new NovoClienteRegistrado
                                                           {
                                                               IdDoCliente = Constantes.IdDoCliente
                                                           };

                                          await _eventBus.PublishOne(evento);

                                          await SalvarEventosNovoClienteRegistradoPublicados(new[] { evento }).ConfigureAwait(false);
                                      });
        }

        private static Task AdicionarComentarios()
        {
            return Task.Run(async () =>
                                      {
                                          var comandos = new List<AvaliarConteudo>();

                                          var resenhas = new[]
                                                             {
                                                                 "Ajudou bem!",
                                                                 "Muito bom.",
                                                                 "Gostei muito deste curso!",
                                                                 "Parabens pelo conteúdo.",
                                                                 "Sensacional!",
                                                                 "Útil!"
                                                             };

                                          var notas = new[] { 3, 4, 5, 4, 5, 4 };

                                          var conteudos = new List<Lancamento>
                                                              {
                                                                  Crencas.ToLancamento(),
                                                                  EntendendoAsCrencas.ToLancamento(),
                                                                  ManejandoObjecoes.ToLancamento(),
                                                                  Objecoes.ToLancamento(),
                                                                  EGuruTalent.ToLancamento(),
                                                                  AReliquiaIndiana.ToLancamento(),
                                                                  LojaDosHerois.ToLancamento(),
                                                                  EstilosDeNegociacao.ToLancamento(),
                                                                  TCM.ToLancamento(),
                                                                  Os4CantosDoMundo.ToLancamento(),
                                                                  SuperFive.ToLancamento(),
                                                                  AncorasDeCarreira.ToLancamento(),
                                                                  MultiplasInteligencias.ToLancamento(),
                                                                  SobNovaLideranca.ToLancamento(),
                                                                  LeaderNoir.ToLancamento(),
                                                                  OLiderDoJogo.ToLancamento(),
                                                                  ClienteCarente.ToLancamento(),
                                                                  GameEspelho.ToLancamento(),
                                                                  VendemosSolucoes.ToLancamento(),
                                                                  SolucaoEProblema.ToLancamento(),
                                                                  ResponsaveisPorResultados.ToLancamento(),
                                                              };

                                          var usuarios = GerarUsuarios();

                                          var random = new Random();

                                          foreach (var conteudo in conteudos)
                                          {
                                              foreach (var usuario in usuarios.Where(x => x.Nome != "Felipe Azevedo"))
                                              {
                                                  var indexAvaliacao = random.Next(0, 5);

                                                  var comando = new AvaliarConteudo
                                                                    {
                                                                        IdDoCliente = Constantes.IdDoCliente,
                                                                        IdDoUsuario = usuario.Id,
                                                                        IdDoConteudo = conteudo.IdDoConteudo,
                                                                        Avaliacao = notas[indexAvaliacao],
                                                                        Conteudo = resenhas[indexAvaliacao]
                                                                    };

                                                  comandos.Add(comando);

                                              }
                                          }

                                          await _commandBus.SendMany(comandos);
                                      });
        }

        private static Task<IEnumerable<Guid>> ObterClientesCadastrados()
        {
            return Task.Run<IEnumerable<Guid>>(() =>
                                                   {
                                                       var collection = _mongoDatabase.GetCollection<NovoClienteRegistrado>("NovoClienteRegistrado");

                                                       var eventos = collection.AsQueryable().ToList();

                                                       return eventos.Select(x => x.IdDoCliente).ToList();
                                                   });
        }

        private static Task SalvarEventosNovoClienteRegistradoPublicados(IEnumerable<NovoClienteRegistrado> eventosPublicados)
        {
            return Task.Run(() =>
                                {
                                    var collection = _mongoDatabase.GetCollection<NovoClienteRegistrado>("NovoClienteRegistrado");

                                    foreach (var evento in eventosPublicados)
                                    {
                                        collection.Insert(evento);
                                    }
                                });
        }

        private static Task SincronizarConteudos()
        {
            return Task.Run(async () =>
                                      {
                                          var servicoConteudosCadastrados = _containerBootstrapper.Container.Resolve<IServicoConteudosCadastrados>();

                                          var conteudos = await servicoConteudosCadastrados.ObterTodosOsConteudosCadastrados().ConfigureAwait(false);

                                          var conteudosJaCadastrados = await ObterConteudosCadastrados().ConfigureAwait(false);

                                          var eventos = conteudos
                                              .Where(x => conteudosJaCadastrados.All(y => y != x.IdDoConteudo))
                                              .Select(x => new NovoConteudoCadastrado
                                                               {
                                                                   IdDoConteudo = x.IdDoConteudo,
                                                                   PrecoSemLimitesDeUsuarios = x.PrecoIlimitado,
                                                                   FaixaDePrecos = x.FaixaDePrecos
                                                                       .Select(y => new NovoConteudoCadastradoFaixaDePreco
                                                                                        {
                                                                                            Preco = y.Preco,
                                                                                            QuantidadeInicialDeLicencas = y.QuantidadeInicialDeLicencas,
                                                                                            QuantidadeFinalDeLicencas = y.QuantidadeFinalDeLicencas
                                                                                        }).ToList(),
                                                                   Promocoes = new List<NovoConteudoCadastradoPromocao>()
                                                               }).ToList();

                                          var eventosPublicados = eventos.ToList();

                                          try
                                          {
                                              await _eventBus.PublishMany(eventos).ConfigureAwait(false);
                                          }
                                          catch (EventPublishException ex)
                                          {
                                              foreach (var evento in ex.NotPublishedEvents.Select(x => eventosPublicados.First(y => y.Id == x.Event.Id)))
                                              {
                                                  eventosPublicados.Remove(evento);
                                              }
                                          }

                                          await SalvarEventosNovoConteudoCadastradoPublicados(eventosPublicados).ConfigureAwait(false);
                                      });
        }

        private static Task<IEnumerable<Guid>> ObterConteudosCadastrados()
        {
            return Task.Run<IEnumerable<Guid>>(() =>
                                                   {
                                                       var collection = _mongoDatabase.GetCollection<NovoConteudoCadastrado>("NovoConteudoCadastrado");

                                                       var eventos = collection.AsQueryable().ToList();

                                                       return eventos.Select(x => x.IdDoConteudo).ToList();
                                                   });
        }

        private static Task SalvarEventosNovoConteudoCadastradoPublicados(IEnumerable<NovoConteudoCadastrado> eventosPublicados)
        {
            return Task.Run(() =>
                                {
                                    var collection = _mongoDatabase.GetCollection<NovoConteudoCadastrado>("NovoConteudoCadastrado");

                                    foreach (var evento in eventosPublicados)
                                    {
                                        collection.Insert(evento);
                                    }
                                });
        }

        private static Task CadastrarConteudosNoMongo()
        {
            var tasks = new[]
                            {
                                CadastrarPaginaInicial(),

                                CadastrarDetalhesDosConteudos(),

                                CadastrarPaginasDeLicencas(),

                                CadastrarUsuarios()
                            };

            return Task.WhenAll(tasks);
        }

        private static Task CadastrarPaginaInicial()
        {
            return Task.Run(() =>
                                {
                                    var paginaInicial = GerarPaginaInicial();

                                    var collectionPaginaInicial = _mongoReadModelDatabase.GetCollection<PaginaInicial>("PaginaInicial");

                                    if (collectionPaginaInicial.AsQueryable().Any()) return;

                                    collectionPaginaInicial.Insert(paginaInicial);
                                });
        }

        private static PaginaInicial GerarPaginaInicial()
        {
            var paginaInicial = new PaginaInicial
                                    {
                                        Lancamentos = new List<Lancamento>
                                                          {
                                                              EGuruTalent.ToLancamento(),
                                                              AReliquiaIndiana.ToLancamento()
                                                          },
                                        MaisPopulares = new List<MaisPopular>
                                                            {
                                                                Objecoes.ToMaisPopular(),
                                                                AReliquiaIndiana.ToMaisPopular(),
                                                                LojaDosHerois.ToMaisPopular(),
                                                                EstilosDeNegociacao.ToMaisPopular(),
                                                                TCM.ToMaisPopular(),
                                                                Os4CantosDoMundo.ToMaisPopular(),
                                                                SuperFive.ToMaisPopular(),
                                                                ClienteCarente.ToMaisPopular(),
                                                                GameEspelho.ToMaisPopular(),
                                                                SolucaoEProblema.ToMaisPopular(),
                                                                VendemosSolucoes.ToMaisPopular(),
                                                                ResponsaveisPorResultados.ToMaisPopular(),
                                                            },
                                        RecomendadosParaOCliente = new List<RecomendadoParaOCliente>
                                                                       {
                                                                           Crencas.ToRecomendadoParaOCliente(),
                                                                           EntendendoAsCrencas.ToRecomendadoParaOCliente(),
                                                                           AncorasDeCarreira.ToRecomendadoParaOCliente(),
                                                                           MultiplasInteligencias.ToRecomendadoParaOCliente(),
                                                                           SobNovaLideranca.ToRecomendadoParaOCliente(),
                                                                           LeaderNoir.ToRecomendadoParaOCliente(),
                                                                           ManejandoObjecoes.ToRecomendadoParaOCliente(),
                                                                           OLiderDoJogo.ToRecomendadoParaOCliente(),
                                                                       },
                                        RecomendadosParaSuaEmpresa = new List<RecomendadoParaSuaEmpresa>
                                                                         {
                                                                             Crencas.ToRecomendadoParaSuaEmpresa(),
                                                                             EntendendoAsCrencas.ToRecomendadoParaSuaEmpresa(),
                                                                             AncorasDeCarreira.ToRecomendadoParaSuaEmpresa(),
                                                                             MultiplasInteligencias.ToRecomendadoParaSuaEmpresa(),
                                                                             SobNovaLideranca.ToRecomendadoParaSuaEmpresa(),
                                                                             LeaderNoir.ToRecomendadoParaSuaEmpresa(),
                                                                             ManejandoObjecoes.ToRecomendadoParaSuaEmpresa(),
                                                                             OLiderDoJogo.ToRecomendadoParaSuaEmpresa(),
                                                                         },
                                        RecomendadosParaSuaEquipe = new List<RecomendadoParaSuaEquipe>
                                                                        {
                                                                            Crencas.ToRecomendadoParaSuaEquipe(),
                                                                            EntendendoAsCrencas.ToRecomendadoParaSuaEquipe(),
                                                                            AncorasDeCarreira.ToRecomendadoParaSuaEquipe(),
                                                                            MultiplasInteligencias.ToRecomendadoParaSuaEquipe(),
                                                                            SobNovaLideranca.ToRecomendadoParaSuaEquipe(),
                                                                            ManejandoObjecoes.ToRecomendadoParaSuaEquipe(),
                                                                            LeaderNoir.ToRecomendadoParaSuaEquipe(),
                                                                            OLiderDoJogo.ToRecomendadoParaSuaEquipe(),
                                                                        },
                                        DestaquesDaSemana = new List<DestaqueDaSemana>
                                                                {
                                                                    new DestaqueDaSemana
                                                                        {
                                                                            IdDoCliente = Constantes.IdDoCliente,
                                                                            IdDaEscola = Constantes.IdDaEscola,
                                                                            SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                                                            IdDaPromocao = Constantes.IdDaPromocao
                                                                        }
                                                                }
                                    };
            return paginaInicial;
        }

        private static Task CadastrarDetalhesDosConteudos()
        {
            return Task.Run(() =>
                                {
                                    var detalhes = GerarDetalhesDosConteudos();

                                    var collectionPaginaDeDetalhesDoConteudo = _mongoReadModelDatabase.GetCollection<PaginaDeDetalhesDoConteudo>("PaginaDeDetalhesDoConteudo");

                                    collectionPaginaDeDetalhesDoConteudo.InsertBatch(detalhes);
                                });
        }

        private static IEnumerable<PaginaDeDetalhesDoConteudo> GerarDetalhesDosConteudos()
        {
            var paginasDeDetalhes = new List<PaginaDeDetalhesDoConteudo>
                                        {
                                            EGuruTalent.ToPaginaDeDetalhesDoConteudo(),
                                            Crencas.ToPaginaDeDetalhesDoConteudo(),
                                            EntendendoAsCrencas.ToPaginaDeDetalhesDoConteudo(),
                                            ManejandoObjecoes.ToPaginaDeDetalhesDoConteudo(),
                                            Objecoes.ToPaginaDeDetalhesDoConteudo(),
                                            AReliquiaIndiana.ToPaginaDeDetalhesDoConteudo(),
                                            LojaDosHerois.ToPaginaDeDetalhesDoConteudo(),
                                            EstilosDeNegociacao.ToPaginaDeDetalhesDoConteudo(),
                                            TCM.ToPaginaDeDetalhesDoConteudo(),
                                            Os4CantosDoMundo.ToPaginaDeDetalhesDoConteudo(),
                                            SuperFive.ToPaginaDeDetalhesDoConteudo(),
                                            AncorasDeCarreira.ToPaginaDeDetalhesDoConteudo(),
                                            MultiplasInteligencias.ToPaginaDeDetalhesDoConteudo(),
                                            SobNovaLideranca.ToPaginaDeDetalhesDoConteudo(),
                                            LeaderNoir.ToPaginaDeDetalhesDoConteudo(),
                                            OLiderDoJogo.ToPaginaDeDetalhesDoConteudo(),
                                            ClienteCarente.ToPaginaDeDetalhesDoConteudo(),
                                            GameEspelho.ToPaginaDeDetalhesDoConteudo(),
                                            VendemosSolucoes.ToPaginaDeDetalhesDoConteudo(),
                                            SolucaoEProblema.ToPaginaDeDetalhesDoConteudo(),
                                            ResponsaveisPorResultados.ToPaginaDeDetalhesDoConteudo()
                                        };

            return paginasDeDetalhes;
        }

        private static Task CadastrarPaginasDeLicencas()
        {
            return Task.Run(() =>
                                {
                                    var paginaDeLicencasDoCliente = new PaginaDeLicencas
                                                                        {
                                                                            IdDoCliente = Constantes.IdDoCliente,
                                                                            Version = 1
                                                                        };

                                    var paginaDeLicencasDaEmpresa = new PaginaDeLicencas
                                                                        {
                                                                            IdDoCliente = Constantes.IdDoCliente,
                                                                            IdDaEmpresa = Constantes.IdDaEmpresa,
                                                                            Version = 1
                                                                        };

                                    var paginaDeLicencasDoUsuario = new PaginaDeLicencas
                                                                        {
                                                                            IdDoCliente = Constantes.IdDoCliente,
                                                                            IdDaEmpresa = Constantes.IdDaEmpresa,
                                                                            IdDoUsuario = Constantes.IdDoUsuario,
                                                                            Version = 1
                                                                        };

                                    var collectionPaginasDeLicencas = _mongoReadModelDatabase.GetCollection<PaginaDeLicencas>("PaginaDeLicencas");

                                    collectionPaginasDeLicencas.Insert(paginaDeLicencasDoCliente);

                                    collectionPaginasDeLicencas.Insert(paginaDeLicencasDaEmpresa);

                                    collectionPaginasDeLicencas.Insert(paginaDeLicencasDoUsuario);
                                });
        }

        private static Task CadastrarUsuarios()
        {
            return Task.Run(() =>
                                {
                                    var usuarios = GerarUsuarios();

                                    var collectionPaginaInicial = _mongoReadModelDatabase.GetCollection<Usuario>("Usuarios");

                                    if (collectionPaginaInicial.AsQueryable().Any()) return;

                                    collectionPaginaInicial.InsertBatch(usuarios);
                                });
        }

        private static IEnumerable<Usuario> GerarUsuarios()
        {
            var usuarios = new List<Usuario>
                               {
                                   Constantes.DiegoMarques,
                                   Constantes.SamuelFrancisco,
                                   Constantes.RafaelDiGenova,
                                   Constantes.LeonardoOliveira,
                                   Constantes.FlavioPortugal,
                                   Constantes.FelipeAzevedo
                               };

            //using (var stream = new FileStream(@"C:\Users\samuel.francisco\Desktop\JMeter\IdsDosUsuarios.txt", FileMode.Create, FileAccess.ReadWrite))
            //using (var streamWriter = new StreamWriter(stream))
            //{
            //    for (int i = 0; i < 1000; i++)
            //    {
            //        var id = Guid.NewGuid();

            //        var usuario = new Usuario
            //        {
            //            Id = id,
            //            Nome = string.Format("Usuario de Teste {0}", i + 1)
            //        };

            //        usuarios.Add(usuario);

            //        streamWriter.WriteLine(id);
            //    }
            //}

            return usuarios;
        }
    }
}
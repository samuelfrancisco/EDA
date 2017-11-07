using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EDA.Poc.Infraestrutura.ReadModel.Interfaces;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeLicencas;
using EDA.Poc.Pagamentos.ReadModel.PaginaInicial;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace EDA.Poc.Pagamentos.Consultas.MongoDb.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                                   .Where(x => x.Name.Contains("Servico"))
                                   .WithServiceDefaultInterfaces()
                                   .LifestyleTransient()
                                   .Configure(x => x.DependsOn(Dependency.OnComponent(typeof(MongoClient), "readModelMongoClient"))));

            MongoClassMap();

            ConfigureIndexes(container);
        }

        private void MongoClassMap()
        {
            BsonClassMap.RegisterClassMap<Lancamento>(x =>
                                                          {
                                                              x.AutoMap();
                                                              x.UnmapMember(m => m.Imagem);
                                                              x.UnmapMember(m => m.ImagemMobile);
                                                          });

            BsonClassMap.RegisterClassMap<DestaqueDaSemana>(x =>
                                                                {
                                                                    x.AutoMap();
                                                                    x.UnmapMember(m => m.Banner);
                                                                    x.UnmapMember(m => m.BannerMobile);
                                                                });

            BsonClassMap.RegisterClassMap<MaisPopular>(x =>
                                                           {
                                                               x.AutoMap();
                                                               x.UnmapMember(m => m.Imagem);
                                                           });

            BsonClassMap.RegisterClassMap<RecomendadoParaSuaEmpresa>(x =>
                                                                         {
                                                                             x.AutoMap();
                                                                             x.UnmapMember(m => m.Imagem);
                                                                         });

            BsonClassMap.RegisterClassMap<RecomendadoParaOCliente>(x =>
                                                                       {
                                                                           x.AutoMap();
                                                                           x.UnmapMember(m => m.Imagem);
                                                                       });

            BsonClassMap.RegisterClassMap<RecomendadoParaSuaEquipe>(x =>
                                                                        {
                                                                            x.AutoMap();
                                                                            x.UnmapMember(m => m.Imagem);
                                                                        });

            BsonClassMap.RegisterClassMap<Detalhes>(x =>
                                                        {
                                                            x.AutoMap();
                                                            x.UnmapMember(m => m.Imagem);
                                                        });

            BsonClassMap.RegisterClassMap<Preview>(x =>
                                                        {
                                                            x.AutoMap();
                                                            x.UnmapMember(m => m.Imagem);
                                                        });

            BsonClassMap.RegisterClassMap<Semelhante>(x =>
                                                          {
                                                              x.AutoMap();
                                                              x.UnmapMember(m => m.Imagem);
                                                          });

            BsonClassMap.RegisterClassMap<MaisDoFornecedor>(x =>
                                                                {
                                                                    x.AutoMap();
                                                                    x.UnmapMember(m => m.Imagem);
                                                                });

            BsonClassMap.RegisterClassMap<Resenha>(x =>
                                                       {
                                                           x.AutoMap();
                                                           x.UnmapMember(m => m.DataLabel);
                                                           x.UnmapMember(m => m.ImagemDoUsuario);
                                                           x.UnmapMember(m => m.DataLabel);
                                                       });

            BsonClassMap.RegisterClassMap<DetalhesDaLicenca>(x =>
                                                                 {
                                                                     x.AutoMap();
                                                                     x.UnmapMember(m => m.PeriodoDaUtilizacaoLabel);
                                                                     x.UnmapMember(m => m.RenovacaoAutomaticaLabel);
                                                                     x.UnmapMember(m => m.AdquiridaEmLabel);
                                                                     x.UnmapMember(m => m.CustoLabel);
                                                                 });

            var type = typeof(IVersionedReadModel);

            var types = Assembly.GetAssembly(typeof(Lancamento))
                .GetTypes()
                .Where(type.IsAssignableFrom)
                .Where(t => t.IsClass);

            foreach (var t in types)
                BsonClassMap.LookupClassMap(t);

            type = typeof(IReadModelProcessedEvent);

            types = Assembly.GetAssembly(typeof(Lancamento))
                .GetTypes()
                .Where(type.IsAssignableFrom)
                .Where(t => t.IsClass);

            foreach (var t in types)
                BsonClassMap.LookupClassMap(t);
        }

        private void ConfigureIndexes(IWindsorContainer container)
        {
            var mongoClient = container.Resolve<MongoClient>("readModelMongoClient");

            var mongoServer = mongoClient.GetServer();

            var mongoDatabase = mongoServer.GetDatabase("MarketPlaceReadModelStore");

            if (!mongoDatabase.GetCollection("PaginaDeDetalhesDoConteudo").Exists())
                mongoDatabase.CreateCollection("PaginaDeDetalhesDoConteudo");

            if (!mongoDatabase.GetCollection("PaginaDeDetalhesDoConteudo_EventStream").Exists())
                mongoDatabase.CreateCollection("PaginaDeDetalhesDoConteudo_EventStream");

            if (!mongoDatabase.GetCollection("PaginaDeLicencas").Exists())
                mongoDatabase.CreateCollection("PaginaDeLicencas");

            if (!mongoDatabase.GetCollection("PaginaDeLicencas_EventStream").Exists())
                mongoDatabase.CreateCollection("PaginaDeLicencas_EventStream");

            if (!mongoDatabase.GetCollection("DetalhesDaLicenca").Exists())
                mongoDatabase.CreateCollection("DetalhesDaLicenca");

            if (!mongoDatabase.GetCollection("PaginaInicial").Exists())
                mongoDatabase.CreateCollection("PaginaInicial");

            if (!mongoDatabase.GetCollection("PaginaInicial_EventStream").Exists())
                mongoDatabase.CreateCollection("PaginaInicial_EventStream");

            var paginaDeDetalhesCollection = mongoDatabase.GetCollection<ReadModel.PaginaDeDetalhesDoConteudo.PaginaDeDetalhesDoConteudo>("PaginaDeDetalhesDoConteudo");
            paginaDeDetalhesCollection.CreateIndex("Identity", "Version", "IdDoConteudo");

            var paginaDeDetalhesEventosCollection = mongoDatabase.GetCollection<ReadModel.PaginaDeDetalhesDoConteudo.PaginaDeDetalhesDoConteudo>("PaginaDeDetalhesDoConteudo_EventStream");
            paginaDeDetalhesEventosCollection.CreateIndex("ReadModelIdentity", "EventUtcDate");

            var paginaDeLicencasCollection = mongoDatabase.GetCollection<ReadModel.PaginaDeLicencas.PaginaDeLicencas>("PaginaDeLicencas");
            paginaDeLicencasCollection.CreateIndex("Identity", "Version", "IdDoCliente", "IdDaEmpresa", "IdDoUsuario");

            var paginaDeLicencasEventosCollection = mongoDatabase.GetCollection<ReadModel.PaginaDeDetalhesDoConteudo.PaginaDeDetalhesDoConteudo>("PaginaDeLicencas_EventStream");
            paginaDeLicencasEventosCollection.CreateIndex("ReadModelIdentity", "EventUtcDate");

            var detalhesDaLicencaCollection = mongoDatabase.GetCollection<DetalhesDaLicenca>("DetalhesDaLicenca");
            detalhesDaLicencaCollection.CreateIndex("IdDaLicenca", "IdDoCliente", "IdDaEmpresa", "IdDoUsuario", "IdDoConteudo");

            var paginaInicialCollection = mongoDatabase.GetCollection<ReadModel.PaginaInicial.PaginaInicial>("PaginaInicial");
            paginaInicialCollection.CreateIndex("Identity", "Version");

            var paginaInicialEventosCollection = mongoDatabase.GetCollection<ReadModel.PaginaDeDetalhesDoConteudo.PaginaDeDetalhesDoConteudo>("PaginaInicial_EventStream");
            paginaInicialEventosCollection.CreateIndex("ReadModelIdentity", "EventUtcDate");
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using EDA.Poc.Pagamentos.Consultas.PaginaDeDetalhesDoConteudo;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EDA.Poc.Pagamentos.Consultas.MongoDb.PaginaDeDetalhesDoConteudo
{
    public class ServicoPaginaDeDetalhesDoConteudo : IServicoPaginaDeDetalhesDoConteudo
    {
        private const string Database = "MarketPlaceReadModelStore";
        private const string Collection = "PaginaDeDetalhesDoConteudo";
        private readonly MongoClient _mongoClient;

        public ServicoPaginaDeDetalhesDoConteudo(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public Task<ReadModel.PaginaDeDetalhesDoConteudo.PaginaDeDetalhesDoConteudo> ObterAPaginaDeDetalhesDoConteudo(Guid idDoConteudo)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(Database);

                                    var collection = database.GetCollection<ReadModel.PaginaDeDetalhesDoConteudo.PaginaDeDetalhesDoConteudo>(Collection);

                                    return collection.AsQueryable().OrderByDescending(x => x.Version).FirstOrDefault(x => x.IdDoConteudo == idDoConteudo);
                                });
        }
    }
}

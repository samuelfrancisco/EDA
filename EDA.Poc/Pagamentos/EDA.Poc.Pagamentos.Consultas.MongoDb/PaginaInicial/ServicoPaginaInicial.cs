using System.Linq;
using System.Threading.Tasks;
using EDA.Poc.Pagamentos.Consultas.PaginaInicial;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EDA.Poc.Pagamentos.Consultas.MongoDb.PaginaInicial
{
    public class ServicoPaginaInicial : IServicoPaginaInicial
    {
        private const string Database = "MarketPlaceReadModelStore";
        private const string Collection = "PaginaInicial";
        private readonly MongoClient _mongoClient;

        public ServicoPaginaInicial(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public Task<ReadModel.PaginaInicial.PaginaInicial> ObterAPaginaInicial()
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(Database);

                                    var collection = database.GetCollection<ReadModel.PaginaInicial.PaginaInicial>(Collection);

                                    return collection.AsQueryable().OrderByDescending(x => x.Version).First();
                                });
        }
    }
}

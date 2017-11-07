using System.Threading.Tasks;
using EDA.Poc.Pagamentos.Consultas.PaginaDeLicencas;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq;

namespace EDA.Poc.Pagamentos.Consultas.MongoDb.PaginaDeLicencas
{
    public class ServicoPaginaDeLicencas : IServicoPaginaDeLicencas
    {
        private const string Database = "MarketPlaceReadModelStore";
        private const string Collection = "PaginaDeLicencas";
        private readonly MongoClient _mongoClient;

        public ServicoPaginaDeLicencas(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public Task<ReadModel.PaginaDeLicencas.PaginaDeLicencas> ObterAPaginaDeLicencasDoCliente(Guid idDoCliente)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(Database);

                                    var collection = database.GetCollection<ReadModel.PaginaDeLicencas.PaginaDeLicencas>(Collection);

                                    return collection.AsQueryable().OrderByDescending(x => x.Version).FirstOrDefault(x => x.IdDoCliente == idDoCliente && x.IdDaEmpresa == null && x.IdDoUsuario == null);
                                });
        }

        public Task<ReadModel.PaginaDeLicencas.PaginaDeLicencas> ObterAPaginaDeLicencasDaEmpresa(Guid idDaEmpresa)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(Database);

                                    var collection = database.GetCollection<ReadModel.PaginaDeLicencas.PaginaDeLicencas>(Collection);

                                    return collection.AsQueryable().OrderByDescending(x => x.Version).FirstOrDefault(x => x.IdDaEmpresa == idDaEmpresa && x.IdDoUsuario == null);
                                });
        }

        public Task<ReadModel.PaginaDeLicencas.PaginaDeLicencas> ObterAPaginaDeLicencasDoUsuario(Guid idDoUsuario)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(Database);

                                    var collection = database.GetCollection<ReadModel.PaginaDeLicencas.PaginaDeLicencas>(Collection);

                                    return collection.AsQueryable().OrderByDescending(x => x.Version).FirstOrDefault(x => x.IdDoUsuario == idDoUsuario);
                                });
        }
    }
}

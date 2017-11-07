using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDA.Poc.Pagamentos.Consultas.WebAPI;
using EDA.Poc.Pagamentos.ReadModel.WebAPI;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EDA.Poc.Pagamentos.Consultas.MongoDb.WebAPI
{
    public class ServicoLicencasAdquiridas : IServicoLicencasAdquiridas
    {
        private const string Database = "MarketPlaceReadModelStore";
        private const string Collection = "Licencas";
        private readonly MongoClient _mongoClient;

        public ServicoLicencasAdquiridas(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public Task<List<Licenca>> ObterLicencas(Guid idDoCliente, string siglaDoIdioma)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(Database);

                                    var collection = database.GetCollection<Licenca>(Collection);

                                    return collection.AsQueryable().Where(x => x.IdDoCliente == idDoCliente && x.SiglaDoIdioma == siglaDoIdioma).ToList();
                                });
        }

        public Task<Licenca> ObterLicencaPeloId(Guid idDaLicenca)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(Database);

                                    var collection = database.GetCollection<Licenca>(Collection);

                                    return collection.AsQueryable().SingleOrDefault(x => x.IdDaLicenca == idDaLicenca);
                                });
        }

        public Task Adicionar(Licenca licenca)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(Database);

                                    var collection = database.GetCollection<Licenca>(Collection);

                                    collection.Insert(licenca);
                                });
        }
    }
}

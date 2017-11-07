using System.Threading.Tasks;
using EDA.Poc.Pagamentos.Consultas.Usuarios;
using EDA.Poc.Pagamentos.ReadModel.Usuarios;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq;

namespace EDA.Poc.Pagamentos.Consultas.MongoDb.Usuarios
{
    public class ServicoUsuario : IServicoUsuario
    {
        private const string Database = "MarketPlaceReadModelStore";
        private const string Collection = "Usuarios";
        private readonly MongoClient _mongoClient;

        public ServicoUsuario(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public Task<Usuario> ObterUsuarioPorId(Guid id)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(Database);

                                    var collection = database.GetCollection<Usuario>(Collection);

                                    return collection.AsQueryable().SingleOrDefault(x => x.Id == id);
                                });
        }
    }
}

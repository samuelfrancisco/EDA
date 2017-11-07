using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDA.Poc.Pagamentos.Consultas.IntegracaoTemporaria;
using EDA.Poc.Pagamentos.ReadModel.IntegracaoTemporaria;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EDA.Poc.Pagamentos.Consultas.MongoDb.IntegracaoTemporaria
{
    public class ServicoConteudosCadastrados : IServicoConteudosCadastrados
    {
        private const string Database = "MarketPlaceReadModelStore";
        private const string Collection = "PaginaDeDetalhesDoConteudo";
        private readonly MongoClient _mongoClient;

        public ServicoConteudosCadastrados(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public Task<List<ConteudoCadastrado>> ObterTodosOsConteudosCadastrados()
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(Database);

                                    var collection = database.GetCollection<ReadModel.PaginaDeDetalhesDoConteudo.PaginaDeDetalhesDoConteudo>(Collection);

                                    var conteudos = collection.AsQueryable()
                                        .Select(x => new ConteudoCadastrado
                                                         {
                                                             IdDoConteudo = x.IdDoConteudo,
                                                             PrecoIlimitado = x.Precos.PrecoIlimitado,
                                                             FaixaDePrecos = x.Precos.PrecosPorQuantidadeDeUsuarios
                                                                 .Select(y => new ConteudoCadastradoFaixaDePreco
                                                                                  {
                                                                                      Preco = y.Preco,
                                                                                      QuantidadeInicialDeLicencas = y.LicencasDe,
                                                                                      QuantidadeFinalDeLicencas = y.LicencasAte
                                                                                  })
                                                         })
                                        .ToList();

                                    return conteudos;
                                });
        }
    }
}

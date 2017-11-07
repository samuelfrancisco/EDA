using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDA.Poc.Pagamentos.Consultas.PaginaDeLicencas;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EDA.Poc.Pagamentos.Consultas.MongoDb.PaginaDeLicencas
{
    public class ServicoDetalhesDaLicenca : IServicoDetalhesDaLicenca
    {
        private const string Database = "MarketPlaceReadModelStore";
        private const string Collection = "DetalhesDaLicenca";
        private readonly MongoClient _mongoClient;

        public ServicoDetalhesDaLicenca(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public Task<List<ReadModel.PaginaDeLicencas.DetalhesDaLicenca>> ObterOsDetalhesDasLicencasDoCliente(Guid idDoCliente)
        {
            return ObterOsDetalhesDasLicencasDoCliente(new DetalhesDaLicencaFiltros { IdDoCliente = idDoCliente });
        }

        public Task<List<ReadModel.PaginaDeLicencas.DetalhesDaLicenca>> ObterOsDetalhesDasLicencasDoCliente(DetalhesDaLicencaFiltros filtros)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(Database);

                                    var collection = database.GetCollection<ReadModel.PaginaDeLicencas.DetalhesDaLicenca>(Collection);

                                    var detalhesDasLicencas = collection.AsQueryable().Where(x => x.IdDoCliente == filtros.IdDoCliente);

                                    if (filtros.RenovacaoAutomatica.HasValue)
                                        detalhesDasLicencas = detalhesDasLicencas.Where(x => x.RenovacaoAutomatica == filtros.RenovacaoAutomatica);

                                    if (filtros.DataDeInicioDeUtilizacao.HasValue)
                                        detalhesDasLicencas = detalhesDasLicencas.Where(x => x.FimDaUtilizacao == null || x.FimDaUtilizacao >= filtros.DataDeInicioDeUtilizacao);

                                    if (filtros.DataDeFimDeUtilizacao.HasValue)
                                        detalhesDasLicencas = detalhesDasLicencas.Where(x => x.InicioDaUtilizacao <= filtros.DataDeFimDeUtilizacao);

                                    if (!string.IsNullOrWhiteSpace(filtros.PalavraChave))
                                        detalhesDasLicencas = detalhesDasLicencas.Where(x => x.DetalhesDoConteudo.Any(y => y.TituloDoConteudo.ToLower().Contains(filtros.PalavraChave.ToLower())));

                                    return detalhesDasLicencas.ToList();
                                });
        }

        public Task<List<ReadModel.PaginaDeLicencas.DetalhesDaLicenca>> ObterOsDetalhesDasLicencasDaEmpresa(Guid idDaEmpresa)
        {
            return ObterOsDetalhesDasLicencasDaEmpresa(new DetalhesDaLicencaFiltros { IdDaEmpresa = idDaEmpresa });
        }

        public Task<List<ReadModel.PaginaDeLicencas.DetalhesDaLicenca>> ObterOsDetalhesDasLicencasDaEmpresa(DetalhesDaLicencaFiltros filtros)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(Database);

                                    var collection = database.GetCollection<ReadModel.PaginaDeLicencas.DetalhesDaLicenca>(Collection);

                                    var detalhesDasLicencas = collection.AsQueryable().Where(x => x.IdDaEmpresa == filtros.IdDaEmpresa);

                                    if (filtros.RenovacaoAutomatica.HasValue)
                                        detalhesDasLicencas = detalhesDasLicencas.Where(x => x.RenovacaoAutomatica == filtros.RenovacaoAutomatica);

                                    if (filtros.DataDeInicioDeUtilizacao.HasValue)
                                        detalhesDasLicencas = detalhesDasLicencas.Where(x => x.FimDaUtilizacao == null || x.FimDaUtilizacao >= filtros.DataDeInicioDeUtilizacao);

                                    if (filtros.DataDeFimDeUtilizacao.HasValue)
                                        detalhesDasLicencas = detalhesDasLicencas.Where(x => x.InicioDaUtilizacao <= filtros.DataDeFimDeUtilizacao);

                                    if (!string.IsNullOrWhiteSpace(filtros.PalavraChave))
                                        detalhesDasLicencas = detalhesDasLicencas.Where(x => x.DetalhesDoConteudo.Any(y => y.TituloDoConteudo.ToLower().Contains(filtros.PalavraChave.ToLower())));

                                    return detalhesDasLicencas.ToList();
                                });
        }

        public Task<List<ReadModel.PaginaDeLicencas.DetalhesDaLicenca>> ObterOsDetalhesDasLicencasDoUsuario(Guid idDoUsuario)
        {
            return ObterOsDetalhesDasLicencasDoUsuario(new DetalhesDaLicencaFiltros { IdDoUsuario = idDoUsuario });
        }

        public Task<List<ReadModel.PaginaDeLicencas.DetalhesDaLicenca>> ObterOsDetalhesDasLicencasDoUsuario(DetalhesDaLicencaFiltros filtros)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(Database);

                                    var collection = database.GetCollection<ReadModel.PaginaDeLicencas.DetalhesDaLicenca>(Collection);

                                    var detalhesDasLicencas = collection.AsQueryable().Where(x => x.IdDoUsuario == filtros.IdDoUsuario);

                                    if (filtros.RenovacaoAutomatica.HasValue)
                                        detalhesDasLicencas = detalhesDasLicencas.Where(x => x.RenovacaoAutomatica == filtros.RenovacaoAutomatica);

                                    if (filtros.DataDeInicioDeUtilizacao.HasValue)
                                        detalhesDasLicencas = detalhesDasLicencas.Where(x => x.FimDaUtilizacao == null || x.FimDaUtilizacao >= filtros.DataDeInicioDeUtilizacao);

                                    if (filtros.DataDeFimDeUtilizacao.HasValue)
                                        detalhesDasLicencas = detalhesDasLicencas.Where(x => x.InicioDaUtilizacao <= filtros.DataDeFimDeUtilizacao);

                                    if (!string.IsNullOrWhiteSpace(filtros.PalavraChave))
                                        detalhesDasLicencas = detalhesDasLicencas.Where(x => x.DetalhesDoConteudo.Any(y => y.TituloDoConteudo.ToLower().Contains(filtros.PalavraChave.ToLower())));

                                    return detalhesDasLicencas.ToList();
                                });
        }

        public Task Adicionar(ReadModel.PaginaDeLicencas.DetalhesDaLicenca detalhesDaLicenca)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(Database);

                                    var collection = database.GetCollection<ReadModel.PaginaDeLicencas.DetalhesDaLicenca>(Collection);

                                    collection.Insert(detalhesDaLicenca);
                                });
        }
    }
}

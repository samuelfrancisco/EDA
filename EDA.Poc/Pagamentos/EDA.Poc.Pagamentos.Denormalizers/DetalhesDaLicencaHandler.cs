using System.Linq;
using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.Messaging.Handling;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Pagamentos.Consultas.PaginaDeDetalhesDoConteudo;
using EDA.Poc.Pagamentos.Consultas.Usuarios;
using EDA.Poc.Pagamentos.Events;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeLicencas;
using MassTransit;
using EDA.Poc.Pagamentos.Consultas.PaginaDeLicencas;

namespace EDA.Poc.Pagamentos.Denormalizers
{
    public class DetalhesDaLicencaHandler : EventHandler,
        IEventHandler<LicencaDeConteudoRegistradaParaCliente>
    {
        private readonly IServicoDetalhesDaLicenca _servicoDetalhesDaLicenca;
        private readonly IServicoUsuario _servicoUsuario;
        private readonly IServicoPaginaDeDetalhesDoConteudo _servicoPaginaDeDetalhesDoConteudo;

        public DetalhesDaLicencaHandler(IEventHandlerRepository eventHandlerRepository,
            IServicoPaginaDeDetalhesDoConteudo servicoPaginaDeDetalhesDoConteudo,
            IServicoDetalhesDaLicenca servicoDetalhesDaLicenca,
            IServicoUsuario servicoUsuario)
            : base(eventHandlerRepository)
        {
            _servicoPaginaDeDetalhesDoConteudo = servicoPaginaDeDetalhesDoConteudo;
            _servicoDetalhesDaLicenca = servicoDetalhesDaLicenca;
            _servicoUsuario = servicoUsuario;
        }

        public Task Consume(ConsumeContext<LicencaDeConteudoRegistradaParaCliente> context)
        {
            return ExecuteConsume(context.Message,
                                  async evento =>
                                  {
                                      //TODO substituir por uma consulta
                                      var idDaEmpresa = System.Guid.Parse("01a2a426-5c95-4c0e-9e22-eb353fc7fa8a");

                                      var paginaDeDetalhesDoConteudo = await _servicoPaginaDeDetalhesDoConteudo
                                                                                 .ObterAPaginaDeDetalhesDoConteudo(evento.IdDoConteudo)
                                                                                 .ConfigureAwait(false);

                                      var usuario = await _servicoUsuario.ObterUsuarioPorId(evento.IdDoUsuario)
                                                              .ConfigureAwait(false);

                                      var detalhesDaLicenca = new DetalhesDaLicenca
                                                                  {
                                                                      RenovacaoAutomatica = !evento.QuantidadeDeMeses.HasValue,
                                                                      AdquiridaEm = evento.DataDaCompra,
                                                                      InicioDaUtilizacao = evento.DisponibilizarAPartirDe,
                                                                      FimDaUtilizacao = evento.QuantidadeDeMeses.HasValue
                                                                                            ? evento.DisponibilizarAPartirDe
                                                                                                  .AddMonths(evento.QuantidadeDeMeses.Value)
                                                                                            : (System.DateTime?)null,
                                                                      QuantidadeDeUsuarios = evento.QuantidadeDeUsuarios,
                                                                      Custo = evento.ValorMensalDaCompra,
                                                                      IdDaLicenca = evento.IdDaLicenca,
                                                                      IdDoCliente = evento.IdDoCliente,
                                                                      IdDoConteudo = evento.IdDoConteudo,
                                                                      IdDoUsuario = evento.IdDoUsuario,
                                                                      NomeDoUsuario = usuario.Nome,
                                                                      IdDaEmpresa = idDaEmpresa,
                                                                      DetalhesDoConteudo = paginaDeDetalhesDoConteudo.Detalhes
                                                                          .Select(x => new DetalhesDoConteudo
                                                                                           {
                                                                                               SiglaDoIdioma = x.SiglaDoIdioma,
                                                                                               TituloDoConteudo = x.Titulo
                                                                                           }).ToList()
                                                                  };

                                      await _servicoDetalhesDaLicenca.Adicionar(detalhesDaLicenca).ConfigureAwait(false);
                                  });
        }
    }
}

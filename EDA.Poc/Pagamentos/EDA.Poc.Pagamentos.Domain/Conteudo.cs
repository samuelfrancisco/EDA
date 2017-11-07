using System;
using System.Collections.Generic;
using System.Linq;
using EDA.Poc.Infraestrutura.EventSourcing;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Pagamentos.Commands;
using EDA.Poc.Pagamentos.Domain.Enums;
using EDA.Poc.Pagamentos.Events;

namespace EDA.Poc.Pagamentos.Domain
{
    public class Conteudo : EventSourced
    {
        private decimal _precoSemLimiteDeUsuarios;
        private List<ConteudoFaixaDePreco> _faixasDePreco;
        private List<ConteudoPromocao> _promocoes;
        private readonly List<ConteudoAvaliacao> _avaliacoes;
        private readonly Dictionary<int, int> _quantidadeDeAvaliacoesPorValorDeAvaliacao;

        public Conteudo(Guid id, decimal precoSemLimitesDeUsuarios, IEnumerable<ConteudoFaixaDePreco> faixasDePrecos, IEnumerable<ConteudoPromocao> promocoes)
            : base(id)
        {
            _faixasDePreco = new List<ConteudoFaixaDePreco>();
            _promocoes = new List<ConteudoPromocao>();
            _avaliacoes = new List<ConteudoAvaliacao>();
            _quantidadeDeAvaliacoesPorValorDeAvaliacao = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 } };

            var evento = new ConteudoCadastrado
            {
                IdDoConteudo = id,
                PrecoSemLimitesDeUsuarios = precoSemLimitesDeUsuarios,
                FaixaDePrecos = faixasDePrecos
                                     .Select(x => new ConteudoCadastradoFaixaDePreco
                                     {
                                         Preco = x.Preco,
                                         QuantidadeInicialDeLicencas = x.QuantidadeInicialDeLicencas,
                                         QuantidadeFinalDeLicencas = x.QuantidadeFinalDeLicencas
                                     }).ToList(),
                Promocoes = promocoes
                                     .Select(x => new ConteudoCadastradoPromocao
                                     {
                                         IdDaPromocao = x.IdDaPromocao,
                                         TipoDeDesconto = x.TipoDeDesconto,
                                         ValorDoDesconto = x.ValorDoDesconto,
                                         Inicio = x.Inicio,
                                         Fim = x.Fim
                                     }).ToList()
            };

            Update(evento);
        }

        public Conteudo(Guid id, IEnumerable<IVersionedEvent> pastEvents)
            : base(id)
        {
            _faixasDePreco = new List<ConteudoFaixaDePreco>();
            _promocoes = new List<ConteudoPromocao>();
            _avaliacoes = new List<ConteudoAvaliacao>();
            _quantidadeDeAvaliacoesPorValorDeAvaliacao = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 } };

            LoadFromHistory(pastEvents);
        }

        public Conteudo(Guid id, IMemento<Conteudo> memento, IEnumerable<IVersionedEvent> pastEvents)
            : base(id)
        {
            Version = memento.Version;

            var conteudoMemento = (ConteudoMemento)memento;

            _precoSemLimiteDeUsuarios = conteudoMemento.PrecoSemLimiteDeLicencas;
            _faixasDePreco = conteudoMemento.FaixasDePreco.ToList();
            _promocoes = conteudoMemento.Promocoes.ToList();
            _avaliacoes = conteudoMemento.Avaliacoes.ToList();
            _quantidadeDeAvaliacoesPorValorDeAvaliacao = new Dictionary<int, int>(conteudoMemento.QuantidadeDeAvaliacoesPorValorDeAvaliacao);

            LoadFromHistory(pastEvents);
        }

        public override IMemento<IEventSourced> GetMemento()
        {
            var memento = new ConteudoMemento
            {
                Id = Id,
                Version = Version,                
                PrecoSemLimiteDeLicencas = _precoSemLimiteDeUsuarios,
                FaixasDePreco = _faixasDePreco.ToList(),
                Promocoes = _promocoes.ToList(),
                Avaliacoes = _avaliacoes.ToList(),
                QuantidadeDeAvaliacoesPorValorDeAvaliacao = new Dictionary<int, int>(_quantidadeDeAvaliacoesPorValorDeAvaliacao)
            };

            return memento;
        }

        public OrdemDeCompra GerarOrdemDeCompra(ComprarConteudo comando)
        {
            var precoDoConteudo = ObterPrecoDoConteudoDeAcordoComOTipoDeLicenciamentoEQuantidadeDeUsuarios(comando.TipoDeLicenciamento, comando.QuantidadeDeUsuarios);

            var promocao = ObterPromocaoAtiva();

            var valorMensalDaCompra = CalcularValorMensalDaCompraDeAcordoComOTipoDeLicenciamentoEQuantidadeDeUsuarios(precoDoConteudo, comando.TipoDeLicenciamento,
                                                                                                          comando.QuantidadeDeUsuarios, promocao);

            var ordemDeCompra = new OrdemDeCompra(comando.IdDoUsuario, comando.IdDoCliente, comando.IdDoConteudo, comando.TipoDeLicenciamento,
                                                  comando.QuantidadeDeUsuarios, comando.DisponibilizarAPartirDe, comando.TipoDeValidade,
                                                  comando.QuantidadeDeMeses, comando.FormaDePagamento, precoDoConteudo,
                                                  valorMensalDaCompra,
                                                  promocao != null
                                                      ? new OrdemDeCompraPromocao
                                                      {
                                                          IdDaPromocao = promocao.IdDaPromocao,
                                                          TipoDeDesconto = promocao.TipoDeDesconto,
                                                          ValorDoDesconto = promocao.ValorDoDesconto
                                                      }
                                                      : null,
                                                  comando.Date,
                                                  comando.Id);

            return ordemDeCompra;
        }

        public void Avaliar(AvaliarConteudo comando)
        {
            var quantidadeDeAvaliacoesPorValorDeAvaliacao = CalcularQuantidadeDeAvaliacoesPorValorDeAvaliacao(comando.IdDoUsuario, comando.Avaliacao);

            var mediaDasAvaliacoes = CalcularNovaMediaDasAvaliacoes(quantidadeDeAvaliacoesPorValorDeAvaliacao);

            if (_avaliacoes.All(x => x.IdDoUsuario != comando.IdDoUsuario))
            {
                var evento = new ConteudoAvaliado
                                 {
                                     CorrelationId = comando.Id,
                                     OriginalCorrelationId = comando.OriginalCorrelationId ?? comando.Id,
                                     IdDoUsuario = comando.IdDoUsuario,
                                     IdDoConteudo = comando.IdDoConteudo,
                                     IdDoCliente = comando.IdDoCliente,
                                     Conteudo = comando.Conteudo,
                                     Avaliacao = comando.Avaliacao,
                                     MediaDasAvaliacoesDoConteudo = mediaDasAvaliacoes,
                                     QuantidadeDeAvaliacoesPorValorDeAvaliacao = quantidadeDeAvaliacoesPorValorDeAvaliacao
                                 };

                Update(evento);
            }
            else
            {
                var evento = new ConteudoReavaliado
                                 {
                                     CorrelationId = comando.Id,
                                     OriginalCorrelationId = comando.OriginalCorrelationId ?? comando.Id,
                                     IdDoUsuario = comando.IdDoUsuario,
                                     IdDoConteudo = comando.IdDoConteudo,
                                     IdDoCliente = comando.IdDoCliente,
                                     Conteudo = comando.Conteudo,
                                     Avaliacao = comando.Avaliacao,
                                     MediaDasAvaliacoesDoConteudo = mediaDasAvaliacoes,
                                     QuantidadeDeAvaliacoesPorValorDeAvaliacao = quantidadeDeAvaliacoesPorValorDeAvaliacao
                                 };

                Update(evento);
            }
        }

        protected override void SetHandlers()
        {
            Handles<ConteudoCadastrado>(When);
            Handles<ConteudoAvaliado>(When);
            Handles<ConteudoReavaliado>(When);
        }

        private void When(ConteudoCadastrado @event)
        {
            _precoSemLimiteDeUsuarios = @event.PrecoSemLimitesDeUsuarios;

            _faixasDePreco = @event.FaixaDePrecos
                .Select(x => new ConteudoFaixaDePreco
                {
                    Preco = x.Preco,
                    QuantidadeInicialDeLicencas = x.QuantidadeInicialDeLicencas,
                    QuantidadeFinalDeLicencas = x.QuantidadeFinalDeLicencas
                }).ToList();

            _promocoes = @event.Promocoes
                .Select(x => new ConteudoPromocao
                {
                    IdDaPromocao = x.IdDaPromocao,
                    TipoDeDesconto = x.TipoDeDesconto,
                    ValorDoDesconto = x.ValorDoDesconto,
                    Inicio = x.Inicio,
                    Fim = x.Fim
                }).ToList();
        }

        private void When(ConteudoAvaliado @event)
        {
            if (_avaliacoes.Any(x => x.IdDoUsuario == @event.IdDoUsuario))
                throw new InvalidOperationException("O conteúdo só pode ser avaliado uma única vez por cada usuário. Para alterar a avaliação é necessário reavaliar o conteúdo.");

            _avaliacoes.Add(new ConteudoAvaliacao
            {
                IdDoUsuario = @event.IdDoUsuario,
                IdDoConteudo = @event.IdDoConteudo,
                Avaliacao = @event.Avaliacao,
                IdDoCliente = @event.IdDoCliente
            });

            _quantidadeDeAvaliacoesPorValorDeAvaliacao[@event.Avaliacao]++;
        }

        private void When(ConteudoReavaliado @event)
        {
            var avaliacao = _avaliacoes.Single(x => x.IdDoUsuario == @event.IdDoUsuario);

            _avaliacoes.Remove(avaliacao);

            _avaliacoes.Add(new ConteudoAvaliacao
            {
                IdDoUsuario = @event.IdDoUsuario,
                IdDoConteudo = @event.IdDoConteudo,
                Avaliacao = @event.Avaliacao,
                IdDoCliente = @event.IdDoCliente
            });

            _quantidadeDeAvaliacoesPorValorDeAvaliacao[avaliacao.Avaliacao]--;

            _quantidadeDeAvaliacoesPorValorDeAvaliacao[@event.Avaliacao]++;
        }

        private decimal ObterPrecoDoConteudoDeAcordoComOTipoDeLicenciamentoEQuantidadeDeUsuarios(TipoDeLicenciamento tipoDeLicenciamento, int? quantidadeDeUsuarios)
        {
            switch (tipoDeLicenciamento)
            {
                case TipoDeLicenciamento.QuantidadeLimitadaDeUsuarios:
                    {
                        if (!quantidadeDeUsuarios.HasValue) throw new ArgumentNullException("quantidadeDeUsuarios");

                        return ObterPrecoDeAcordoComAQuantidadeDeUsuarios(quantidadeDeUsuarios.Value);
                    }
                case TipoDeLicenciamento.SemLimiteDeUsuarios:
                    return _precoSemLimiteDeUsuarios;
                default:
                    throw new ArgumentOutOfRangeException("tipoDeLicenciamento");
            }
        }

        private decimal ObterPrecoDeAcordoComAQuantidadeDeUsuarios(int quantidadeDeUsuarios)
        {
            var faixaDePreco = _faixasDePreco.OrderBy(x => x.QuantidadeInicialDeLicencas)
                .First(x => x.QuantidadeInicialDeLicencas <= quantidadeDeUsuarios
                            && (!x.QuantidadeFinalDeLicencas.HasValue || (x.QuantidadeFinalDeLicencas >= quantidadeDeUsuarios)));

            return faixaDePreco.Preco;
        }

        private ConteudoPromocao ObterPromocaoAtiva()
        {
            var promocaoAtiva = _promocoes.OrderBy(x => x.Inicio).FirstOrDefault(x => x.Inicio <= DateTime.Today && x.Fim >= DateTime.Today);

            return promocaoAtiva;
        }

        private decimal CalcularValorMensalDaCompraDeAcordoComOTipoDeLicenciamentoEQuantidadeDeUsuarios(decimal precoDoConteudo,
            TipoDeLicenciamento tipoDeLicenciamento, int? quantidadeDeUsuarios, ConteudoPromocao promocao)
        {
            switch (tipoDeLicenciamento)
            {
                case TipoDeLicenciamento.QuantidadeLimitadaDeUsuarios:
                    {
                        if (!quantidadeDeUsuarios.HasValue) throw new ArgumentNullException("quantidadeDeUsuarios");

                        var precoPorLicenca = promocao == null ? precoDoConteudo : CalcularValorComDesconto(precoDoConteudo, promocao);

                        return (precoPorLicenca * quantidadeDeUsuarios.Value);
                    }
                case TipoDeLicenciamento.SemLimiteDeUsuarios:
                    return promocao == null ? precoDoConteudo : CalcularValorComDesconto(precoDoConteudo, promocao);
                default:
                    throw new ArgumentOutOfRangeException("tipoDeLicenciamento");
            }
        }

        private decimal CalcularValorComDesconto(decimal preco, ConteudoPromocao promocao)
        {
            switch (promocao.TipoDeDesconto)
            {
                case TipoDeDesconto.Valor:
                    return (preco - promocao.ValorDoDesconto);
                case TipoDeDesconto.Percentual:
                    return (preco - ((preco / 100) * promocao.ValorDoDesconto));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private decimal CalcularNovaMediaDasAvaliacoes(Dictionary<int, int> quantidadeDeAvaliacoesPorValorDeAvaliacao)
        {
            decimal somaDosPesos = quantidadeDeAvaliacoesPorValorDeAvaliacao.Sum(x => x.Value);

            decimal somaDasAvaliacoesVezesOsPesos = quantidadeDeAvaliacoesPorValorDeAvaliacao.Sum(x => (x.Value * x.Key));

            var mediaPonderada = (somaDasAvaliacoesVezesOsPesos / somaDosPesos);

            return mediaPonderada;
        }

        private Dictionary<int, int> CalcularQuantidadeDeAvaliacoesPorValorDeAvaliacao(Guid idDoUsuario, int novaAvaliacao)
        {
            var minhaAvaliacao = _avaliacoes.SingleOrDefault(x => x.IdDoUsuario == idDoUsuario);

            var quantidadeDeAvaliacoesPorValorDeAvaliacao = _quantidadeDeAvaliacoesPorValorDeAvaliacao.ToDictionary(x => x.Key, x => x.Value);

            if (minhaAvaliacao != null)
            {
                quantidadeDeAvaliacoesPorValorDeAvaliacao[minhaAvaliacao.Avaliacao]--;
            }

            quantidadeDeAvaliacoesPorValorDeAvaliacao[novaAvaliacao]++;

            return quantidadeDeAvaliacoesPorValorDeAvaliacao;
        }
    }
}

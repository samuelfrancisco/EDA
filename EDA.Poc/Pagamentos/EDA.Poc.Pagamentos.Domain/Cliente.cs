using System;
using System.Collections.Generic;
using System.Linq;
using EDA.Poc.Infraestrutura.EventSourcing;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.Utils;
using EDA.Poc.Pagamentos.Commands;
using EDA.Poc.Pagamentos.Domain.Enums;
using EDA.Poc.Pagamentos.Events;

namespace EDA.Poc.Pagamentos.Domain
{
    public class Cliente : EventSourced
    {
        private readonly List<CobrancaDeCompra> _cobrancas;
        private readonly List<FaturaMensal> _faturasMensais;
        private readonly List<LicencaDeConteudo> _licencasDeConteudo;
        private readonly List<CobrancaRecorrente> _cobrancasRecorrentes;

        public Cliente(Guid id)
            : base(id)
        {
            var evento = new NovoClienteRegistradoNaLoja
            {
                IdDoCliente = id
            };

            Update(evento);
        }

        public Cliente(Guid id, IEnumerable<IVersionedEvent> pastEvents)
            : base(id)
        {
            _cobrancas = new List<CobrancaDeCompra>();
            _faturasMensais = new List<FaturaMensal>();
            _licencasDeConteudo = new List<LicencaDeConteudo>();
            _cobrancasRecorrentes = new List<CobrancaRecorrente>();

            LoadFromHistory(pastEvents);
        }

        public Cliente(Guid id, IMemento<Cliente> memento, IEnumerable<IVersionedEvent> pastEvents)
            : base(id)
        {
            var clienteMemento = (ClienteMemento)memento;

            Version = memento.Version;

            _cobrancas = clienteMemento.Cobrancas.ToList();
            _faturasMensais = clienteMemento.FaturasMensais.ToList();
            _licencasDeConteudo = clienteMemento.LicencasDeConteudo.ToList();
            _cobrancasRecorrentes = clienteMemento.CobrancasRecorrentes.ToList();

            LoadFromHistory(pastEvents);
        }

        public override IMemento<IEventSourced> GetMemento()
        {
            var memento = new ClienteMemento
            {
                Id = Id,
                Version = Version,
                Cobrancas = _cobrancas.ToList(),
                FaturasMensais = _faturasMensais.ToList(),
                LicencasDeConteudo = _licencasDeConteudo.ToList(),
                CobrancasRecorrentes = _cobrancasRecorrentes.ToList()
            };

            return memento;
        }

        public void GerarCombrancaDaCompra(GerarCobrancaDaCompraParaOCliente comando)
        {
            if (_cobrancas.Any(x => x.IdDaOrdemDeCompra == comando.IdDaOrdemDeCompra))
                throw new InvalidOperationException(string.Format("Já foi gerada uma cobrança para a ordem de compra {0}", comando.IdDaOrdemDeCompra));

            var idDaCobranca = Guid.NewGuid();

            var cobrancaDaCompraGeradaParaOCliente = new CobrancaDaCompraGeradaParaOCliente
            {
                CorrelationId = comando.Id,
                OriginalCorrelationId = comando.OriginalCorrelationId.HasValue ? comando.OriginalCorrelationId.Value : comando.Id,
                IdDaCobranca = idDaCobranca,
                IdDaOrdemDeCompra = comando.IdDaOrdemDeCompra,
                IdDoProcessoDeCompra = comando.IdDoProcessoDeCompra,
                IdDoUsuario = comando.IdDoUsuario,
                IdDoCliente = comando.IdDoCliente,
                IdDoConteudo = comando.IdDoConteudo,
                TipoDeLicenciamento = comando.TipoDeLicenciamento,
                QuantidadeDeUsuarios = comando.QuantidadeDeUsuarios,
                DisponibilizarAPartirDe = comando.DisponibilizarAPartirDe,
                TipoDeValidade = comando.TipoDeValidade,
                QuantidadeDeMeses = comando.QuantidadeDeMeses,
                FormaDePagamento = comando.FormaDePagamento,
                DataDaCompra = comando.DataDaCompra,
                ValorMensalDaCompra = comando.ValorMensalDaCompra
            };

            Update(cobrancaDaCompraGeradaParaOCliente);

            if (comando.FormaDePagamento == FormaDePagamento.CobrancaDireta)
            {
                if (comando.TipoDeValidade == TipoDeValidade.SemLimiteDeMeses)
                {
                    var dataDoDebito = comando.DisponibilizarAPartirDe.AddMonths(1);

                    var mesDaFatura = new Mes(dataDoDebito);

                    var cobrancaRecorrenteRegistrada = new CobrancaRecorrenteRegistrada
                    {
                        CorrelationId = comando.Id,
                        OriginalCorrelationId = comando.OriginalCorrelationId.HasValue ? comando.OriginalCorrelationId.Value : comando.Id,
                        IdDaCobrancaRecorrente = Guid.NewGuid(),
                        IdDaCobranca = idDaCobranca,
                        IdDaOrdemDeCompra = comando.IdDaOrdemDeCompra,
                        IdDoProcessoDeCompra = comando.IdDoProcessoDeCompra,
                        IdDoProcessoDeCobrancaRecorrente = Guid.NewGuid(),
                        IdDoUsuario = comando.IdDoUsuario,
                        IdDoCliente = comando.IdDoCliente,
                        IdDoConteudo = comando.IdDoConteudo,
                        DataDaCompra = comando.DataDaCompra,
                        ValorRecorrente = comando.ValorMensalDaCompra,
                        DataDaPrimeiraCobranca = dataDoDebito
                    };

                    Update(cobrancaRecorrenteRegistrada);                    

                    var debitoRegistradoNaFaturaDoCliente = new DebitoRegistradoNaFaturaDoCliente
                    {
                        CorrelationId = comando.Id,
                        OriginalCorrelationId = comando.OriginalCorrelationId.HasValue ? comando.OriginalCorrelationId.Value : comando.Id,
                        IdDoDebito = Guid.NewGuid(),
                        IdDaCobranca = idDaCobranca,
                        IdDaOrdemDeCompra = comando.IdDaOrdemDeCompra,
                        IdDoProcessoDeCompra = comando.IdDoProcessoDeCompra,
                        IdDoUsuario = comando.IdDoUsuario,
                        IdDoCliente = comando.IdDoCliente,
                        IdDoConteudo = comando.IdDoConteudo,
                        DataDaCompra = comando.DataDaCompra,
                        Valor = comando.ValorMensalDaCompra,
                        MesDaFatura = mesDaFatura
                    };

                    Update(debitoRegistradoNaFaturaDoCliente);                    
                }
                else
                {
                    for (int i = 0; i < comando.QuantidadeDeMeses; i++)
                    {
                        var dataDoDebito = comando.DisponibilizarAPartirDe.AddMonths(1 + i);

                        var mesDaFatura = new Mes(dataDoDebito);

                        var debitoRegistradoNaFaturaDoCliente = new DebitoRegistradoNaFaturaDoCliente
                        {
                            CorrelationId = comando.Id,
                            OriginalCorrelationId = comando.OriginalCorrelationId.HasValue ? comando.OriginalCorrelationId.Value : comando.Id,
                            IdDoDebito = Guid.NewGuid(),
                            IdDaCobranca = idDaCobranca,
                            IdDaOrdemDeCompra = comando.IdDaOrdemDeCompra,
                            IdDoProcessoDeCompra = comando.IdDoProcessoDeCompra,
                            IdDoUsuario = comando.IdDoUsuario,
                            IdDoCliente = comando.IdDoCliente,
                            IdDoConteudo = comando.IdDoConteudo,
                            DataDaCompra = comando.DataDaCompra,
                            Valor = comando.ValorMensalDaCompra,
                            MesDaFatura = mesDaFatura
                        };

                        Update(debitoRegistradoNaFaturaDoCliente);
                    }
                }
            }
        }

        public void RegistrarDebitoRecorrenteNaFatura(RegistrarDebitoRecorrenteNaFaturaDoCliente comando)
        {
            var faturaDoMes = _faturasMensais.FirstOrDefault(x => x.Mes == comando.MesDaFatura);

            if (faturaDoMes != null && faturaDoMes.Debitos.Any(x => x.IdDaOrdemDeCompra == comando.IdDaOrdemDeCompra))
                throw new InvalidOperationException(string.Format("Já foi registrado um débito para a ordem de compra {0} na fatura de {1} do cliente", comando.IdDaOrdemDeCompra, comando.MesDaFatura));

            var evento = new DebitoRecorrenteRegistradoNaFaturaDoCliente
            {
                CorrelationId = comando.Id,
                OriginalCorrelationId = comando.OriginalCorrelationId.HasValue ? comando.OriginalCorrelationId.Value : comando.Id,
                IdDoDebito = comando.IdDoDebito,
                IdDoProcessoDeCobrancaRecorrente = comando.IdDoProcessoDeCobrancaRecorrente,
                IdDaCobrancaRecorrente = comando.IdDaCobrancaRecorrente,
                IdDaCobranca = comando.IdDaCobranca,
                IdDaOrdemDeCompra = comando.IdDaOrdemDeCompra,                
                IdDoUsuario = comando.IdDoUsuario,
                IdDoCliente = comando.IdDoCliente,
                IdDoConteudo = comando.IdDoConteudo,
                DataDaCompra = comando.DataDaCompra,
                Valor = comando.Valor,
                MesDaFatura = comando.MesDaFatura
            };

            Update(evento);
        }

        public void RegistrarLicencaDeConteudo(RegistrarLicencaParaCliente comando)
        {
            if (_licencasDeConteudo.Any(x => x.IdDaOrdemDeCompra == comando.IdDaOrdemDeCompra))
                throw new InvalidOperationException(string.Format("Uma licença já foi registrada para a ordem de compra {0}", comando.IdDaOrdemDeCompra));

            var evento = new LicencaDeConteudoRegistradaParaCliente
            {
                CorrelationId = comando.Id,
                OriginalCorrelationId = comando.OriginalCorrelationId.HasValue ? comando.OriginalCorrelationId.Value : comando.Id,
                IdDaOrdemDeCompra = comando.IdDaOrdemDeCompra,
                IdDoProcessoDeCompra = comando.IdDoProcessoDeCompra,
                IdDaLicenca = Guid.NewGuid(),
                IdDoUsuario = comando.IdDoUsuario,
                IdDoCliente = comando.IdDoCliente,
                IdDoConteudo = comando.IdDoConteudo,
                TipoDeLicenciamento = comando.TipoDeLicenciamento,
                QuantidadeDeUsuarios = comando.QuantidadeDeUsuarios,
                DisponibilizarAPartirDe = comando.DisponibilizarAPartirDe,
                TipoDeValidade = comando.TipoDeValidade,
                QuantidadeDeMeses = comando.QuantidadeDeMeses,
                DataDaCompra = comando.DataDaCompra,
                ValorMensalDaCompra = comando.ValorMensalDaCompra,
            };

            Update(evento);
        }

        protected override void SetHandlers()
        {
            Handles<NovoClienteRegistradoNaLoja>(When);
            Handles<CobrancaDaCompraGeradaParaOCliente>(When);
            Handles<CobrancaRecorrenteRegistrada>(When);
            Handles<DebitoRegistradoNaFaturaDoCliente>(When);
            Handles<DebitoRecorrenteRegistradoNaFaturaDoCliente>(When);
            Handles<LicencaDeConteudoRegistradaParaCliente>(When);
        }

        private void When(NovoClienteRegistradoNaLoja @event)
        {

        }

        private void When(CobrancaDaCompraGeradaParaOCliente @event)
        {
            _cobrancas.Add(new CobrancaDeCompra(@event.IdDaCobranca, @event.IdDaOrdemDeCompra, @event.DataDaCompra, @event.ValorMensalDaCompra,
                                                @event.TipoDeValidade, @event.QuantidadeDeMeses));
        }

        private void When(CobrancaRecorrenteRegistrada @event)
        {
            _cobrancasRecorrentes.Add(new CobrancaRecorrente(@event.IdDaCobrancaRecorrente, @event.IdDaCobranca, @event.IdDaOrdemDeCompra, @event.IdDoUsuario,
                                                             @event.DataDaCompra, @event.ValorRecorrente));
        }

        private void When(DebitoRegistradoNaFaturaDoCliente @event)
        {
            var fatura = _faturasMensais.SingleOrDefault(x => x.Mes == @event.MesDaFatura);

            if (fatura == null)
            {
                _faturasMensais.Add(fatura = new FaturaMensal(@event.MesDaFatura));
            }

            fatura.RegistrarDebito(new FaturaMensalDebito(@event.IdDoDebito, @event.IdDaCobranca, @event.IdDaOrdemDeCompra, @event.IdDoUsuario,
                                                          @event.DataDaCompra, @event.Valor));
        }

        private void When(DebitoRecorrenteRegistradoNaFaturaDoCliente @event)
        {
            var fatura = _faturasMensais.SingleOrDefault(x => x.Mes == @event.MesDaFatura);

            if (fatura == null)
            {
                _faturasMensais.Add(fatura = new FaturaMensal(@event.MesDaFatura));
            }

            fatura.RegistrarDebito(new FaturaMensalDebito(@event.IdDoDebito, @event.IdDaCobranca, @event.IdDaOrdemDeCompra, @event.IdDoUsuario,
                                                          @event.DataDaCompra, @event.Valor));
        }

        private void When(LicencaDeConteudoRegistradaParaCliente @event)
        {
            var licenca = new LicencaDeConteudo(@event.IdDaLicenca, @event.IdDaOrdemDeCompra, @event.IdDoUsuario, @event.IdDoCliente, @event.IdDoConteudo,
                                                @event.TipoDeLicenciamento, @event.QuantidadeDeUsuarios, @event.DisponibilizarAPartirDe,
                                                @event.TipoDeValidade, @event.QuantidadeDeMeses, @event.DataDaCompra, @event.Date);

            _licencasDeConteudo.Add(licenca);
        }
    }
}
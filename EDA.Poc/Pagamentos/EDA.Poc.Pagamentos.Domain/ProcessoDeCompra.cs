using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.Processes;
using EDA.Poc.Pagamentos.Commands;
using EDA.Poc.Pagamentos.Domain.Enums;
using EDA.Poc.Pagamentos.Events;

namespace EDA.Poc.Pagamentos.Domain
{
    public class ProcessoDeCompra : EventSourcedProcessManager
    {
        private ProcessoDeCompraStatus _status;
        private Guid _idDaOrdemDeCompra;
        private DateTime _dataDaCompra;
        private DateTime _dataDeCriacaoDaOrdemDeCompra;
        private DateTime? _dataDeGeracaoDaCobrancaDaCompra;
        private DateTime? _dataDoRegistroDaLicenca;
        private DateTime? _dataDeEncerramentoDoProcesso;
        private Guid _idDoUsuario;
        private Guid _idDoCliente;
        private Guid _idDoConteudo;
        private TipoDeLicenciamento _tipoDeLicenciamento;
        private int? _quantidadeDeUsuarios;
        private DateTime _disponibilizarAPartirDe;
        private TipoDeValidade _tipoDeValidade;
        private int? _quantidadeDeMeses;
        private FormaDePagamento _formaDePagamento;
        private decimal _precoDoConteudo;
        private decimal _valorMensalDaCompra;
        private ProcessoDeCompraPromocao _promocao;

        public ProcessoDeCompra(Guid id)
            : base(id)
        {
            _status = ProcessoDeCompraStatus.NaoIniciado;
        }

        public ProcessoDeCompra(Guid id, IEnumerable<IVersionedEvent> pastEvents)
            : base(id)
        {
            LoadFromHistory(pastEvents);
        }

        public ProcessoDeCompra(Guid id, IMemento<ProcessoDeCompra> memento, IEnumerable<IVersionedEvent> pastEvents)
            : base(id)
        {
            var processoDeCompraMemento = (ProcessoDeCompraMemento) memento;

            Version = memento.Version;

            _idDaOrdemDeCompra = processoDeCompraMemento.IdDaOrdemDeCompra;
            _dataDaCompra = processoDeCompraMemento.DataDaCompra;
            _dataDeCriacaoDaOrdemDeCompra = processoDeCompraMemento.DataDeCriacaoDaOrdemDeCompra;
            _dataDeGeracaoDaCobrancaDaCompra = processoDeCompraMemento.DataDeGeracaoDaCobrancaDaCompra;
            _dataDoRegistroDaLicenca = processoDeCompraMemento.DataDoRegistroDaLicenca;
            _dataDeEncerramentoDoProcesso = processoDeCompraMemento.DataDeEncerramentoDoProcesso;
            _idDoUsuario = processoDeCompraMemento.IdDoUsuario;
            _idDoCliente = processoDeCompraMemento.IdDoCliente;
            _idDoConteudo = processoDeCompraMemento.IdDoConteudo;
            _tipoDeLicenciamento = processoDeCompraMemento.TipoDeLicenciamento;
            _quantidadeDeUsuarios = processoDeCompraMemento.QuantidadeDeUsuarios;
            _disponibilizarAPartirDe = processoDeCompraMemento.DisponibilizarAPartirDe;
            _tipoDeValidade = processoDeCompraMemento.TipoDeValidade;
            _quantidadeDeMeses = processoDeCompraMemento.QuantidadeDeMeses;
            _formaDePagamento = processoDeCompraMemento.FormaDePagamento;
            _precoDoConteudo = processoDeCompraMemento.PrecoDoConteudo;
            _valorMensalDaCompra = processoDeCompraMemento.ValorMensalDaCompra;
            _promocao = processoDeCompraMemento.Promocao;
            _status = processoDeCompraMemento.Status;

            LoadFromHistory(pastEvents);
        }

        public override IMemento<IEventSourced> GetMemento()
        {
            var memento = new ProcessoDeCompraMemento
                              {
                                  Id = Id,
                                  Version = Version,                                  
                                  IdDaOrdemDeCompra = _idDaOrdemDeCompra,
                                  DataDaCompra = _dataDaCompra,
                                  DataDeCriacaoDaOrdemDeCompra = _dataDeCriacaoDaOrdemDeCompra,
                                  DataDeGeracaoDaCobrancaDaCompra = _dataDeGeracaoDaCobrancaDaCompra,
                                  DataDoRegistroDaLicenca = _dataDoRegistroDaLicenca,
                                  DataDeEncerramentoDoProcesso = _dataDeEncerramentoDoProcesso,
                                  IdDoUsuario = _idDoUsuario,
                                  IdDoCliente = _idDoCliente,
                                  IdDoConteudo = _idDoConteudo,
                                  TipoDeLicenciamento = _tipoDeLicenciamento,
                                  QuantidadeDeUsuarios = _quantidadeDeUsuarios,
                                  DisponibilizarAPartirDe = _disponibilizarAPartirDe,
                                  TipoDeValidade = _tipoDeValidade,
                                  QuantidadeDeMeses = _quantidadeDeMeses,
                                  FormaDePagamento = _formaDePagamento,
                                  PrecoDoConteudo = _precoDoConteudo,
                                  ValorMensalDaCompra = _valorMensalDaCompra,
                                  Promocao = _promocao,
                                  Status = _status
                              };

            return memento;
        }

        public void NaCriacaoDaOrdemDeCompra(OrdemDeCompraCriada evento)
        {
            if (_status != ProcessoDeCompraStatus.NaoIniciado) throw new InvalidOperationException("O processo de compra não pode ser iniciado novamente.");

            var comando = new GerarCobrancaDaCompraParaOCliente
                              {
                                  CorrelationId = evento.Id,
                                  OriginalCorrelationId = evento.OriginalCorrelationId.HasValue ? evento.OriginalCorrelationId.Value : evento.Id,
                                  IdDaOrdemDeCompra = evento.SourceId,
                                  IdDoProcessoDeCompra = evento.IdDoProcessoDeCompra,
                                  IdDoUsuario = evento.IdDoUsuario,
                                  IdDoCliente = evento.IdDoCliente,
                                  IdDoConteudo = evento.IdDoConteudo,
                                  FormaDePagamento = evento.FormaDePagamento,
                                  TipoDeLicenciamento = evento.TipoDeLicenciamento,
                                  QuantidadeDeUsuarios = evento.QuantidadeDeUsuarios,
                                  DisponibilizarAPartirDe = evento.DisponibilizarAPartirDe,
                                  TipoDeValidade = evento.TipoDeValidade,
                                  QuantidadeDeMeses = evento.QuantidadeDeMeses,
                                  ValorMensalDaCompra = evento.ValorMensalDaCompra,
                                  DataDaCompra = evento.DataDaCompra,
                              };

            AddCommand(comando);

            var eventoInterno = new ProcessoDeCompraIniciado
                                    {
                                        CorrelationId = evento.Id,
                                        OriginalCorrelationId = evento.OriginalCorrelationId.HasValue ? evento.OriginalCorrelationId.Value : evento.Id,
                                        IdDaOrdemDeCompra = evento.SourceId,
                                        IdDoUsuario = evento.IdDoUsuario,
                                        IdDoCliente = evento.IdDoCliente,
                                        IdDoConteudo = evento.IdDoConteudo,
                                        TipoDeLicenciamento = evento.TipoDeLicenciamento,
                                        QuantidadeDeUsuarios = evento.QuantidadeDeUsuarios,
                                        DisponibilizarAPartirDe = evento.DisponibilizarAPartirDe,
                                        TipoDeValidade = evento.TipoDeValidade,
                                        QuantidadeDeMeses = evento.QuantidadeDeMeses,
                                        FormaDePagamento = evento.FormaDePagamento,
                                        PrecoDoConteudo = evento.PrecoDoConteudo,
                                        ValorMensalDaCompra = evento.ValorMensalDaCompra,
                                        DataDeCriacaoDaOrdemDeCompra = evento.Date,
                                        DataDaCompra = evento.DataDaCompra,
                                    };

            if (evento.Promocao != null)
                eventoInterno.Promocao = new ProcessoDeCompraIniciadoPromocao
                                             {
                                                 IdDaPromocao = evento.Promocao.IdDaPromocao,
                                                 TipoDeDesconto = evento.Promocao.TipoDeDesconto,
                                                 ValorDoDesconto = evento.Promocao.ValorDoDesconto
                                             };

            Update(eventoInterno);
        }

        public void NaGeracaoDaCobrancaDaCompra(CobrancaDaCompraGeradaParaOCliente evento)
        {
            if (_status != ProcessoDeCompraStatus.AguardandoGeracaoCobrancaDaCompraParaOCliente) throw new InvalidOperationException("Operação inválida nessa fase do processo.");

            if (_formaDePagamento == FormaDePagamento.CobrancaDireta)
            {
                var comando = new RegistrarLicencaParaCliente
                                  {
                                      CorrelationId = evento.Id,
                                      OriginalCorrelationId = evento.OriginalCorrelationId.HasValue ? evento.OriginalCorrelationId.Value : evento.Id,
                                      IdDaOrdemDeCompra = _idDaOrdemDeCompra,
                                      IdDoProcessoDeCompra = Id,
                                      IdDoUsuario = _idDoUsuario,
                                      IdDoCliente = _idDoCliente,
                                      IdDoConteudo = _idDoConteudo,
                                      TipoDeLicenciamento = _tipoDeLicenciamento,
                                      QuantidadeDeUsuarios = _quantidadeDeUsuarios,
                                      DisponibilizarAPartirDe = _disponibilizarAPartirDe,
                                      TipoDeValidade = _tipoDeValidade,
                                      QuantidadeDeMeses = _quantidadeDeMeses,
                                      DataDaCompra = _dataDaCompra,
                                      ValorMensalDaCompra = _valorMensalDaCompra
                                  };

                AddCommand(comando);
            }

            var eventoInterno = new ProcessoDeCompraAlteradoParaAguardandoRegistroDaLicenca
                                    {
                                        CorrelationId = evento.Id,
                                        OriginalCorrelationId = evento.OriginalCorrelationId.HasValue ? evento.OriginalCorrelationId.Value : evento.Id,
                                        DataDeGeracaoDaCobrancaDaCompra = evento.Date
                                    };

            Update(eventoInterno);
        }

        public void NoRegistroDaLicenca(LicencaDeConteudoRegistradaParaCliente evento)
        {
            if(_status != ProcessoDeCompraStatus.AguardandoRegistroDaLicencaParaOCliente) throw new InvalidOperationException("Operação inválida nessa fase do processo.");

            var comando = new ConcluirOrdemDeCompra
                              {
                                  CorrelationId = evento.Id,
                                  OriginalCorrelationId = evento.OriginalCorrelationId.HasValue ? evento.OriginalCorrelationId.Value : evento.Id,
                                  IdDaOrdemDeCompra = evento.IdDaOrdemDeCompra
                              };

            AddCommand(comando);

            var eventoInterno = new ProcessoDeCompraFinalizado
                                    {
                                        CorrelationId = evento.Id,
                                        OriginalCorrelationId = evento.OriginalCorrelationId.HasValue ? evento.OriginalCorrelationId.Value : evento.Id,
                                        DataDoRegistroDaLicenca = evento.Date
                                    };
            Update(eventoInterno);
        }

        protected override void SetHandlers()
        {
            Handles<ProcessoDeCompraIniciado>(When);
            Handles<ProcessoDeCompraAlteradoParaAguardandoRegistroDaLicenca>(When);
            Handles<ProcessoDeCompraFinalizado>(When);
        }

        private void When(ProcessoDeCompraIniciado @event)
        {
            _idDaOrdemDeCompra = @event.IdDaOrdemDeCompra;
            _dataDaCompra = @event.DataDaCompra;
            _dataDeCriacaoDaOrdemDeCompra = @event.DataDeCriacaoDaOrdemDeCompra;
            _idDoUsuario = @event.IdDoUsuario;
            _idDoCliente = @event.IdDoCliente;
            _idDoConteudo = @event.IdDoConteudo;
            _tipoDeLicenciamento = @event.TipoDeLicenciamento;
            _quantidadeDeUsuarios = @event.QuantidadeDeUsuarios;
            _disponibilizarAPartirDe = @event.DisponibilizarAPartirDe;
            _tipoDeValidade = @event.TipoDeValidade;
            _quantidadeDeMeses = @event.QuantidadeDeMeses;
            _formaDePagamento = @event.FormaDePagamento;
            _precoDoConteudo = @event.PrecoDoConteudo;
            _valorMensalDaCompra = @event.ValorMensalDaCompra;

            _status = ProcessoDeCompraStatus.AguardandoGeracaoCobrancaDaCompraParaOCliente;

            if (@event.Promocao != null)
            {
                _promocao = new ProcessoDeCompraPromocao
                                {
                                    IdDaPromocao = @event.Promocao.IdDaPromocao,
                                    TipoDeDesconto = @event.Promocao.TipoDeDesconto,
                                    ValorDoDesconto = @event.Promocao.ValorDoDesconto
                                };
            }
        }

        private void When(ProcessoDeCompraAlteradoParaAguardandoRegistroDaLicenca @event)
        {
            _dataDeGeracaoDaCobrancaDaCompra = @event.DataDeGeracaoDaCobrancaDaCompra;
            
            _status = ProcessoDeCompraStatus.AguardandoRegistroDaLicencaParaOCliente;
        }

        private void When(ProcessoDeCompraFinalizado @event)
        {
            _dataDoRegistroDaLicenca = @event.DataDoRegistroDaLicenca;
            _dataDeEncerramentoDoProcesso = @event.Date;

            _status = ProcessoDeCompraStatus.LicensaRegistradaParaOCliente;
        }
    }
}

using EDA.Poc.Infraestrutura.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Pagamentos.Domain.Enums;
using EDA.Poc.Pagamentos.Events;
using EDA.Poc.Infraestrutura.Utils;
using EDA.Poc.Pagamentos.Commands;

namespace EDA.Poc.Pagamentos.Domain
{
    public class ProcessoDeCobrancaRecorrente : EventSourcedProcessManager
    {
        private ProcessoDeCobrancaRecorrenteStatus _status;
        private Guid _idDaCobrancaRecorrente;
        private Guid _idDaCobranca;
        private Guid _idDaOrdemDeCompra;
        private Guid _idDoUsuario;
        private Guid _idDoCliente;
        private Guid _idDoConteudo;
        private DateTime _dataDaCompra;
        private decimal _valorRecorrente;
        private DateTime _dataDaPrimeiraCobranca;
        private int _mes;
        private List<ProcessoDeCobrancaRecorrenteDebito> _debitosAgendados;
        private List<ProcessoDeCobrancaRecorrenteDebito> _debitosRegistrados;

        public ProcessoDeCobrancaRecorrente(Guid id)
            : base(id)
        {
            _debitosAgendados = new List<ProcessoDeCobrancaRecorrenteDebito>();
            _debitosRegistrados = new List<ProcessoDeCobrancaRecorrenteDebito>();

            _status = ProcessoDeCobrancaRecorrenteStatus.NaoIniciado;
        }

        public ProcessoDeCobrancaRecorrente(Guid id, IEnumerable<IVersionedEvent> pastEvents)
            : base(id)
        {
            _debitosAgendados = new List<ProcessoDeCobrancaRecorrenteDebito>();
            _debitosRegistrados = new List<ProcessoDeCobrancaRecorrenteDebito>();

            LoadFromHistory(pastEvents);
        }

        public ProcessoDeCobrancaRecorrente(Guid id, IMemento<ProcessoDeCobrancaRecorrente> memento, IEnumerable<IVersionedEvent> pastEvents)
            : base(id)
        {
            Version = memento.Version;

            var processoDeCobrancaRecorrenteMemento = memento as ProcessoDeCobrancaRecorrenteMemento;

            _status = processoDeCobrancaRecorrenteMemento.Status;
            _idDaCobrancaRecorrente = processoDeCobrancaRecorrenteMemento.IdDaCobrancaRecorrente;
            _idDaCobranca = processoDeCobrancaRecorrenteMemento.IdDaCobranca;
            _idDaOrdemDeCompra = processoDeCobrancaRecorrenteMemento.IdDaOrdemDeCompra;
            _idDoUsuario = processoDeCobrancaRecorrenteMemento.IdDoUsuario;
            _idDoCliente = processoDeCobrancaRecorrenteMemento.IdDoCliente;
            _idDoConteudo = processoDeCobrancaRecorrenteMemento.IdDoConteudo;
            _dataDaCompra = processoDeCobrancaRecorrenteMemento.DataDaCompra;
            _valorRecorrente = processoDeCobrancaRecorrenteMemento.ValorRecorrente;
            _dataDaPrimeiraCobranca = processoDeCobrancaRecorrenteMemento.DataDaPrimeiraCobranca;
            _mes = processoDeCobrancaRecorrenteMemento.Mes;
            _debitosAgendados = processoDeCobrancaRecorrenteMemento.DebitosAgendados.ToList();
            _debitosRegistrados = processoDeCobrancaRecorrenteMemento.DebitosRegistrados.ToList();

            LoadFromHistory(pastEvents);
        }

        public override IMemento<IEventSourced> GetMemento()
        {
            var memento = new ProcessoDeCobrancaRecorrenteMemento
            {
                Id = Id,
                Version = Version,
                Status = _status,
                IdDaCobrancaRecorrente = _idDaCobrancaRecorrente,
                IdDaCobranca = _idDaCobranca,
                IdDaOrdemDeCompra = _idDaOrdemDeCompra,
                IdDoUsuario = _idDoUsuario,
                IdDoCliente = _idDoCliente,
                IdDoConteudo = _idDoConteudo,
                DataDaCompra = _dataDaCompra,
                ValorRecorrente = _valorRecorrente,
                DataDaPrimeiraCobranca = _dataDaPrimeiraCobranca,
                Mes = _mes,
                DebitosAgendados = _debitosAgendados.ToList(),
                DebitosRegistrados = _debitosRegistrados.ToList()
            };

            return memento;
        }

        public void NoRegistroDaCobrancaRecorrente(CobrancaRecorrenteRegistrada evento)
        {
            var dataDaProximaCobranca = evento.DataDaPrimeiraCobranca.AddMonths(1);

            var mesDaFatura = new Mes(dataDaProximaCobranca);

            var idDoDebito = Guid.NewGuid();

            var registrarDebitoRecorrenteNaFaturaDoCliente = new RegistrarDebitoRecorrenteNaFaturaDoCliente
            {
                CorrelationId = evento.Id,
                OriginalCorrelationId = evento.OriginalCorrelationId.HasValue ? evento.OriginalCorrelationId.Value : evento.Id,
                IdDoDebito = idDoDebito,
                IdDoProcessoDeCobrancaRecorrente = Id,
                IdDaCobrancaRecorrente = evento.IdDaCobrancaRecorrente,
                IdDaCobranca = evento.IdDaCobranca,
                IdDaOrdemDeCompra = evento.IdDaOrdemDeCompra,
                IdDoUsuario = evento.IdDoUsuario,
                IdDoCliente = evento.IdDoCliente,
                IdDoConteudo = evento.IdDoConteudo,
                DataDaCompra = evento.DataDaCompra,
                Valor = evento.ValorRecorrente,
                MesDaFatura = mesDaFatura,
                Delay = DateTime.Now.AddMonths(1) - DateTime.Now,
            };

            AddCommand(registrarDebitoRecorrenteNaFaturaDoCliente);

            var processoDeCobrancaRecorrenteIniciado = new ProcessoDeCobrancaRecorrenteIniciado
            {
                CorrelationId = evento.Id,
                OriginalCorrelationId = evento.OriginalCorrelationId.HasValue ? evento.OriginalCorrelationId.Value : evento.Id,
                IdDaCobrancaRecorrente = evento.IdDaCobrancaRecorrente,
                IdDaCobranca = evento.IdDaCobranca,
                IdDaOrdemDeCompra = evento.IdDaOrdemDeCompra,
                IdDoUsuario = evento.IdDoUsuario,
                IdDoCliente = evento.IdDoCliente,
                IdDoConteudo = evento.IdDoConteudo,
                DataDaCompra = evento.DataDaCompra,
                ValorRecorrente = evento.ValorRecorrente,
                DataDaPrimeiraCobranca = evento.DataDaPrimeiraCobranca
            };

            Update(processoDeCobrancaRecorrenteIniciado);

            var debitoRecorrenteAgendado = new DebitoRecorrenteAgendado
            {
                CorrelationId = evento.Id,
                OriginalCorrelationId = evento.OriginalCorrelationId.HasValue ? evento.OriginalCorrelationId.Value : evento.Id,
                IdDoDebito = idDoDebito,
                IdDaCobrancaRecorrente = evento.IdDaCobrancaRecorrente,
                IdDaCobranca = evento.IdDaCobranca,
                IdDaOrdemDeCompra = evento.IdDaOrdemDeCompra,
                IdDoUsuario = evento.IdDoUsuario,
                IdDoCliente = evento.IdDoCliente,
                IdDoConteudo = evento.IdDoConteudo,
                DataDaCompra = evento.DataDaCompra,
                Valor = evento.ValorRecorrente,
                MesDaFatura = mesDaFatura
            };

            Update(debitoRecorrenteAgendado);
        }

        public void NoRegistroDebitoRecorrenteNaFaturaDoCliente(DebitoRecorrenteRegistradoNaFaturaDoCliente evento)
        {
            var dataDaProximaCobranca = _dataDaPrimeiraCobranca.AddMonths(_mes + 1);

            var mesDaFatura = new Mes(dataDaProximaCobranca);

            var idDoDebito = Guid.NewGuid();

            var registrarDebitoRecorrenteNaFaturaDoCliente = new RegistrarDebitoRecorrenteNaFaturaDoCliente
            {
                CorrelationId = evento.Id,
                OriginalCorrelationId = evento.OriginalCorrelationId.HasValue ? evento.OriginalCorrelationId.Value : evento.Id,
                IdDoDebito = idDoDebito,
                IdDoProcessoDeCobrancaRecorrente = Id,
                IdDaCobrancaRecorrente = evento.IdDaCobrancaRecorrente,
                IdDaCobranca = evento.IdDaCobranca,
                IdDaOrdemDeCompra = evento.IdDaOrdemDeCompra,
                IdDoUsuario = evento.IdDoUsuario,
                IdDoCliente = evento.IdDoCliente,
                IdDoConteudo = evento.IdDoConteudo,
                DataDaCompra = evento.DataDaCompra,
                Valor = _valorRecorrente,
                MesDaFatura = mesDaFatura,
                Delay = DateTime.Now.AddMonths(1) - DateTime.Now,
            };

            AddCommand(registrarDebitoRecorrenteNaFaturaDoCliente);

            var debitoRecorrenteRegistrado = new DebitoRecorrenteRegistrado
            {
                CorrelationId = evento.Id,
                OriginalCorrelationId = evento.OriginalCorrelationId.HasValue ? evento.OriginalCorrelationId.Value : evento.Id,
                IdDoDebito = idDoDebito,
                IdDaCobrancaRecorrente = evento.IdDaCobrancaRecorrente,
                IdDaCobranca = evento.IdDaCobranca,
                IdDaOrdemDeCompra = evento.IdDaOrdemDeCompra,
                IdDoUsuario = evento.IdDoUsuario,
                IdDoCliente = evento.IdDoCliente,
                IdDoConteudo = evento.IdDoConteudo,
                DataDaCompra = evento.DataDaCompra,
                Valor = evento.Valor,
                MesDaFatura = evento.MesDaFatura
            };

            Update(debitoRecorrenteRegistrado);

            var debitoRecorrenteAgendado = new DebitoRecorrenteAgendado
            {
                CorrelationId = evento.Id,
                OriginalCorrelationId = evento.OriginalCorrelationId.HasValue ? evento.OriginalCorrelationId.Value : evento.Id,
                IdDoDebito = idDoDebito,
                IdDaCobrancaRecorrente = evento.IdDaCobrancaRecorrente,
                IdDaCobranca = evento.IdDaCobranca,
                IdDaOrdemDeCompra = evento.IdDaOrdemDeCompra,
                IdDoUsuario = evento.IdDoUsuario,
                IdDoCliente = evento.IdDoCliente,
                IdDoConteudo = evento.IdDoConteudo,
                DataDaCompra = evento.DataDaCompra,
                Valor = _valorRecorrente,
                MesDaFatura = mesDaFatura
            };

            Update(debitoRecorrenteAgendado);
        }
        protected override void SetHandlers()
        {
            Handles<ProcessoDeCobrancaRecorrenteIniciado>(When);
            Handles<DebitoRecorrenteAgendado>(When);
            Handles<DebitoRecorrenteRegistrado>(When);
        }

        private void When(ProcessoDeCobrancaRecorrenteIniciado @event)
        {
            _status = ProcessoDeCobrancaRecorrenteStatus.AguardandoRegistroDoDebito;
            _idDaCobrancaRecorrente = @event.IdDaCobrancaRecorrente;
            _idDaCobranca = @event.IdDaCobranca;
            _idDaOrdemDeCompra = @event.IdDaOrdemDeCompra;
            _idDoUsuario = @event.IdDoUsuario;
            _idDoCliente = @event.IdDoCliente;
            _idDoConteudo = @event.IdDoConteudo;
            _dataDaCompra = @event.DataDaCompra;
            _valorRecorrente = @event.ValorRecorrente;
            _mes = 1;
        }

        private void When(DebitoRecorrenteAgendado @event)
        {
            _debitosAgendados.Add(new ProcessoDeCobrancaRecorrenteDebito(@event.IdDoDebito, @event.Valor, @event.MesDaFatura));
        }

        private void When(DebitoRecorrenteRegistrado @event)
        {
            _debitosRegistrados.Add(new ProcessoDeCobrancaRecorrenteDebito(@event.IdDoDebito, @event.Valor, @event.MesDaFatura));

            _mes++;
        }
    }
}

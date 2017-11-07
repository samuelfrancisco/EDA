using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.EventSourcing;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Pagamentos.Commands;
using EDA.Poc.Pagamentos.Domain.Enums;
using EDA.Poc.Pagamentos.Events;

namespace EDA.Poc.Pagamentos.Domain
{
    public class OrdemDeCompra : EventSourced
    {
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
        private OrdemDeCompraPromocao _promocao;
        private DateTime _criadaEm;
        private DateTime _dataDaCompra;
        private DateTime? _concluidaEm;
        private OrdemDeCompraStatus _status;

        public OrdemDeCompra(Guid idDoUsuario, Guid idDoCliente, Guid idDoConteudo, TipoDeLicenciamento tipoDeLicenciamento, 
            int? quantidadeDeUsuarios, DateTime disponibilizarAPartirDe, TipoDeValidade tipoDeValidade, int? quantidadeDeMeses,
            FormaDePagamento formaDePagamento, decimal precoDoConteudo, decimal valorMensalDaCompra, 
            OrdemDeCompraPromocao promocao, DateTime dataDaCompra, Guid idDoComandoDeCompra)
            : base(Guid.NewGuid())
        {
            var evento = new OrdemDeCompraCriada
                             {
                                 CorrelationId = idDoComandoDeCompra,
                                 OriginalCorrelationId = idDoComandoDeCompra,
                                 IdDoProcessoDeCompra = Guid.NewGuid(),
                                 IdDoUsuario = idDoUsuario,
                                 IdDoCliente = idDoCliente,
                                 IdDoConteudo = idDoConteudo,
                                 TipoDeLicenciamento = tipoDeLicenciamento,
                                 QuantidadeDeUsuarios = quantidadeDeUsuarios,
                                 DisponibilizarAPartirDe = disponibilizarAPartirDe,
                                 TipoDeValidade = tipoDeValidade,
                                 QuantidadeDeMeses = quantidadeDeMeses,
                                 FormaDePagamento = formaDePagamento,
                                 PrecoDoConteudo = precoDoConteudo,
                                 ValorMensalDaCompra = valorMensalDaCompra,
                                 DataDaCompra = dataDaCompra
                             };

            if (promocao != null)
                evento.Promocao = new OrdemDeCompraCriadaPromocao
                                      {
                                          IdDaPromocao = promocao.IdDaPromocao,
                                          TipoDeDesconto = promocao.TipoDeDesconto,
                                          ValorDoDesconto = promocao.ValorDoDesconto
                                      };

            Update(evento);
        }

        public OrdemDeCompra(Guid id, IEnumerable<IVersionedEvent> pastEvents)
            : base(id)
        {
            LoadFromHistory(pastEvents);
        }

        public OrdemDeCompra(Guid id, IMemento<OrdemDeCompra> memento, IEnumerable<IVersionedEvent> pastEvents)
            : base(id)
        {
            var ordemDeCompraMemento = (OrdemDeCompraMemento) memento;

            Version = memento.Version;

            _idDoUsuario = ordemDeCompraMemento.IdDoUsuario;
            _idDoCliente = ordemDeCompraMemento.IdDoCliente;
            _idDoConteudo = ordemDeCompraMemento.IdDoConteudo;
            _tipoDeLicenciamento = ordemDeCompraMemento.TipoDeLicenciamento;
            _quantidadeDeUsuarios = ordemDeCompraMemento.QuantidadeDeUsuarios;
            _disponibilizarAPartirDe = ordemDeCompraMemento.DisponibilizarAPartirDe;
            _tipoDeValidade = ordemDeCompraMemento.TipoDeValidade;
            _quantidadeDeMeses = ordemDeCompraMemento.QuantidadeDeMeses;
            _formaDePagamento = ordemDeCompraMemento.FormaDePagamento;
            _precoDoConteudo = ordemDeCompraMemento.PrecoDoConteudo;
            _valorMensalDaCompra = ordemDeCompraMemento.ValorMensalDaCompra;
            _promocao = ordemDeCompraMemento.Promocao;
            _criadaEm = ordemDeCompraMemento.CriadaEm;
            _concluidaEm = ordemDeCompraMemento.ConcluidaEm;
            _dataDaCompra = ordemDeCompraMemento.DataDaCompra;

            LoadFromHistory(pastEvents);
        }

        public override IMemento<IEventSourced> GetMemento()
        {
            var memento = new OrdemDeCompraMemento
                              {
                                  Id = Id,
                                  Version = Version,                                  
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
                                  DataDaCompra = _dataDaCompra,
                                  CriadaEm = _criadaEm,
                                  ConcluidaEm = _concluidaEm,
                                  Status = _status
                              };

            return memento;
        }

        public void Concluir(ConcluirOrdemDeCompra comando)
        {
            var evento = new OrdemDeCompraConcluida
                             {
                                 CorrelationId = comando.Id,
                                 OriginalCorrelationId = comando.OriginalCorrelationId.HasValue ? comando.OriginalCorrelationId.Value : comando.Id
                             };

            Update(evento);
        }

        protected override void SetHandlers()
        {
            Handles<OrdemDeCompraCriada>(When);
            Handles<OrdemDeCompraConcluida>(When);
        }

        private void When(OrdemDeCompraCriada @evento)
        {
            _idDoUsuario = @evento.IdDoUsuario;
            _idDoCliente = @evento.IdDoCliente;
            _idDoConteudo = @evento.IdDoConteudo;
            _tipoDeLicenciamento = @evento.TipoDeLicenciamento;
            _quantidadeDeUsuarios = @evento.QuantidadeDeUsuarios;
            _disponibilizarAPartirDe = @evento.DisponibilizarAPartirDe;
            _tipoDeValidade = @evento.TipoDeValidade;
            _quantidadeDeMeses = @evento.QuantidadeDeMeses;
            _formaDePagamento = @evento.FormaDePagamento;
            _precoDoConteudo = @evento.PrecoDoConteudo;
            _valorMensalDaCompra = @evento.ValorMensalDaCompra;
            _criadaEm = @evento.Date;
            _dataDaCompra = @evento.DataDaCompra;
            _status = OrdemDeCompraStatus.Pendente;

            if (@evento.Promocao != null)
            {
                _promocao = new OrdemDeCompraPromocao
                                {
                                    IdDaPromocao = @evento.Promocao.IdDaPromocao,
                                    TipoDeDesconto = @evento.Promocao.TipoDeDesconto,
                                    ValorDoDesconto = @evento.Promocao.ValorDoDesconto
                                };
            }
        }

        private void When(OrdemDeCompraConcluida @event)
        {
            _concluidaEm = @event.Date;
            _status = OrdemDeCompraStatus.Concluida;
        }
    }
}
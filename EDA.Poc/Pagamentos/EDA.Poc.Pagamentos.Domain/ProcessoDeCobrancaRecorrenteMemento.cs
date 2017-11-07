using System;
using EDA.Poc.Pagamentos.Domain.Enums;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using System.Collections.Generic;

namespace EDA.Poc.Pagamentos.Domain
{
    public class ProcessoDeCobrancaRecorrenteMemento : IMemento<ProcessoDeCobrancaRecorrente>, IMemento<IEventSourced>
    {        
        public Guid Id { get; set; }
        public long Version { get; set; }
        public ProcessoDeCobrancaRecorrenteStatus Status { get; set; }
        public Guid IdDaCobrancaRecorrente { get; set; }
        public Guid IdDaCobranca { get; set; }
        public Guid IdDaOrdemDeCompra { get; set; }
        public Guid IdDoUsuario { get; set; }
        public Guid IdDoCliente { get; set; }
        public Guid IdDoConteudo { get; set; }
        public DateTime DataDaCompra { get; set; }
        public decimal ValorRecorrente { get; set; }
        public DateTime DataDaPrimeiraCobranca { get; set; }
        public int Mes { get; set; }       
        public List<ProcessoDeCobrancaRecorrenteDebito> DebitosAgendados { get; set; }
        public List<ProcessoDeCobrancaRecorrenteDebito> DebitosRegistrados { get; set; }        
    }
}

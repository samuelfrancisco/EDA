using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;

namespace EDA.Poc.Pagamentos.Domain
{
    public class ClienteMemento : IMemento<Cliente>, IMemento<IEventSourced>
    {
        public Guid Id { get; set; }
        public long Version { get; set; }        
        public List<CobrancaDeCompra> Cobrancas { get; set; }
        public List<FaturaMensal> FaturasMensais { get; set; }
        public List<LicencaDeConteudo> LicencasDeConteudo { get; set; }
        public List<CobrancaRecorrente> CobrancasRecorrentes { get; set; }
    }
}
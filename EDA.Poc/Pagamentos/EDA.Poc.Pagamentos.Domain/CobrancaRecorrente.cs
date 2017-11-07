using System;

namespace EDA.Poc.Pagamentos.Domain
{
    public class CobrancaRecorrente
    {
        public Guid Id { get; set; }
        public Guid IdDaCobranca { get; set; }
        public Guid IdDaOrdemDeCompra { get; set; }
        public Guid IdDoUsuario { get; set; }
        public DateTime DataDaCompra { get; set; }
        public decimal ValorRecorrente { get; set; }

        public CobrancaRecorrente(Guid id, Guid idDaCobranca, Guid idDaOrdemDeCompra, Guid idDoUsuario, DateTime dataDaCompra, decimal valorRecorrente)
        {
            Id = id;
            IdDaCobranca = idDaCobranca;
            IdDaOrdemDeCompra = idDaOrdemDeCompra;
            IdDoUsuario = idDoUsuario;
            DataDaCompra = dataDaCompra;
            ValorRecorrente = valorRecorrente;
        }
    }
}

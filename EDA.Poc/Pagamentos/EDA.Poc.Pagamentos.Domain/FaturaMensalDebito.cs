using System;

namespace EDA.Poc.Pagamentos.Domain
{
    public class FaturaMensalDebito
    {
        public Guid Id { get; private set; }
        public Guid IdDaCobranca { get; private set; }
        public Guid IdDaOrdemDeCompra { get; private set; }
        public Guid IdDoUsuario { get; private set; }
        public DateTime DataDaCompra { get; private set; }
        public decimal Valor { get; private set; }

        public FaturaMensalDebito(Guid id, Guid idDaCobranca, Guid idDaOrdemDeCompra, Guid idDoUsuario, DateTime dataDaCompra, decimal valor)
        {
            if (id == Guid.Empty) throw new ArgumentOutOfRangeException("id");
            if (idDaCobranca == Guid.Empty) throw new ArgumentOutOfRangeException("idDaCobranca");
            if (idDaOrdemDeCompra == Guid.Empty) throw new ArgumentOutOfRangeException("idDaOrdemDeCompra");
            if (idDoUsuario == Guid.Empty) throw new ArgumentOutOfRangeException("idDoUsuario");

            Id = id;
            IdDaCobranca = idDaCobranca;
            IdDaOrdemDeCompra = idDaOrdemDeCompra;
            IdDoUsuario = idDoUsuario;
            DataDaCompra = dataDaCompra;
            Valor = valor;
        }
    }
}
using System;
using EDA.Poc.Pagamentos.Domain.Enums;

namespace EDA.Poc.Pagamentos.Domain
{
    public class CobrancaDeCompra
    {
        public Guid Id { get; set; }
        public Guid IdDaOrdemDeCompra { get; set; }
        public DateTime DataDaCompra { get; set; }
        public decimal ValorMensalDaCompra { get; set; }
        public TipoDeValidade TipoDeValidade { get; set; }
        public int? QuantidadeDeMeses { get; set; }
        public CobrancaDeCompraStatus Status { get; set; }

        public CobrancaDeCompra(Guid id, Guid idDaOrdemDeCompra, DateTime dataDaCompra, decimal valorMensalDaCompra, 
                                TipoDeValidade tipoDeValidade, int? quantidadeDeMeses)
        {
            Id = id;
            IdDaOrdemDeCompra = idDaOrdemDeCompra;
            DataDaCompra = dataDaCompra;
            ValorMensalDaCompra = valorMensalDaCompra;
            TipoDeValidade = tipoDeValidade;
            QuantidadeDeMeses = quantidadeDeMeses;
            Status = CobrancaDeCompraStatus.Ativa;
        }
    }
}
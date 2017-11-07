using System;
using EDA.Poc.Pagamentos.Domain.Enums;

namespace EDA.Poc.Pagamentos.Domain
{
    public class ProcessoDeCompraPromocao
    {
        public Guid IdDaPromocao { get; set; }
        public TipoDeDesconto TipoDeDesconto { get; set; }
        public decimal ValorDoDesconto { get; set; }
    }
}
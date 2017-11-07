using System;
using EDA.Poc.Pagamentos.Domain.Enums;

namespace EDA.Poc.Pagamentos.Events
{
    public class ProcessoDeCompraIniciadoPromocao
    {
        public Guid IdDaPromocao { get; set; }
        public TipoDeDesconto TipoDeDesconto { get; set; }
        public decimal ValorDoDesconto { get; set; }
    }
}
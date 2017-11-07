using System;
using EDA.Poc.Pagamentos.Domain.Enums;

namespace EDA.Poc.Pagamentos.Domain
{
    public class ConteudoPromocao
    {
        public Guid IdDaPromocao { get; set; }
        public TipoDeDesconto TipoDeDesconto { get; set; }
        public decimal ValorDoDesconto { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
    }
}
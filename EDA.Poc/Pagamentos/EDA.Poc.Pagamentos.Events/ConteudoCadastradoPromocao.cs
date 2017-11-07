using System;
using EDA.Poc.Pagamentos.Domain.Enums;

namespace EDA.Poc.Pagamentos.Events
{
    public class ConteudoCadastradoPromocao
    {
        public Guid IdDaPromocao { get; set; }
        public TipoDeDesconto TipoDeDesconto { get; set; }
        public decimal ValorDoDesconto { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
    }
}
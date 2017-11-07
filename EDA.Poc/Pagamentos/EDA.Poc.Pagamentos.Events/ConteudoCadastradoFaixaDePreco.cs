using System;

namespace EDA.Poc.Pagamentos.Events
{
    public class ConteudoCadastradoFaixaDePreco
    {
        public int QuantidadeInicialDeLicencas { get; set; }
        public int? QuantidadeFinalDeLicencas { get; set; }
        public Decimal Preco { get; set; }
    }
}
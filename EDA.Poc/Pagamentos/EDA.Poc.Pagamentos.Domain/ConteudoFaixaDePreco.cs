using System;

namespace EDA.Poc.Pagamentos.Domain
{
    public class ConteudoFaixaDePreco
    {
        public int QuantidadeInicialDeLicencas { get; set; }
        public int? QuantidadeFinalDeLicencas { get; set; }
        public Decimal Preco { get; set; }
    }
}
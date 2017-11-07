using System;

namespace EDA.Poc.IntegracaoTemporaria.Events
{
    public class NovoConteudoCadastradoFaixaDePreco
    {
        public int QuantidadeInicialDeLicencas { get; set; }
        public int? QuantidadeFinalDeLicencas { get; set; }
        public Decimal Preco { get; set; }
    }
}
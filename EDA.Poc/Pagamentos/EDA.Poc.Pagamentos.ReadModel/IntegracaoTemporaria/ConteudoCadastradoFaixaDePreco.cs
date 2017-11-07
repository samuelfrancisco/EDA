using System;

namespace EDA.Poc.Pagamentos.ReadModel.IntegracaoTemporaria
{
    public class ConteudoCadastradoFaixaDePreco
    {
        public int QuantidadeInicialDeLicencas { get; set; }
        public int? QuantidadeFinalDeLicencas { get; set; }
        public Decimal Preco { get; set; }
    }
}
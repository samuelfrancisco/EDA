using System;
using System.Collections.Generic;

namespace EDA.Poc.Pagamentos.ReadModel.IntegracaoTemporaria
{
    public class ConteudoCadastrado
    {
        public Guid IdDoConteudo { get; set; }
        public decimal PrecoIlimitado { get; set; }
        public IEnumerable<ConteudoCadastradoFaixaDePreco> FaixaDePrecos { get; set; }
    }
}

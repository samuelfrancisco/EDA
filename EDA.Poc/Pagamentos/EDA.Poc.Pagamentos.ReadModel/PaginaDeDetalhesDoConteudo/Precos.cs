using System;
using System.Collections.Generic;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo
{
    public class Precos
    {       
        public Guid IdDoConteudo { get; set; }

        public decimal PrecoIlimitado { get; set; }

        public string PrecoIlimitadoLabel { get; set; }

        public virtual List<PrecoPorQuantidadeDeUsuarios> PrecosPorQuantidadeDeUsuarios { get; set; }
    }
}


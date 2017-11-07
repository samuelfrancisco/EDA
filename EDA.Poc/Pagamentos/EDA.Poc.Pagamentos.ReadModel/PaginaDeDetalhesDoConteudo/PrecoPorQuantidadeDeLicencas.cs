using System;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo
{
    public class PrecoPorQuantidadeDeUsuarios
    {        
        public Guid IdDoConteudo { get; set; }
        
        public Guid IdPrecoPorQuantidadeDeUsuarios { get; set; }

        public decimal Preco { get; set; }

        public int LicencasDe { get; set; }

        public int? LicencasAte { get; set; }
    }
}
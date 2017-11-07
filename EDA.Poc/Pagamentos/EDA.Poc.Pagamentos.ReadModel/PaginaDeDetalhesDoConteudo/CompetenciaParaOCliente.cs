using System;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo
{
    public class CompetenciaParaOCliente
    {
        public Guid IdDoConteudo { get; set; }    
            
        public string SiglaDoIdioma { get; set; }

        public Guid IdDoCliente { get; set; }

        public Guid IdDaCompetencia { get; set; }

        public string Titulo { get; set; }

        public string Gaps { get; set; }

        public string Medidas { get; set; }

        public string Trabalhadas { get; set; }
    }
}

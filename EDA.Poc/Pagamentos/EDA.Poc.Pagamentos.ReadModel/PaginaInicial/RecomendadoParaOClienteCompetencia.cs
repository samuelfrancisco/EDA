using System;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaInicial
{
    public class RecomendadoParaOClienteCompetencia
    {
        public Guid IdDoCliente { get; set; }

        public string SiglaDoIdioma { get; set; }

        public Guid IdDoConteudo { get; set; }

        public string IdDaCompetencia { get; set; }

        public int QuantidadeDeVezesMedidas { get; set; }

        public int PorcentagemDosGaps { get; set; }
    }
}

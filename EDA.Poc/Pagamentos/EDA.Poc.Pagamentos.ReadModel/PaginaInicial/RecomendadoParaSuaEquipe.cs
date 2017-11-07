using EDA.Poc.Infraestrutura.Images.Enums;
using EDA.Poc.Infraestrutura.Images.Services;
using System;
using System.Collections.Generic;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaInicial
{
    public class RecomendadoParaSuaEquipe
    {
        public Guid IdDoCliente { get; set; }

        public string SiglaDoIdioma { get; set; }

        public Guid IdDoUsuario { get; set; }

        public Guid IdDoConteudo { get; set; }

        public decimal Avaliacao { get; set; }

        public decimal Custo { get; set; }

        public string Tipo { get; set; }

        public string Titulo { get; set; }

        public string CustoLabel { get; set; }

        public Guid IdDaEscola { get; set; }

        public virtual List<RecomendadoParaSuaEquipeCompetencia> CompetenciasAvaliadas { get; set; }

        public RecomendadoParaSuaEquipe()
        {
            CompetenciasAvaliadas = new List<RecomendadoParaSuaEquipeCompetencia>();
        }

        public string Imagem
        {
            get
            {
                var servicoImagem = new ImagePathService();

                return servicoImagem.GetImagePath(IdDoConteudo, ImageCollection.Conteudo, ImageFormat.Default);
            }
        }
    }
}

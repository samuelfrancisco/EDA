using EDA.Poc.Infraestrutura.Images.Enums;
using EDA.Poc.Infraestrutura.Images.Services;
using System;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo
{
    public class Preview
    {        
        public Guid IdDoConteudo { get; set; }
        
        public string SiglaDoIdioma { get; set; }

        public Guid IdDoPreview { get; set; }

        public string Titulo { get; set; }
        
        public string Imagem
        {
            get
            {
                var servicoImagem = new ImagePathService();

                return servicoImagem.GetImagePath(IdDoPreview, ImageCollection.Preview, ImageFormat.Default);
            }
        }
    }
}

using EDA.Poc.Infraestrutura.Images.Enums;
using EDA.Poc.Infraestrutura.Images.Services;
using System;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaInicial
{
    public class DestaqueDaSemana
    {
        public Guid IdDoCliente { get; set; }
                
        public string SiglaDoIdioma { get; set; }

        public Guid IdDaPromocao { get; set; }

        public string LarguraMinima { get; set; }

        public Guid IdDaEscola { get; set; }
        
        public string Banner
        {
            get
            {
                var servicoImagem = new ImagePathService();

                return servicoImagem.GetImagePath(IdDaPromocao, ImageCollection.Promocao, ImageFormat.Banner);
            }
        }
        
        public string BannerMobile
        {
            get
            {
                var servicoImagem = new ImagePathService();

                return servicoImagem.GetImagePath(IdDaPromocao, ImageCollection.Promocao, ImageFormat.BannerMobile);
            }
        }
    }
}

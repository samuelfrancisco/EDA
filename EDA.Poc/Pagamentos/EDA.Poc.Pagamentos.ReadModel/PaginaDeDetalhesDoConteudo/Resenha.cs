using EDA.Poc.Infraestrutura.Images.Enums;
using EDA.Poc.Infraestrutura.Images.Services;
using System;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo
{
    public class Resenha
    {        
        public Guid IdDoConteudo { get; set; }
                
        public Guid IdDoUsuario { get; set; }

        public int Avaliacao { get; set; }

        public string NomeUsuario { get; set; }

        public string Conteudo { get; set; }
        
        public DateTime Data { get; set; }

        public string DataLabel { get { return Data.ToShortDateString(); } }
       
        public string ImagemDoUsuario
        {
            get
            {
                var servicoImagem = new ImagePathService();

                return servicoImagem.GetImagePath(IdDoUsuario, ImageCollection.Usuario, ImageFormat.Default);
            }
        }
    }
}

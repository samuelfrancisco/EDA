using EDA.Poc.Infraestrutura.Images.Enums;
using EDA.Poc.Infraestrutura.Images.Services;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;
using System;
using System.ComponentModel.DataAnnotations;

namespace EDA.Poc.Web.ViewModels
{
    public class AvaliarViewModel
    {
        public AvaliarViewModel()
        {
        }

        public AvaliarViewModel(Resenha minhaAvaliacao)
        {
            IdDoConteudo = minhaAvaliacao.IdDoConteudo;
            NomeDoUsuario = minhaAvaliacao.NomeUsuario;
            IdDoUsuario = minhaAvaliacao.IdDoUsuario;
            Avaliacao = minhaAvaliacao.Avaliacao;
            Conteudo = minhaAvaliacao.Conteudo;
        }

        public AvaliarViewModel(Guid idDoConteudo, Guid idDoUsuario, string nomeDoUsuario, Resenha minhaAvaliacao)
        {
            IdDoUsuario = idDoUsuario;
            IdDoConteudo = idDoConteudo;
            NomeDoUsuario = nomeDoUsuario;

            if (minhaAvaliacao == null) return;

            Avaliacao = minhaAvaliacao.Avaliacao;
            Conteudo = minhaAvaliacao.Conteudo;
        }

        public Guid IdDoConteudo { get; set; }

        public Guid IdDoUsuario { get; set; }

        public string ImagemDoUsuario {
            get
            {
                var servicoImagem = new ImagePathService();

                return servicoImagem.GetImagePath(IdDoUsuario, ImageCollection.Usuario, ImageFormat.Default);
            }
        }

        public string NomeDoUsuario { get; set; }

        [Required(ErrorMessage="Por favor, insira um texto para a sua avaliação.")]
        [RegularExpression(".+", ErrorMessage = "Por favor, insira um texto para a sua avaliação.")]
        public string Conteudo { get; set; }

        [Required(ErrorMessage = "Por favor, escolha a quantidade de estrelas da sua avaliação.")]
        [Range(1, 5, ErrorMessage = "Por favor, escolha a quantidade de estrelas da sua avaliação.")]
        public int Avaliacao { get; set; }
    }
}

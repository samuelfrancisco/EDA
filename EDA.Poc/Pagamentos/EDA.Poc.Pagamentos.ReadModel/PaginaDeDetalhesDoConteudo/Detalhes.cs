using EDA.Poc.Infraestrutura.Images.Enums;
using EDA.Poc.Infraestrutura.Images.Services;
using System;
using System.Collections.Generic;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo
{
    public class Detalhes
    {
        public Guid IdDoConteudo { get; set; }

        public string SiglaDoIdioma { get; set; }

        public int QuantidadeDeAquisicoes { get; set; }

        public decimal Avaliacao { get; set; }

        public string CustoLabel { get; set; }

        public string Tipo { get; set; }

        public string Titulo { get; set; }

        public string Objetivo { get; set; }

        public string Conteudo { get; set; }

        public string Descricao { get; set; }

        public string Link { get; set; }

        public string CargaHoraria { get; set; }

        public List<PalavraChave> PalavrasChave { get; set; }

        public Guid IdDaEscola { get; set; }

        public string Imagem
        {
            get
            {
                var servicoImagem = new ImagePathService();

                return servicoImagem.GetImagePath(IdDoConteudo, ImageCollection.Conteudo, ImageFormat.Default);
            }
        }
        
        public Detalhes()
        {
            PalavrasChave = new List<PalavraChave>();
        }
    }
}


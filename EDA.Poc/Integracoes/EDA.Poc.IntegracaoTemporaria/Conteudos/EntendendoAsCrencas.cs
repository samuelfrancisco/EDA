using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class EntendendoAsCrencas : ConteudoBase
    {
        protected override Guid IdDoConteudo { get; set; }
        protected override string Titulo { get; set; }
        protected override string Objetivo { get; set; }
        protected override string Descricao { get; set; }
        protected override string Conteudo { get; set; }
        protected override string Tipo { get; set; }
        protected override decimal Custo { get; set; }
        protected override string CustoLabel { get; set; }
        protected override decimal PrecoIlimitado { get; set; }
        protected override string PrecoIlimitadoLabel { get; set; }
        protected override string Link { get; set; }
        protected override string CargaHoraria { get; set; }

        public EntendendoAsCrencas()
        {
            IdDoConteudo = Guid.Parse("4646d340-cddf-48c3-a326-9d4e9232ce25");
            Titulo = "Entendendo as crenças";
            Objetivo = "";
            Descricao = "Vídeo com Alberto relacionando as crenças com o nosso desempenho.";
            Conteudo = "Como as crenças podem afetar o desempenho do vendedor?";
            Tipo = "vLearning";
            Custo = 100;
            CustoLabel = "R$ 100,00";
            PrecoIlimitado = 10000;
            PrecoIlimitadoLabel = "R$ 10.000,00";
            Link = string.Format("{0}{1}{2}", ConfigurationManager.AppSettings["LinkPadraoDoPacoteScorm"], IdDoConteudo, "/");
            CargaHoraria = "01:00h";
        }

        protected override List<PalavraChave> GerarPalavrasChave()
        {
            return new List<PalavraChave>
                       {
                           Tags.Venda(IdDoConteudo),

                           Tags.Crenca(IdDoConteudo),

                           Tags.AlbertoCouto(IdDoConteudo),

                           Tags.Desempenho(IdDoConteudo)
                       };
        }

        protected override List<Preview> GerarPreviews()
        {
            return new List<Preview>
                       {
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("934b11e5-5ce7-4eb5-86a3-66d49194f086"),
                                   Titulo = "Entenda Crenças",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("fa25f7d8-535d-4461-9b4e-9b8de4beea89"),
                                   Titulo = "Entenda Crenças",
                               }
                       };
        }


        protected override List<MaisDoFornecedor> ObterMaisDoFornecedor()
        {
            return new List<MaisDoFornecedor>()
            {
                new ManejandoObjecoes().ToMaisDoFornecedor(),

                new Objecoes().ToMaisDoFornecedor()
            };
        }

        protected override List<Semelhante> ObterSemelhantes()
        {
            return new List<Semelhante>()
            {
                new Objecoes().ToSemelhante(),

                new Crencas().ToSemelhante()
            };
        } 
    }
}

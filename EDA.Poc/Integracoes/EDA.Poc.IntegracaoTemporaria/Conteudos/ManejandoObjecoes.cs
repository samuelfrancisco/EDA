using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class ManejandoObjecoes : ConteudoBase
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

        public ManejandoObjecoes()
        {
            IdDoConteudo = Guid.Parse("e0a3053e-5a7d-476e-8813-b567da5e0f11");
            Titulo = "Manejando objeções";
            Objetivo = "";
            Descricao = "Após explicar o que são objeções, Alberto aborda como manejá-las a seu favor.";
            Conteudo = "Como manejar as objeções do cliente para conseguir fechar o negócio.";
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

                           Tags.Objecao(IdDoConteudo),

                           Tags.AlbertoCouto(IdDoConteudo),

                           Tags.Cliente(IdDoConteudo)
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
                                   IdDoPreview = Guid.Parse("6dea7f41-32e3-4d38-857c-2d01b0f6cb3f"),
                                   Titulo = "Objeções",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("75d6d0d1-1162-4a92-bfa9-23294b019dff"),
                                   Titulo = "Objeções",
                               }
                       };
        }

        protected override List<MaisDoFornecedor> ObterMaisDoFornecedor()
        {
            return new List<MaisDoFornecedor>()
            {
                new Objecoes().ToMaisDoFornecedor(),

                new Crencas().ToMaisDoFornecedor()
            };
        }

        protected override List<Semelhante> ObterSemelhantes()
        {
            return new List<Semelhante>()
            {
                new Crencas().ToSemelhante(),

                new EntendendoAsCrencas().ToSemelhante()
            };
        } 
    }
}

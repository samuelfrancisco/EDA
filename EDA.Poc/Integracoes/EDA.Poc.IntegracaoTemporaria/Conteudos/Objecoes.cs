using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class Objecoes : ConteudoBase
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

        public Objecoes()
        {
            IdDoConteudo = Guid.Parse("ec786267-02a0-48b8-a6ad-73335936d9dc");
            Titulo = "Objeções";
            Objetivo = "";
            Descricao = "Vídeo com Alberto explicando o que são objeções e porque elas ocorrem.";
            Conteudo = "Entenda melhor como ocorrem as objeções durante suas conversas com os clientes.";
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
                                   IdDoPreview = Guid.Parse("81fcafe3-8a9e-430d-8fad-8142400fa0f9"),
                                   Titulo = "Manejando Objeções",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("47917099-bcbd-493f-b8a4-6558093767a5"),
                                   Titulo = "Manejando Objeções",
                               }
                       };
        }

        protected override List<MaisDoFornecedor> ObterMaisDoFornecedor()
        {
            return new List<MaisDoFornecedor>()
            {
                new Crencas().ToMaisDoFornecedor(),

                new EntendendoAsCrencas().ToMaisDoFornecedor()
            };
        }

        protected override List<Semelhante> ObterSemelhantes()
        {
            return new List<Semelhante>()
            {
                new EntendendoAsCrencas().ToSemelhante(),

                new ManejandoObjecoes().ToSemelhante()
            };
        } 
    }
}

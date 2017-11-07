using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class Crencas : ConteudoBase
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

        public Crencas()
        {
            IdDoConteudo = Guid.Parse("e1960fff-b159-4b42-8fca-48968052c10b");
            Titulo = "Crenças";
            Objetivo = "";
            Descricao = "Vídeo com Alberto abordando o assunto crenças";
            Conteudo = "Alberto demonstra como as crenças surgem e são facilmente incorporadas no nosso cotidiano.";
            Tipo = "vLearning";
            Custo = 100;
            CustoLabel = "R$ 100,00";
            PrecoIlimitado = 10000;
            PrecoIlimitadoLabel = "R$ 10.000,00";
            Link = string.Format("{0}{1}{2}", ConfigurationManager.AppSettings["LinkPadraoDoPacoteScorm"], IdDoConteudo ,"/");
            CargaHoraria = "01:00h";
        }

        protected override List<CompetenciaParaOCliente> GerarCompetenciasParaOCliente()
        {
            return new List<CompetenciaParaOCliente>
            {
                CompetenciasCliente.TrabalhoEmEquipe(IdDoConteudo, "3-5", "2", "3"),

                CompetenciasCliente.Vendas(IdDoConteudo, "3", "3", "3"),

                CompetenciasCliente.FocoNoCliente(IdDoConteudo, "2", "1", "2"),

                CompetenciasCliente.FocoEmResultados(IdDoConteudo, "1-5", "1", "2")
            };
        }

        protected override List<CompetenciaParaSuaEmpresa> GerarCompetenciasParaSuaEmpresa()
        {
            return new List<CompetenciaParaSuaEmpresa>
            {
                CompetenciasEmpresa.TrabalhoEmEquipe(IdDoConteudo, "3", "2", "3"),

                CompetenciasEmpresa.Vendas(IdDoConteudo, "2", "3", "3"),

                CompetenciasEmpresa.FocoNoCliente(IdDoConteudo, "1", "1", "2"),

                CompetenciasEmpresa.FocoEmResultados(IdDoConteudo, "1-5", "1", "2")
            };
        }

        protected override List<PalavraChave> GerarPalavrasChave()
        {
            return new List<PalavraChave>
                       {
                           Tags.Venda(IdDoConteudo),

                           Tags.Crenca(IdDoConteudo),

                           Tags.AlbertoCouto(IdDoConteudo)
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
                                   IdDoPreview = Guid.Parse("6500fed8-f7d3-4982-a80a-654bbad45229"),
                                   Titulo = "Crenças",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("3256044a-9a45-4e71-a4a0-d27104a10627"),
                                   Titulo = "Crenças",
                               }
                       };
        }

        protected override List<MaisDoFornecedor> ObterMaisDoFornecedor()
        {
            return new List<MaisDoFornecedor>()
            {
                new EntendendoAsCrencas().ToMaisDoFornecedor(),

                new ManejandoObjecoes().ToMaisDoFornecedor()
            };
        }

        protected override List<Semelhante> ObterSemelhantes()
        {
            return new List<Semelhante>()
            {
                new ManejandoObjecoes().ToSemelhante(),

                new Objecoes().ToSemelhante()
            };
        } 
    }
}

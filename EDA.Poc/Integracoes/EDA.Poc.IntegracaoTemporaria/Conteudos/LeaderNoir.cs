using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class LeaderNoir : ConteudoBase
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

        public LeaderNoir()
        {
            IdDoConteudo = Guid.Parse("b05c726d-0d2c-4482-b328-b819dd745a6f");
            Titulo = "Leader Noir";
            Objetivo = "Ajudar o participante a refletir sobre a competência de liderança de forma prática e estimulante.";
            Descricao = "Em Leader Noir, o participante será responsável por ajudar o Gerente de Marketing de uma importante, mas decadente, marca de chocolates, a Céu Estrelado a reverter um quadro de profunda desmotivação de sua equipe. Para isso, é preciso conhecer bem a equipe e entender o que é importante para o engajamento de cada um deles.";
            Conteudo = @"<h4>Público-Alvo</h4><span>Gestores de Pessoas e Ocupantes de posições de 1ª liderança.</span>
                         <h4>Por Que Utilizar Este Game?</h4><span>Em Leader Noir, o participante terá a oportunidade de praticar suas habilidades de liderança especialmente no que tange à liderança de equipes, gestão de talentos, feedback e retenção. Trata-se da oportunidade de levá-lo à compreensão na prática de como tornar uma equipe mais motivada, comprometida e produtiva.</span>
                         <h4>Principais Ganhos</h4><span>Autoconhecimento; desafios próximos da realidade que permitem uma análise objetiva e fidedigna do profissional.</span>
                         <h4>Análise de Competências</h4><span>Gestão de Equipes.  Liderança. Gestão de Talentos. Foco em resultados. Solução de Problemas. Inovação.</span>";
            Tipo = "Game (eguru)";
            Custo = 100;
            CustoLabel = "R$ 100,00";
            PrecoIlimitado = 10000;
            PrecoIlimitadoLabel = "R$ 10.000,00";
            Link = string.Format("{0}{1}{2}", ConfigurationManager.AppSettings["LinkPadraoDoPacoteScorm"], IdDoConteudo, "/");
            CargaHoraria = "01:30h";
        }

        protected override List<PalavraChave> GerarPalavrasChave()
        {
            return new List<PalavraChave>
                       {
                           Tags.Game(IdDoConteudo),

                           Tags.Lideranca(IdDoConteudo),

                           Tags.GestaoDeEquipes(IdDoConteudo),

                           Tags.Motivacao(IdDoConteudo),

                           Tags.Engajamento(IdDoConteudo),

                           Tags.Produtividade(IdDoConteudo)
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
                                   IdDoPreview = Guid.Parse("ea5cf9c9-4c82-4102-822d-68270fca7d7d"),
                                   Titulo = "Leader Noir",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("548e7d1b-68a8-470f-925d-d05bdf24f5f4"),
                                   Titulo = "Leader Noir",
                               }
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

        protected override List<CompetenciaParaOCliente> GerarCompetenciasParaOCliente()
        {
            return new List<CompetenciaParaOCliente>
            {
                CompetenciasCliente.TrabalhoEmEquipe(IdDoConteudo, "3-5", "4-5", "5"),

                CompetenciasCliente.SolucaoDeProblemas(IdDoConteudo, "3", "4", "4-5"),

                CompetenciasCliente.Lideranca(IdDoConteudo, "3", "4", "4"),

                CompetenciasCliente.GestaoDePessoas(IdDoConteudo, "2-5", "3", "3-5"),

                CompetenciasCliente.Negociacao(IdDoConteudo, "2-5", "3", "3"),

                CompetenciasCliente.FocoEmResultados(IdDoConteudo, "2", "2-5", "2"),
                
            };
        }

        protected override List<CompetenciaParaSuaEmpresa> GerarCompetenciasParaSuaEmpresa()
        {
            return new List<CompetenciaParaSuaEmpresa>
            {
                CompetenciasEmpresa.Flexibilidade(IdDoConteudo, "3", "4", "5"),

                CompetenciasEmpresa.FocoNoCliente(IdDoConteudo, "2", "3", "4"),

                CompetenciasEmpresa.Negociacao(IdDoConteudo, "1", "2", "2"),

                CompetenciasEmpresa.Relacionamento(IdDoConteudo, "1-5", "2", "2")
            };
        }
    }
}

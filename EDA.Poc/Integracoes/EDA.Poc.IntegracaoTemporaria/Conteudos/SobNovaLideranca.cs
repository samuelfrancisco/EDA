using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class SobNovaLideranca : ConteudoBase
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

        public SobNovaLideranca()
        {
            IdDoConteudo = Guid.Parse("1607a64b-5cd0-4ac1-9e9f-43eeec750958");
            Titulo = "Sob Nova Liderança";
            Objetivo = "Mapear os pontos fortes e a desenvolver quanto as competências de liderança.";
            Descricao = "Neste game, o participante assume a coordenação de uma área e tem como objetivo entregar resultados concretos para a empresa fazendo uma boa gestão da equipe e dos recursos, sempre com foco em atender aos seus clientes (internos e externos).";
            Conteudo = @"<h4>Público-Alvo</h4><span>Profissionais que ocupam e estão se preparando para ocupar posições de liderança que desejam saber seu grau de prontidão para liderança.</span>
                         <h4>Por Que Utilizar Este Game?</h4><span>Game inspirado no pipeline de liderança do Ram Charam, que aborda os desafios da transformação do líder de si mesmo para líder de equipe. Oferece ao participante a oportunidade de enfrentar desafios próximos daqueles que poderá encontrar na sua função ou ao longo de sua carreira.</span>
                         <h4>Principais Ganhos</h4><span>Verificação da prontidão para a liderança. Identificação dos pontos fortes e pontos a desenvolver. Vivência de desafios próximos à realidade que permitem uma análise objetiva do profissional avaliado.</span>
                         <h4>Análise de Competências</h4><span>Trabalho em Equipe. Solução de Problemas. Liderança. Gestão de Pessoas. Negociação. Foco em Resultados.</span>";
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
                           Tags.Lideranca(IdDoConteudo),

                           Tags.Assessment(IdDoConteudo),

                           Tags.Game(IdDoConteudo),

                           Tags.Pipeline(IdDoConteudo),

                           Tags.Prontidao(IdDoConteudo),
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
                                   IdDoPreview = Guid.Parse("c4ce2170-cc73-47af-95ff-e560e3792090"),
                                   Titulo = "Sob Nova Liderança",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("920d8f12-3ab9-406f-8fdb-7845498aa679"),
                                   Titulo = "Sob Nova Liderança",
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

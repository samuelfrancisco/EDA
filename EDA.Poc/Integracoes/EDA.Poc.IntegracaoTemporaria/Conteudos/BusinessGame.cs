using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class BusinessGame : ConteudoBase
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

        public BusinessGame()
        {
            IdDoConteudo = Guid.Parse("be04a850-a19c-4aab-b67a-1b6de7589b07");
            Titulo = "Business Game";
            Objetivo = "Analisar competências de candidatos a posições de estágio e trainee.";
            Descricao = "Neste game, o participante poderá mostrar sua capacidade de transformar sua empresa em uma organização mais eficiente e lucrativa. Suas decisões e habilidade serão decisivas.";
            Conteudo = @"<h4>Público Alvo</h4><span>Trainees e estagiários.</span>
                         <h4>Por Que Utilizar Esse Game?</h4><span>Na maioria das vezes, os processos seletivos, em sua fase final, dependem da observação da interação entre os candidatos para que os consultores possam analisar o perfil de cada um. A utilização de um game como mediador estimula, de forma engajadora, o debate dos objetivos propostos, criando um ambiente natural para troca de experiências.</span>
                         <h4>Principais Ganhos</h4><span>Análise e observação de competências. Agilidade e consistência na apresentação de dados, minimizando a subjetividade das escolhas. Enredo engajador e motivador que estimula a interação e explora de forma altamente assertiva os comportamentos observáveis.</span>
                         <h4>Análise de Competências</h4><span>Comunicação. Flexibilidade. Mudança e Inovação. Trabalho em Equipe. Solução de Problemas. Liderança. Relacionamento. Negociação e Influência. Capacidade de Lidar com Risco e Pressão. Capacidade Analítica de Dados e Resultados.</span>";
            Tipo = "Game (eguru)";
            Custo = 100;
            CustoLabel = "R$ 100,00";
            PrecoIlimitado = 10000;
            PrecoIlimitadoLabel = "R$ 10.000,00";
            Link = string.Format("{0}{1}{2}", ConfigurationManager.AppSettings["LinkPadraoDoPacoteScorm"], IdDoConteudo, "/");
            CargaHoraria = "03:00h";
        }

        protected override List<PalavraChave> GerarPalavrasChave()
        {
            return new List<PalavraChave>
                       {
                           Tags.Trainee(IdDoConteudo),
                           
                           Tags.Estagio(IdDoConteudo),
                           
                           Tags.Competencias(IdDoConteudo),

                           Tags.Game(IdDoConteudo),

                           Tags.Assessment(IdDoConteudo)
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
                                   IdDoPreview = Guid.Parse("284c5440-1d1e-4daf-a4ad-44c98754af27"),
                                   Titulo = "Trailer",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("7071c92b-1384-4692-aa38-fafd97597475"),
                                   Titulo = "Roleta",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("b3c123fe-4b09-4370-b20a-4d70556d3b34"),
                                   Titulo = "Qual é o negócio?",
                               }
                       };
        }

        protected override List<CompetenciaParaOCliente> GerarCompetenciasParaOCliente()
        {
            return new List<CompetenciaParaOCliente>
            {
                CompetenciasCliente.Comunicacao(IdDoConteudo, "3-5", "4-5", "5"),

                CompetenciasCliente.Flexibilidade(IdDoConteudo, "3", "4", "4-5"),

                CompetenciasCliente.MudancaEInovacao(IdDoConteudo, "3", "4", "4"),

                CompetenciasCliente.TrabalhoEmEquipe(IdDoConteudo, "2-5", "3", "3-5"),

                CompetenciasCliente.SolucaoDeProblemas(IdDoConteudo, "2-5", "3", "3"),

                CompetenciasCliente.Lideranca(IdDoConteudo, "2", "2-5", "2"),

                CompetenciasCliente.Relacionamento(IdDoConteudo, "1-5", "2", "2"),

                CompetenciasCliente.NegociacaoEInfluencia(IdDoConteudo, "1-5", "2", "1"),

                CompetenciasCliente.CapacidadeDeLidarComRiscoEPressao(IdDoConteudo, "1", "1", "1"),

                CompetenciasCliente.CapacidadeAnaliticaDeDadosEResultados(IdDoConteudo, "0", "0", "1"),
            };
        }

        protected override List<CompetenciaParaSuaEmpresa> GerarCompetenciasParaSuaEmpresa()
        {
            return new List<CompetenciaParaSuaEmpresa>
            {
                CompetenciasEmpresa.Comunicacao(IdDoConteudo, "4", "4-5", "5"),

                CompetenciasEmpresa.Flexibilidade(IdDoConteudo, "3-5", "4", "4-5"),

                CompetenciasEmpresa.MudancaEInovacao(IdDoConteudo, "3", "4", "4"),

                CompetenciasEmpresa.TrabalhoEmEquipe(IdDoConteudo, "2-5", "3", "3-5"),

                CompetenciasEmpresa.SolucaoDeProblemas(IdDoConteudo, "2", "3", "3"),

                CompetenciasEmpresa.Lideranca(IdDoConteudo, "1", "2-5", "2"),

                CompetenciasEmpresa.Relacionamento(IdDoConteudo, "0", "2", "2"),

                CompetenciasEmpresa.NegociacaoEInfluencia(IdDoConteudo, "0", "2", "1"),

                CompetenciasEmpresa.CapacidadeDeLidarComRiscoEPressao(IdDoConteudo, "0", "1", "1"),

                CompetenciasEmpresa.CapacidadeAnaliticaDeDadosEResultados(IdDoConteudo, "0", "0", "1"),
            };
        }
    }
}

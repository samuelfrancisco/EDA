using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class LojaDosHerois : ConteudoBase
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

        public LojaDosHerois()
        {
            IdDoConteudo = Guid.Parse("ec992e96-180f-4055-b37c-ab90f4db1437");
            Titulo = "Pagamentos Dos Herois";
            Objetivo = "Analisar competências técnicas e comportamentais de candidatos e profissionais para a área do Varejo.";
            Descricao = "Neste game, o participante tem uma loja em uma região que vive sendo atacada por seres estranhos e monstros. Aspirantes a heróis o procuram em busca de armas e acessórios para enfrentar esses desafios. O participante deverá entender seus perfis, necessidades e oferecer o item certo para o sucesso dos clientes nas suas missões e batalhas.";
            Conteudo = @"<h4>Público-Alvo</h4><span>Profissionais do Varejo.</span>
                         <h4>Por Que Utilizar Este Game?</h4><span>O setor de Varejo é com certeza um dos setores que mais emprega e atrai profissionais com foco em vendas e atendimento. Exatamente por ser uma vitrine e ser composto por empresas de segmentos tão diferentes, é fundamental entender os pontos fortes dos candidatos para uma escolha mais assertiva; bem como as competências dos profissionais avaliados para uma análise e um redirecionamento mais adequados.</span>
                         <h4>Principais Ganhos</h4><span>Maior produtividade e agilidade na integração. Perfil mais alinhado ao segmento, aos desafios propostos e à cultura organizacional. Diminuição do turnover. Análises mais coerentes para geração de planos de desenvolvimento mais assertivos.</span>
                         <h4>Análise de Competências</h4><span>Comunicação. Flexibilidade. Solução de Problemas. Foco no Cliente. Relacionamento. Negociação. Foco em Resultados.</span>";
            Tipo = "Game (eguru)";
            Custo = 100;
            CustoLabel = "R$ 100,00";
            PrecoIlimitado = 10000;
            PrecoIlimitadoLabel = "R$ 10.000,00";
            Link = string.Format("{0}{1}{2}", ConfigurationManager.AppSettings["LinkPadraoDoPacoteScorm"], IdDoConteudo, "/");
            CargaHoraria = "00:45h";
        }

        protected override List<PalavraChave> GerarPalavrasChave()
        {
            return new List<PalavraChave>
                       {
                           Tags.Venda(IdDoConteudo),

                           Tags.Varejo(IdDoConteudo),

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
                                   IdDoPreview = Guid.Parse("50b782d2-3587-46e0-b77c-7a49d0839a7e"),
                                   Titulo = "Pagamentos Dos Heróis",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("dfd958bb-d977-48ad-b6ad-01038515a959"),
                                   Titulo = "Pagamentos Dos Heróis",
                               }
                       };
        }

        protected override List<Semelhante> ObterSemelhantes()
        {
            return new List<Semelhante>()
            {
                new AReliquiaIndiana().ToSemelhante(),

                new BusinessGame().ToSemelhante()
            };
        }

        protected override List<CompetenciaParaOCliente> GerarCompetenciasParaOCliente()
        {
            return new List<CompetenciaParaOCliente>
            {
                CompetenciasCliente.TrabalhoEmEquipe(IdDoConteudo, "3-5", "4", "5"),

                CompetenciasCliente.SolucaoDeProblemas(IdDoConteudo, "3", "3", "4"),

                CompetenciasCliente.Lideranca(IdDoConteudo, "2", "2", "2"),

                CompetenciasCliente.GestaoDeEquipes(IdDoConteudo, "1-5", "2", "2"),

                CompetenciasCliente.Negociacao(IdDoConteudo, "1", "2", "2"),

                CompetenciasCliente.FocoEmResultados(IdDoConteudo, "0", "1", "1"),
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

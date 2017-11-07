using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class Os4CantosDoMundo : ConteudoBase
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

        public Os4CantosDoMundo()
        {
            IdDoConteudo = Guid.Parse("1eb06bdb-bf59-4285-a20d-16bd030dfe3e");
            Titulo = "Os 4 Cantos Do Mundo";
            Objetivo = "Analisar competências técnicas e comportamentais de jovens profissionais.";
            Descricao = "Nesse game, o participante assume o papel de um jornalista recém contratado por uma revista do segmento de viagens. Com o objetivo de produzir uma matéria sobre quatro diferentes regiões do mundo, ele sai em uma grande aventura em busca de informações e boas histórias para contar.";
            Conteudo = @"<h4>Público-Alvo</h4><span>Estagiários.</span>
                         <h4>Por Que Utilizar Este Game?</h4><span>Empresas de diferentes segmentos, tamanhos e culturas buscam estagiários e jovens talentos para integrar suas equipes. Identificar o perfil comportamental e algumas habilidades técnicas essenciais permite uma avaliação assertiva do grau de prontidão dos candidatos, o que contribui para a aceleração do processo de adaptação à cultura da Organização e para o alinhamento do perfil desses futuros profissionais aos desafios propostos.</span>
                         <h4>Principais Ganhos</h4><span>Potencialização da adesão à cultura organizacional. Agilidade na integração e aceleração da produtividade Maior alinhamento do perfil aos desafios propostos. Impacto positivo na redução do turnover.</span>
                         <h4>Análise de Competências</h4><span>Comunicação. Flexibilidade. Trabalho em Equipe. Relacionamento. Português. Raciocínio Lógico, Inglês.</span>";
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
                           Tags.Estagio(IdDoConteudo),

                           Tags.Competencias(IdDoConteudo),

                           Tags.Game(IdDoConteudo),

                           Tags.Assessment(IdDoConteudo),
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
                                   IdDoPreview = Guid.Parse("71142c87-e1ad-442a-ae19-aa5ecfdf232c"),
                                   Titulo = "Perfil",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("2b63988d-8b54-4233-af2c-94bb83a0aeac"),
                                   Titulo = "Perfil",
                               }
                       };
        }

        protected override List<Semelhante> ObterSemelhantes()
        {
            return new List<Semelhante>()
            {
                new LojaDosHerois().ToSemelhante(),

                new BusinessGame().ToSemelhante()
            };
        }

        protected override List<CompetenciaParaOCliente> GerarCompetenciasParaOCliente()
        {
            return new List<CompetenciaParaOCliente>
            {
                CompetenciasCliente.Comunicacao(IdDoConteudo, "4-5", "5", "5"),

                CompetenciasCliente.Flexibilidade(IdDoConteudo, "4", "5", "4"),

                CompetenciasCliente.TrabalhoEmEquipe(IdDoConteudo, "4", "4", "4"),

                CompetenciasCliente.Relacionamento(IdDoConteudo, "3", "3", "3"),

                CompetenciasCliente.Portugues(IdDoConteudo, "3", "3", "2"),

                CompetenciasCliente.RaciocinioLogico(IdDoConteudo, "2-5", "2", "2"),

                CompetenciasCliente.Ingles(IdDoConteudo, "2", "1", "1"),
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

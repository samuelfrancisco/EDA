using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class SuperFive : ConteudoBase
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

        public SuperFive()
        {
            IdDoConteudo = Guid.Parse("e2dbfc20-a5f9-4a4a-87a4-5f997fc26ff3");
            Titulo = "Super Five";
            Objetivo = "Apresentar, divulgar, consolidar ou checar novas informações e conceitos utilizando um aplicativo que gera aplicabilidade, diversão e competitividade.";
            Descricao = "Através de um desafio dinâmico e competitivo, o participante terá acesso a um grande número de novas informações. De forma divertida, ele conseguirá medir o seu conhecimento sobre os temas propostos e desafiar seus amigos em busca do topo do pódio.";
            Conteudo = @"<h4>Público-Alvo</h4><span>Todos os públicos.</span>
                         <h4>Por Que Utilizar Este Game?</h4><span>Sempre que temas devem ser divulgados a diversos públicos, seja em um evento, uma campanha, um lançamento ou processo de apresentação de uma ideia, de um produto ou de um propósito, o uso do Super Five é um diferencial sempre que o objetivo for compartilhar um tema com diversos públicos, seja em um evento, campanha, lançamento, ou ainda em processos de apresentação de uma ideia, produto ou propósito. Exatamente por conseguir grande engajamento com seu conceito divertido e competitivo, quebrando paradigmas e fomentando  a essência da gamificação de forma simples e eficaz.</span>
                         <h4>Principais Ganhos</h4><span>Agilidade na divulgação de conceitos. Engajamento dos participantes. Rastreabilidade dos dados, o que permite análises importantes visando à manutenção ou mudança na direção das ações. Abordagem de um número ilimitado de temas ou conteúdos. Gamificação da educação.</span>
                         <h4>Análise de Competências</h4><span>Conteúdo relacionado aos temas propostos.</span>";
            Tipo = "Game (eguru)";
            Custo = 100;
            CustoLabel = "R$ 100,00";
            PrecoIlimitado = 10000;
            PrecoIlimitadoLabel = "R$ 10.000,00";
            Link = string.Format("{0}{1}{2}", ConfigurationManager.AppSettings["LinkPadraoDoPacoteScorm"], IdDoConteudo, "/");
            CargaHoraria = "00:05h";
        }

        protected override List<PalavraChave> GerarPalavrasChave()
        {
            return new List<PalavraChave>
                       {
                           Tags.APP(IdDoConteudo),

                           Tags.LancamentoDeProdutos(IdDoConteudo),

                           Tags.Educacao(IdDoConteudo),

                           Tags.Gamificacao(IdDoConteudo),

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
                                   IdDoPreview = Guid.Parse("1cc68977-d4d7-4df1-bceb-a4a5c96d1aba"),
                                   Titulo = "Super Five",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("cb13153a-bfc0-4ad3-ab38-8c31c71220e6"),
                                   Titulo = "Super Five",
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

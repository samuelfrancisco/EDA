using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class OLiderDoJogo : ConteudoBase
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

        public OLiderDoJogo()
        {
            IdDoConteudo = Guid.Parse("cd53eafd-510b-4b77-bdf7-1f094ba0a9dd");
            Titulo = "O Líder do Jogo";
            Objetivo = "Ajudar o participante a entender o conceito de liderança situacional  de forma prática e estimulante.";
            Descricao = "Neste game o participante assumirá a liderança de uma equipe de quatro desenvolvedores em uma empresa de criação de jogos, a Innovative Games. Sua missão é entregar o projeto de um novo jogo em 3 meses, tomando decisões que equilibrem seus principais indicadores: Resultado e Clima da Equipe, e os indicadores secundários: Tempo e Orçamento.";
            Conteudo = @"<h4>Público-Alvo</h4><span>Gestores de Pessoas e Ocupantes de posições de 1ª liderança.</span>
                         <h4>Por Que Utilizar Este Game?</h4><span>Esse game mostra que o líder precisa alcançar resultados e manter um bom clima dentro de sua equipe nas mais diversas situações, lidando com  restrições de tempo e orçamento. A compreensão de que existem diferentes estilos de liderança e que todos precisam ser usados em momentos distintos com perfis de colaboradores diferentes, ajuda na melhoria da competência do líder e na sua capacidade de levar a equipe a alcançar os melhores resultados. (Baseado nos conceitos de Liderança Situacional apresentados no artigo Leadership that Gets Results, de Daniel Goleman, publicado pela Harvard Business Review).</span>
                         <h4>Principais Ganhos</h4><span>Autoconhecimento. Identificação do perfil de liderança segundo o conceito de Liderança Situacional.</span>
                         <h4>Análise de Competências</h4><span>Liderança Situacional. Gestão de Equipes; Gestão de tempo. Solução de Problemas. Foco em Resultados.</span>";
            Tipo = "Game (eguru)";
            Custo = 100;
            CustoLabel = "R$ 100,00";
            PrecoIlimitado = 10000;
            PrecoIlimitadoLabel = "R$ 10.000,00";
            Link = string.Format("{0}{1}{2}", ConfigurationManager.AppSettings["LinkPadraoDoPacoteScorm"], IdDoConteudo, "/");
            CargaHoraria = "00:40h";
        }

        protected override List<PalavraChave> GerarPalavrasChave()
        {
            return new List<PalavraChave>
                       {
                           Tags.Game(IdDoConteudo),

                           Tags.Lideranca(IdDoConteudo),

                           Tags.LiderancaSituacional(IdDoConteudo),

                           Tags.GestaoDeEquipes(IdDoConteudo)
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
                                   IdDoPreview = Guid.Parse("cd740595-bd58-43f0-9685-634c377f03b8"),
                                   Titulo = "O Líder do Jogo",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("67b270cf-d0bc-456e-83c6-4ff19087c6db"),
                                   Titulo = "O Líder do Jogo",
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
                CompetenciasCliente.EstiloDeAcao(IdDoConteudo, "4", "5", "4"),

                CompetenciasCliente.Comunicacao(IdDoConteudo, "4", "4", "4"),

                CompetenciasCliente.Ritmo(IdDoConteudo, "3", "3", "2"),

                CompetenciasCliente.ComoLidarComReferenciasERegras(IdDoConteudo, "1", "2", "2"),
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

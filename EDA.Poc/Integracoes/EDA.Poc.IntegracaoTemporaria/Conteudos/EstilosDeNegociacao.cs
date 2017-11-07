using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class EstilosDeNegociacao : ConteudoBase
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

        public EstilosDeNegociacao()
        {
            IdDoConteudo = Guid.Parse("4b548b9b-1faf-4f9c-b5df-3713d658cc41");
            Titulo = "Estilos de Negociação";
            Objetivo = "Desenvolver a autopercepção sobre as habilidades e estilos utilizados para negociação e adaptação aos diferentes perfis existentes.";
            Descricao = "Esse teste ajuda o participante a conhecer suas motivações e preferências, configurando um estilo próprio na relação com as pessoas e com os desafios a sua frente.";
            Conteudo = @"<h4>Público-Alvo</h4><span>Todos os públicos.</span>
                         <h4>Por Que Utilizar Este Game?</h4><span>Negociar é a arte de entender pontos de vista diferentes e conseguir acordos que sejam favoráveis a todos. O teste permite uma rápida compreensão tanto do perfil como do relacionamento com aqueles que naturalmente pensam e agem de forma diferente. Por essa razão o game pode contribuir para o processo de autoconhecimento dos participantes e levar à autorealização e ao alcance de melhores resultados.</span>
                         <h4>Principais Ganhos</h4><span>Ganho de tempo e produtividade. Autoconhecimento. Desenvolvimento de habilidades de negociação. Flexibilidade de pontos de vista. Comunicação e argumentação mais assertivas.</span>
                         <h4>Análise de Competências</h4><span>Perfil comportamental, estilo de negociação.</span>";
            Tipo = "Game (eguru)";
            Custo = 100;
            CustoLabel = "R$ 100,00";
            PrecoIlimitado = 10000;
            PrecoIlimitadoLabel = "R$ 10.000,00";
            Link = string.Format("{0}{1}{2}", ConfigurationManager.AppSettings["LinkPadraoDoPacoteScorm"], IdDoConteudo, "/");
            CargaHoraria = "00:30h";
        }

        protected override List<PalavraChave> GerarPalavrasChave()
        {
            return new List<PalavraChave>
                       {
                           Tags.Teste(IdDoConteudo),

                           Tags.Aprendizagem(IdDoConteudo),

                           Tags.Habilidades(IdDoConteudo),

                           Tags.Comportamento(IdDoConteudo),

                           Tags.Perfil(IdDoConteudo),

                           Tags.Negociacao(IdDoConteudo),

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
                                   IdDoPreview = Guid.Parse("d4ffae91-7605-45f0-89ba-941578c671f6"),
                                   Titulo = "Perfil",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("3fcefcfd-1889-4d45-a499-86104f398aab"),
                                   Titulo = "Perfil",
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
                CompetenciasCliente.PerfilDeNegociacao(IdDoConteudo, "4", "5", "2"),

                CompetenciasCliente.EstiloDeNegociacao(IdDoConteudo, "3", "4", "1"),
                
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

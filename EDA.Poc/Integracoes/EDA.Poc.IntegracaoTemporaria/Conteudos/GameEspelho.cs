using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class GameEspelho : ConteudoBase
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

        public GameEspelho()
        {
            IdDoConteudo = Guid.Parse("3173388e-86a8-480c-8227-ffdf62b6a7c2");
            Titulo = "Game Espelho";
            Objetivo = "Desenvolver os consultores de vendas em aspectos técnicos e comportamentais a partir da metodologia de venda consultiva.";
            Descricao = "Baseada na metodologia de venda consultiva do Alberto Couto, que tem experiência e reputação de mais de 30 anos no mercado desenvolvendo equipes comerciais, a Trilha é um programa inovador de formação e certificação dos profissionais de vendas através de uma rica experiência gamificada e online.";
            Conteudo = @"<h4>Público-Alvo</h4><span>Profissionais de áreas comerciais de todos os segmentos que desejam aumentar sua performance.</span>
                         <h4>Por Que Utilizar Este Game?</h4><span>Todos desejam melhores resultados em vendas. Com uma abordagem prática, que organiza conceitos importantes sempre conectados com exemplos aplicados, os participantes têm a oportunidade de refletir e praticar sobre suas ações cotidianas e aplicar alternativas comprovadamente eficazes pela metodologia de venda consultiva.</span>
                         <h4>Principais Ganhos</h4><span>Formação em conceitos de vendas. Aumento de vendas e produtividade. Mudança de postura frente aos clientes e frente ao alcance das metas.</span>
                         <h4>Análise de Competências</h4><span>Responsabilidade por resultados. Manejo de objeções. Negociação. Planejamento de visitas. Venda de soluções. Excelência no Atendimento.</span>";
            Tipo = "Game (eguru)";
            Custo = 100;
            CustoLabel = "R$ 100,00";
            PrecoIlimitado = 10000;
            PrecoIlimitadoLabel = "R$ 10.000,00";
            Link = string.Format("{0}{1}{2}", ConfigurationManager.AppSettings["LinkPadraoDoPacoteScorm"], IdDoConteudo, "/");
            CargaHoraria = "Em torno de 6 horas, com atividades de até 30 minutos.";
        }

        protected override List<PalavraChave> GerarPalavrasChave()
        {
            return new List<PalavraChave>
                       {
                           Tags.Lideranca(IdDoConteudo),

                           Tags.Venda(IdDoConteudo),

                           Tags.Consultiva(IdDoConteudo),

                           Tags.Assessment(IdDoConteudo),

                           Tags.Game(IdDoConteudo),

                           Tags.Trilha(IdDoConteudo),
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
                                   IdDoPreview = Guid.Parse("33028ab0-8edb-428f-9a82-bfd84854bd55"),
                                   Titulo = "Cliente Carente",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("6a592992-6f92-46e0-a2ef-47306ab627e9"),
                                   Titulo = "Cliente Carente",
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
                CompetenciasCliente.ResponsabilidadePorResultados(IdDoConteudo, "3-5", "4-5", "5"),

                CompetenciasCliente.ManejoDeObjecoes(IdDoConteudo, "3", "4", "4-5"),

                CompetenciasCliente.Negociacao(IdDoConteudo, "3", "4", "4"),

                CompetenciasCliente.PlanejamentoDeVisitas(IdDoConteudo, "2-5", "3", "3-5"),

                CompetenciasCliente.VendaDeSolucoes(IdDoConteudo, "2-5", "3", "3"),

                CompetenciasCliente.ExcelenciaNoAtendimento(IdDoConteudo, "2", "2-5", "2"),
                
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

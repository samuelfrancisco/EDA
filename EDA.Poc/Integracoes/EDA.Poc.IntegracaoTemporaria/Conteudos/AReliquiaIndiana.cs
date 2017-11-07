using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class AReliquiaIndiana : ConteudoBase
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

        public AReliquiaIndiana()
        {
            IdDoConteudo = Guid.Parse("fea57552-d911-43df-b0ef-1558f7ebbc42");
            Titulo = "A Reliquia Indiana";
            Objetivo = "Analisar competências dos profissionais de Call Center.";
            Descricao = "Neste game, Rajesh é um excêntrico milionário indiano que lançou um desafio com equipes do mundo inteiro. Em uma cidade arqueológica do seu país, as equipes devem buscar uma preciosa relíquia indiana que está perdida e aquela que se destacar e conseguir encontrá-la ganhará o prêmio máximo das mãos de Rajesh.";
            Conteudo = @"<h4>Público-Alvo</h4><span>Profissionais ou candidatos a posições de Call Center</span>
                         <h4>Por Que Utilizar Este Game?</h4><span>A área de Call Center ainda é a porta de entrada de grande parte dos profissionais que ingressam no mercado de trabalho. Identificar seu estilo comportamental facilita sua inserção nessa posição inicial; apresenta pontos de atenção visando um plano de desenvolvimento para quem já atua na área; bem como também ajuda a identificar potenciais habilidades para posições futuras.</span>
                         <h4>Principais Ganhos</h4><span>Verificação da prontidão para o exercício da função, com foco no atendimento com excelência, e alinhamento do perfil à função.</span>
                         <h4>Análise de Competências</h4><span>Flexibilidade. Foco no Cliente. Negociação. Relacionamento.</span>";
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
                           Tags.CallCenter(IdDoConteudo),

                           Tags.Assessment(IdDoConteudo),

                           Tags.Flexibilidade(IdDoConteudo),

                           Tags.Competencias(IdDoConteudo),

                           Tags.Game(IdDoConteudo),

                           Tags.Atendimento(IdDoConteudo)
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
                                   IdDoPreview = Guid.Parse("66c7a826-a709-40a9-a514-c1b9deb53638"),
                                   Titulo = "A Reliquia Indiana",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("0d84c9a2-f362-456d-ba48-d1375127c3d9"),
                                   Titulo = "A Reliquia Indiana",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("eb070c85-80ef-41d7-9cae-3e0825d8e36a"),
                                   Titulo = "A Reliquia Indiana",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("2f5b472d-bdd5-4f4a-aa96-c972e6aa6c69"),
                                   Titulo = "A Reliquia Indiana",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("8e110ff6-90f6-4b52-a550-882f0c3d5b2a"),
                                   Titulo = "A Reliquia Indiana",
                               }
                       };
        }

        protected override List<MaisDoFornecedor> ObterMaisDoFornecedor()
        {
            return new List<MaisDoFornecedor>()
            {
                new Crencas().ToMaisDoFornecedor(),

                new EntendendoAsCrencas().ToMaisDoFornecedor()
            };
        }

        protected override List<Semelhante> ObterSemelhantes()
        {
            return new List<Semelhante>()
            {
                new EntendendoAsCrencas().ToSemelhante(),

                new ManejandoObjecoes().ToSemelhante()
            };
        }

        protected override List<CompetenciaParaOCliente> GerarCompetenciasParaOCliente()
        {
            return new List<CompetenciaParaOCliente>
            {
                CompetenciasCliente.Flexibilidade(IdDoConteudo, "3-5", "4", "5"),

                CompetenciasCliente.FocoNoCliente(IdDoConteudo, "3", "3", "4"),

                CompetenciasCliente.Negociacao(IdDoConteudo, "2", "2", "2"),

                CompetenciasCliente.Relacionamento(IdDoConteudo, "1-5", "2", "2")
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

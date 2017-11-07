using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class TCM : ConteudoBase
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

        public TCM()
        {
            IdDoConteudo = Guid.Parse("a8f31cfe-f12d-495d-9880-fb667b74ae7b");
            Titulo = "TCM";
            Objetivo = "Mapeamento das principais tendências comportamentais e motivadores dos profissionais.";
            Descricao = "Todos nós temos tendências comportamentais mais marcantes que definem nossos motivadores e formas de agir. Esse teste mapeia essas tendências com base na metodologia DISC.";
            Conteudo = @"<h4>Público-Alvo</h4><span>Todos os públicos.</span>
                         <h4>Por Que Utilizar Este Game?</h4><span>Permite de forma rápida e objetiva reconhecer padrões de comportamento normalmente utilizados pelas pessoas e seus consequentes motivadores. Pode ser utilizado no processo de autoconhecimento e no assessment de um profissional ou de uma equipe visando à análise de perfil para um processo de seleção, de promoção ou sucessão, levando à melhor adaptação de candidatos à cultura e desafios das diferentes organizações.</span>
                         <h4>Principais Ganhos</h4><span>Autoconhecimento, percepção e análise das principais tendências comportamentais relativas à dominância, influência, estabilidade e conformidade.</span>
                         <h4>Análise de Competências</h4><span>Estilo de ação,  tomada de decisão, comunicação, ritmo e como lidar com referências e regras.</span>";
            Tipo = "Game (eguru)";
            Custo = 100;
            CustoLabel = "R$ 100,00";
            PrecoIlimitado = 10000;
            PrecoIlimitadoLabel = "R$ 10.000,00";
            Link = string.Format("{0}{1}{2}", ConfigurationManager.AppSettings["LinkPadraoDoPacoteScorm"], IdDoConteudo, "/");
            CargaHoraria = "00:15h";
        }

        protected override List<PalavraChave> GerarPalavrasChave()
        {
            return new List<PalavraChave>
                       {
                           Tags.Teste(IdDoConteudo),

                           Tags.Autoconhecimento(IdDoConteudo),

                           Tags.Comportamento(IdDoConteudo),

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
                                   IdDoPreview = Guid.Parse("0f440174-1146-42fb-94dd-45c67d81d9eb"),
                                   Titulo = "TCM",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("4052269e-817b-4b92-8b72-2d37daf4d4cc"),
                                   Titulo = "TCM",
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

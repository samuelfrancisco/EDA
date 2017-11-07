using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class MultiplasInteligencias : ConteudoBase
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

        public MultiplasInteligencias()
        {
            IdDoConteudo = Guid.Parse("e69f09ba-a12c-4b46-96ea-45715d60e084");
            Titulo = "Múltiplas Inteligências";
            Objetivo = "Desenvolver a autopercepção sobre as diversas habilidades que cada pessoa possui.";
            Descricao = "Cada pessoa possui habilidades, preferências e interesses diversos. Nesse game, cada um poderá explicitar suas escolhas e através delas conhecer como estão distribuídas as suas múltiplas inteligências.";
            Conteudo = @"<h4>Público-Alvo</h4><span>Todos os públicos.</span>
                         <h4>Por Que Utilizar Este Game?</h4><span>A compreensão das preferências, habilidades e estilo de aprendizagem torna o processo de desenvolvimento mais ágil, leve e com melhores resultados, o que contribui para o aumento do interesse, do engajamento e da busca constante pelo autodesenvolvimento.</span>
                         <h4>Principais Ganhos</h4><span>Autoconhecimento e percepção do estilo de aprendizagem que pode ser linguístico, lógico matemático, musical, corporal-cinestésico, espacial-visual, interpessoal ou intrapessoal.</span>
                         <h4>Análise de Competências</h4><span>Estilos de aprendizagem.</span>";
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

                           Tags.Aprendizagem(IdDoConteudo),

                           Tags.Habilidades(IdDoConteudo),

                           Tags.Inteligencia(IdDoConteudo),

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
                                   IdDoPreview = Guid.Parse("b17c3be0-5718-4f9c-a6c9-8526230dd47c"),
                                   Titulo = "Multiplas Inteligências",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("2224c746-b6aa-4347-913e-34ecce7ca196"),
                                   Titulo = "Multiplas Inteligências",
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
                CompetenciasCliente.EstiloDeAprendizagem(IdDoConteudo, "3-5", "4", "5"),
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

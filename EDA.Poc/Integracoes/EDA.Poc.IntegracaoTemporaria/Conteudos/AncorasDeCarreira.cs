using System;
using System.Collections.Generic;
using System.Configuration;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public sealed class AncorasDeCarreira : ConteudoBase
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

        public AncorasDeCarreira()
        {
            IdDoConteudo = Guid.Parse("d64a5e23-367a-4ab5-8c2e-f82d3b3c2e8f");
            Titulo = "Âncoras De Carreira";
            Objetivo = "Desenvolver a auto percepção sobre a carreira dos participantes através da identificação de valores e habilidades essenciais.";
            Descricao = "O que o participante valoriza ajuda a definir o seu plano de carreira com mais assertividade. Fazer com que ele tenha consciência de seus motivadores é fundamental para o acompanhamento de sua trajetória e consequente alcance do sucesso profissional esperado.";
            Conteudo = @"<h4>Público-Alvo</h4><span>Todos os públicos.</span>
                         <h4>Por Que Utilizar Este Game?</h4><span>O autoconhecimento é essencial para quem busca uma carreira de sucesso. Conhecer os fatores motivacionais, as necessidades e preferências contribui para a escolha de desafios e organizações mais compatíveis aos diferentes perfis. Esse processo facilita a adesão à cultura organizacional e aos estilos de liderança imediata; possibilita melhor integração entre membros de equipes; leva  ao aumento do engajamento; à autorealização e ao alcance de melhores resultados.</span>
                         <h4>Principais Ganhos</h4><span>Conhecimento dos motivadores e valores essenciais e das âncoras de carreira (autonomia, segurança, competência técnico-funcional, competência gerencial, criatividade empreendedora, dedicação a uma causa, desafio puro e estilo de vida). Direcionamento da carreira e autoconhecimento.</span>
                         <h4>Análise de Competência</h4><span>Perfil comportamental e motivadores de carreira.</span>";
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

                           Tags.Carreira(IdDoConteudo),

                           Tags.AncorasDeCarreira(IdDoConteudo),

                           Tags.Motivadores(IdDoConteudo),

                           Tags.Valores(IdDoConteudo),

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
                                   IdDoPreview = Guid.Parse("5b424800-4adf-47dd-b7b3-069dfb51cdc7"),
                                   Titulo = "Ancoras de Carreira",
                               },
                           new Preview
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                   IdDoPreview = Guid.Parse("b3dd505a-fada-4e50-bb48-2c0d9a25f9f4"),
                                   Titulo = "Ancoras de Carreira",
                               }
                       };
        }

        protected override List<Semelhante> ObterSemelhantes()
        {
            return new List<Semelhante>()
            {
                new TCM().ToSemelhante(),

                new Objecoes().ToSemelhante()
            };
        }

        protected override List<CompetenciaParaOCliente> GerarCompetenciasParaOCliente()
        {
            return new List<CompetenciaParaOCliente>
            {
                CompetenciasCliente.PerfilComportamental(IdDoConteudo, "3", "4", "5"),

                CompetenciasCliente.MotivadoresDeCarreira(IdDoConteudo, "2", "3", "4"),
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

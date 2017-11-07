using System;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria
{
    public static class CompetenciasCliente
    {
        public static CompetenciaParaOCliente TrabalhoEmEquipe(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("cce7bea4-bd9d-4d03-bf11-bebaec3cc860"),
                Titulo = "Trabalho em equipe",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente PerfilComportamental(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("41da8cc2-cb0a-46b9-b6d1-924ccaf781a9"),
                Titulo = "Perfil Comportamental",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente MotivadoresDeCarreira(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("46ea04f8-c47e-4926-a66c-ffbd46688ab5"),
                Titulo = "Motivadores de Carreira",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente Vendas(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("47f5113a-e6b2-4cf1-b8e1-d4e9b7ffd3bc"),
                Titulo = "Vendas",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente FocoNoCliente(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("a917ffa0-68c0-4c24-b12e-96a3416c6ba5"),
                Titulo = "Foco no Cliente",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente GestaoDeEquipes(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("8b4bf020-c607-4e60-87c6-bad6bc7aa2dd"),
                Titulo = "Gestão de Equipes",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente GestaoDePessoas (Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("920d8f12-3ab9-406f-8fdb-7845498aa679"),
                Titulo = "Gestão de Pessoas",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente FocoEmResultados(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("019f94ab-c042-4805-8334-e8de8cb19693"),
                Titulo = "Foco em Resultados",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente Flexibilidade(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("1655bf79-fa30-476b-9175-d44c7ad0e966"),
                Titulo = "Flexibilidade",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente Negociacao(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("bde37624-2815-4713-90f0-64b6d923bd19"),
                Titulo = "Negociacao",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente Relacionamento(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("d9cd3419-6844-46b8-ab90-f2f25ae45f33"),
                Titulo = "Relacionamento",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente Comunicacao(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("0599e19a-35e0-416c-8c98-0c5a6d43bd5d"),
                Titulo = "Comunicação",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente MudancaEInovacao(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("5b87992d-3860-43da-9838-c614cbb048f3"),
                Titulo = "Mudança e Inovação",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente SolucaoDeProblemas(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("36386d56-270c-4493-80c6-957e4ff48dc3"),
                Titulo = "Solução de Problemas",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente Lideranca(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("d70a48d4-bc6d-457e-8f2c-23e13c543347"),
                Titulo = "Liderança",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente NegociacaoEInfluencia(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("ab185f87-23fa-4959-a1ae-405d3f82ea67"),
                Titulo = "Negociação e Influência",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente CapacidadeDeLidarComRiscoEPressao(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("06b99a17-3d14-486d-92e2-a0e5c8c10e2e"),
                Titulo = "Capacidade De Lidar Com Risco e Pressão",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente CapacidadeAnaliticaDeDadosEResultados(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("1276e92f-bc0f-4d80-9198-3ffea030688b"),
                Titulo = "Capacidade Analítica de Dados e Resultados",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente PerfilDeNegociacao(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("1ed15bd8-2eca-4d7d-9c9b-d2d8d8140242"),
                Titulo = "Perfil de Negociação",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente EstiloDeNegociacao(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("2d8fb8ff-ba12-425e-97ca-18ba3740d926"),
                Titulo = "Estilo de Negociação",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente Ritmo(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("858e988d-1645-4c24-9f11-f22dde81328b"),
                Titulo = "Ritmo",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente ComoLidarComReferenciasERegras(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("a11a92e8-d712-4baa-b343-c659f998911a"),
                Titulo = "Como Lidar Com Referências e Regras",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente EstiloDeAcao(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("858e988d-1645-4c24-9f11-f22dde81328b"),
                Titulo = "Estilo de Ação",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente EstiloDeAprendizagem(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("bad24434-c220-4c8c-91ef-ab4ea3d7d164"),
                Titulo = "Estilo de Aprendizagem",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente Portugues(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("363b45b4-a4ff-466e-87b5-222a7320ab2c"),
                Titulo = "Português",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente RaciocinioLogico(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("f77810ef-a771-4b69-b9ba-a763853d0616"),
                Titulo = "Raciocínio Lógico",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente Ingles(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("df639627-e94f-4d8b-a027-310f71b2f2f0"),
                Titulo = "Inglês",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente ResponsabilidadePorResultados(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("df639627-e94f-4d8b-a027-310f71b2f2f0"),
                Titulo = "Responsabilidade Por Resultados",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente ManejoDeObjecoes(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("df639627-e94f-4d8b-a027-310f71b2f2f0"),
                Titulo = "Manejo De Objeções",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente PlanejamentoDeVisitas(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("df639627-e94f-4d8b-a027-310f71b2f2f0"),
                Titulo = "Planejamento de Visitas",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente VendaDeSolucoes(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("df639627-e94f-4d8b-a027-310f71b2f2f0"),
                Titulo = "Venda de Soluções",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }

        public static CompetenciaParaOCliente ExcelenciaNoAtendimento(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaOCliente
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaCompetencia = Guid.Parse("df639627-e94f-4d8b-a027-310f71b2f2f0"),
                Titulo = "Excelência no Atendimento",
                Gaps = gap,
                Medidas = medida,
                Trabalhadas = trabalhada,
            };
        }
    }
}

using System;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria
{
    public static class CompetenciasEmpresa
    {
        public static CompetenciaParaSuaEmpresa TrabalhoEmEquipe(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaSuaEmpresa
                       {
                           IdDoConteudo = idDoConteudo,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           IdDaCompetencia = Guid.Parse("a078dd69-9b84-4dcb-9b1b-4a4776046686"),
                           Titulo = "Trabalho em equipe",
                           Gaps = gap,
                           Medidas = medida,
                           Trabalhadas = trabalhada,
                       };
        }

        public static CompetenciaParaSuaEmpresa Vendas(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaSuaEmpresa
                        {
                            IdDoConteudo = idDoConteudo,
                            SiglaDoIdioma = Constantes.SiglaDoIdioma,
                            IdDaCompetencia = Guid.Parse("bb7c8a72-571f-4afd-ae36-e7ff12b2447d"),
                            Titulo = "Vendas",
                            Gaps = gap,
                            Medidas = medida,
                            Trabalhadas = trabalhada,
                        };
        }

        public static CompetenciaParaSuaEmpresa FocoNoCliente(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaSuaEmpresa
                        {
                            IdDoConteudo = idDoConteudo,
                            SiglaDoIdioma = Constantes.SiglaDoIdioma,
                            IdDaCompetencia = Guid.Parse("bc94a641-cb49-4598-a0b2-aba8f5205cc3"),
                            Titulo = "Foco no Cliente",
                            Gaps = gap,
                            Medidas = medida,
                            Trabalhadas = trabalhada,
                        };
        }

        public static CompetenciaParaSuaEmpresa FocoEmResultados(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaSuaEmpresa
                        {
                            IdDoConteudo = idDoConteudo,
                            SiglaDoIdioma = Constantes.SiglaDoIdioma,
                            IdDaCompetencia = Guid.Parse("a0bda97f-eec2-4c62-b75b-fd96466010cb"),
                            Titulo = "Foco em Resultados",
                            Gaps = gap,
                            Medidas = medida,
                            Trabalhadas = trabalhada,
                        };
        }

        public static CompetenciaParaSuaEmpresa Flexibilidade(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaSuaEmpresa
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

        public static CompetenciaParaSuaEmpresa Negociacao(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaSuaEmpresa
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

        public static CompetenciaParaSuaEmpresa Relacionamento(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaSuaEmpresa
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


        public static CompetenciaParaSuaEmpresa Comunicacao(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaSuaEmpresa
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

        public static CompetenciaParaSuaEmpresa MudancaEInovacao(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaSuaEmpresa
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

        public static CompetenciaParaSuaEmpresa SolucaoDeProblemas(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaSuaEmpresa
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

        public static CompetenciaParaSuaEmpresa Lideranca(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaSuaEmpresa
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

        public static CompetenciaParaSuaEmpresa NegociacaoEInfluencia(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaSuaEmpresa
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

        public static CompetenciaParaSuaEmpresa CapacidadeDeLidarComRiscoEPressao(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaSuaEmpresa
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

        public static CompetenciaParaSuaEmpresa CapacidadeAnaliticaDeDadosEResultados(Guid idDoConteudo, string gap, string medida, string trabalhada)
        {
            return new CompetenciaParaSuaEmpresa
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
    }
}

namespace EGuru.MarketPlace.Loja.Consultas.Entity.Migrations
{
    using System.Data.Entity.Migrations;
    using Contextos;
    using ReadModel.PaginaInicial;
    using System;
    using System.Collections.Generic;
    using ReadModel.PaginaDeDetalhesDoConteudo;
    using ReadModel.WebAPI;

    internal sealed class Configuration : DbMigrationsConfiguration<LojaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        private readonly Random _random = new Random();

        private PaginaDeDetalhesDoConteudo AddDetalhe(System.Guid idDoCliente, Guid idDoUsuario, System.Guid idDoConteudo, System.Guid idEscola,  
            decimal avaliacao, string custoLabel, string tipo, string titulo, RecomendadoParaOCliente[] recomendados = null, MaisPopular[] maisPopulares = null)
        {
            var paginaDeDetalhes = new PaginaDeDetalhesDoConteudo()
            {
                IdDoConteudo = idDoConteudo,
            };

            paginaDeDetalhes.Detalhes = new List<Detalhes>()
            {
                new Detalhes {
                CargaHoraria = "02:00h",
                IdDoConteudo = idDoConteudo,
                PaginaDeDetalhes = paginaDeDetalhes,
                Objetivo = "Assim mesmo, o desenvolvimento contínuo de distintas formas de atuação representa uma abertura para a melhoria dos procedimentos normalmente adotados.",
                Conteudo = "É importante questionar o quanto a competitividade nas transações comerciais apresenta tendências no sentido de aprovar a manutenção das direções preferenciais no sentido do progresso.",
                Avaliacao = avaliacao,
                CustoLabel = custoLabel,
                IdDaEscola = idEscola,
                QuantidadeDeAquisicoes = _random.Next(10000),
                SiglaDoIdioma = "pt-BR",
                Tipo = tipo,
                Titulo = titulo
                }
            };

            paginaDeDetalhes.ConsolidadoAvaliacao = new ConsolidadoAvaliacao()
            {
                IdDoConteudo = idDoConteudo,
                QuantidadeAvaliacoes = 0,
                MediaDasAvaliacoesDoConteudo = 0,
                QuantidadeDeAvaliacoesPorValorDeAvaliacao = new Dictionary<int,int>() {{1,0},{2,0},{3,0},{4,0},{5,0}},
                PaginaDeDetalhes = paginaDeDetalhes
            };

            paginaDeDetalhes.Previews = new List<Preview>() {
                new Preview() {IdDoConteudo = idDoConteudo, PaginaDeDetalhes = paginaDeDetalhes, SiglaDoIdioma = "pt-BR", IdDoPreview = System.Guid.NewGuid(), Titulo="preview"},
                new Preview() {IdDoConteudo = idDoConteudo, PaginaDeDetalhes = paginaDeDetalhes, SiglaDoIdioma = "pt-BR", IdDoPreview = System.Guid.NewGuid(), Titulo="preview"},
                new Preview() {IdDoConteudo = idDoConteudo, PaginaDeDetalhes = paginaDeDetalhes, SiglaDoIdioma = "pt-BR", IdDoPreview = System.Guid.NewGuid(), Titulo="preview"},
                new Preview() {IdDoConteudo = idDoConteudo, PaginaDeDetalhes = paginaDeDetalhes, SiglaDoIdioma = "pt-BR", IdDoPreview = System.Guid.NewGuid(), Titulo="preview"},
                new Preview() {IdDoConteudo = idDoConteudo, PaginaDeDetalhes = paginaDeDetalhes, SiglaDoIdioma = "pt-BR", IdDoPreview = System.Guid.NewGuid(), Titulo="preview"},
                new Preview() {IdDoConteudo = idDoConteudo, PaginaDeDetalhes = paginaDeDetalhes, SiglaDoIdioma = "pt-BR", IdDoPreview = System.Guid.NewGuid(), Titulo="preview"},
                new Preview() {IdDoConteudo = idDoConteudo, PaginaDeDetalhes = paginaDeDetalhes, SiglaDoIdioma = "pt-BR", IdDoPreview = System.Guid.NewGuid(), Titulo="preview"},
            };

            paginaDeDetalhes.CompetenciasParaOCliente = new List<CompetenciaParaOCliente>(){
                new CompetenciaParaOCliente() {IdDoCliente = idDoCliente, IdDoConteudo = idDoConteudo, SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdDaCompetencia = System.Guid.NewGuid(),
                    Titulo = "Busca por Excelência", Gaps = _random.Next(5).ToString(), Medidas = _random.Next(5).ToString(), Trabalhadas = _random.Next(5).ToString()},
                new CompetenciaParaOCliente() {IdDoCliente = idDoCliente, IdDoConteudo = idDoConteudo, SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdDaCompetencia = System.Guid.NewGuid(),
                    Titulo = "Foco em Resultados", Gaps = _random.Next(5).ToString(), Medidas = _random.Next(5).ToString(), Trabalhadas = _random.Next(5).ToString()},
                new CompetenciaParaOCliente() {IdDoCliente = idDoCliente, IdDoConteudo = idDoConteudo, SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdDaCompetencia = System.Guid.NewGuid(),
                    Titulo = "Foco no Cliente", Gaps = _random.Next(5).ToString(), Medidas = _random.Next(5).ToString(), Trabalhadas = _random.Next(5).ToString()},
                new CompetenciaParaOCliente() {IdDoCliente = idDoCliente, IdDoConteudo = idDoConteudo, SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdDaCompetencia = System.Guid.NewGuid(),
                    Titulo = "Vendas", Gaps = _random.Next(5).ToString(), Medidas = _random.Next(5).ToString(), Trabalhadas = _random.Next(5).ToString()},
                new CompetenciaParaOCliente() {IdDoCliente = idDoCliente, IdDoConteudo = idDoConteudo, SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdDaCompetencia = System.Guid.NewGuid(),
                    Titulo = "Brilho nos Olhos", Gaps = _random.Next(5).ToString(), Medidas = _random.Next(5).ToString(), Trabalhadas = _random.Next(5).ToString()},
                new CompetenciaParaOCliente() {IdDoCliente = idDoCliente, IdDoConteudo = idDoConteudo, SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdDaCompetencia = System.Guid.NewGuid(),
                    Titulo = "Olhar como dono", Gaps = _random.Next(5).ToString(), Medidas = _random.Next(5).ToString(), Trabalhadas = _random.Next(5).ToString()}
            };

            paginaDeDetalhes.CompetenciasParaSuaEquipe = new List<CompetenciaParaSuaEquipe>(){
                new CompetenciaParaSuaEquipe() {IdDoConteudo = idDoConteudo, SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdDaCompetencia = System.Guid.NewGuid(),
                    Titulo = "Busca por Excelência", Gaps = _random.Next(5).ToString(), Medidas = _random.Next(5).ToString(), Trabalhadas = _random.Next(5).ToString()},
                new CompetenciaParaSuaEquipe() {IdDoConteudo = idDoConteudo, SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdDaCompetencia = System.Guid.NewGuid(),
                    Titulo = "Foco em Resultados", Gaps = _random.Next(5).ToString(), Medidas = _random.Next(5).ToString(), Trabalhadas = _random.Next(5).ToString()},
                new CompetenciaParaSuaEquipe() {IdDoConteudo = idDoConteudo, SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdDaCompetencia = System.Guid.NewGuid(),
                    Titulo = "Foco no Cliente", Gaps = _random.Next(5).ToString(), Medidas = _random.Next(5).ToString(), Trabalhadas = _random.Next(5).ToString()},
                new CompetenciaParaSuaEquipe() {IdDoConteudo = idDoConteudo, SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdDaCompetencia = System.Guid.NewGuid(),
                    Titulo = "Vendas", Gaps = _random.Next(5).ToString(), Medidas = _random.Next(5).ToString(), Trabalhadas = _random.Next(5).ToString()},
                new CompetenciaParaSuaEquipe() {IdDoConteudo = idDoConteudo, SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdDaCompetencia = System.Guid.NewGuid(),
                    Titulo = "Brilho nos Olhos", Gaps = _random.Next(5).ToString(), Medidas = _random.Next(5).ToString(), Trabalhadas = _random.Next(5).ToString()},
                new CompetenciaParaSuaEquipe() {IdDoConteudo = idDoConteudo, SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdDaCompetencia = System.Guid.NewGuid(),
                    Titulo = "Olhar como dono", Gaps = _random.Next(5).ToString(), Medidas = _random.Next(5).ToString(), Trabalhadas = _random.Next(5).ToString()}
            };

            if (recomendados != null)
            {
                int[] semelhantesIndex = { _random.Next(4), _random.Next(4) };

                paginaDeDetalhes.Semelhantes = new List<Semelhante>() {
                    new Semelhante() {SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdSemelhante = System.Guid.NewGuid(),
                        IdDoConteudo = recomendados[semelhantesIndex[0]].IdDoConteudo, Avaliacao = recomendados[semelhantesIndex[0]].Avaliacao, CustoLabel = recomendados[semelhantesIndex[0]].CustoLabel,
                        Tipo = recomendados[semelhantesIndex[0]].Tipo, Titulo = recomendados[semelhantesIndex[0]].Titulo},
                    new Semelhante() {SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdSemelhante = System.Guid.NewGuid(),
                        IdDoConteudo = recomendados[semelhantesIndex[1]].IdDoConteudo, Avaliacao = recomendados[semelhantesIndex[1]].Avaliacao, CustoLabel = recomendados[semelhantesIndex[1]].CustoLabel,
                        Tipo = recomendados[semelhantesIndex[1]].Tipo, Titulo = recomendados[semelhantesIndex[1]].Titulo},
                };

                int[] maisDoFornecedorIndex = { _random.Next(4), _random.Next(4) };

                paginaDeDetalhes.MaisDoFornecedor = new List<MaisDoFornecedor>()
                {
                    new MaisDoFornecedor() {SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdMaidDoFornecedor = System.Guid.NewGuid(),
                        IdDoConteudo = recomendados[maisDoFornecedorIndex[0]].IdDoConteudo, Avaliacao = recomendados[maisDoFornecedorIndex[0]].Avaliacao, CustoLabel = recomendados[maisDoFornecedorIndex[0]].CustoLabel,
                        Tipo = recomendados[maisDoFornecedorIndex[0]].Tipo, Titulo = recomendados[maisDoFornecedorIndex[0]].Titulo},
                    new MaisDoFornecedor() {SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdMaidDoFornecedor = System.Guid.NewGuid(),
                        IdDoConteudo = recomendados[maisDoFornecedorIndex[1]].IdDoConteudo, Avaliacao = recomendados[maisDoFornecedorIndex[1]].Avaliacao, CustoLabel = recomendados[maisDoFornecedorIndex[1]].CustoLabel,
                        Tipo = recomendados[maisDoFornecedorIndex[1]].Tipo, Titulo = recomendados[maisDoFornecedorIndex[1]].Titulo},
                };
            }
            else
            {
                int[] semelhantesIndex = { _random.Next(4), _random.Next(4) };

                paginaDeDetalhes.Semelhantes = new List<Semelhante>() {
                    new Semelhante() {SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdSemelhante = System.Guid.NewGuid(),
                        IdDoConteudo = maisPopulares[semelhantesIndex[0]].IdDoConteudo, Avaliacao = maisPopulares[semelhantesIndex[0]].Avaliacao, CustoLabel = maisPopulares[semelhantesIndex[0]].CustoLabel,
                        Tipo = maisPopulares[semelhantesIndex[0]].Tipo, Titulo = maisPopulares[semelhantesIndex[0]].Titulo},
                    new Semelhante() {SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdSemelhante = System.Guid.NewGuid(),
                        IdDoConteudo = maisPopulares[semelhantesIndex[1]].IdDoConteudo, Avaliacao = maisPopulares[semelhantesIndex[1]].Avaliacao, CustoLabel = maisPopulares[semelhantesIndex[1]].CustoLabel,
                        Tipo = maisPopulares[semelhantesIndex[1]].Tipo, Titulo = maisPopulares[semelhantesIndex[1]].Titulo},
                };

                int[] maisDoFornecedorIndex = { _random.Next(4), _random.Next(4) };

                paginaDeDetalhes.MaisDoFornecedor = new List<MaisDoFornecedor>()
                {
                    new MaisDoFornecedor() {SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdMaidDoFornecedor = System.Guid.NewGuid(),
                        IdDoConteudo = maisPopulares[maisDoFornecedorIndex[0]].IdDoConteudo, Avaliacao = maisPopulares[maisDoFornecedorIndex[0]].Avaliacao, CustoLabel = maisPopulares[maisDoFornecedorIndex[0]].CustoLabel,
                        Tipo = maisPopulares[maisDoFornecedorIndex[0]].Tipo, Titulo = maisPopulares[maisDoFornecedorIndex[0]].Titulo},
                    new MaisDoFornecedor() {SiglaDoIdioma = "pt-BR", PaginaDeDetalhes =  paginaDeDetalhes, IdMaidDoFornecedor = System.Guid.NewGuid(),
                        IdDoConteudo = maisPopulares[maisDoFornecedorIndex[1]].IdDoConteudo, Avaliacao = maisPopulares[maisDoFornecedorIndex[1]].Avaliacao, CustoLabel = maisPopulares[maisDoFornecedorIndex[1]].CustoLabel,
                        Tipo = maisPopulares[maisDoFornecedorIndex[1]].Tipo, Titulo = maisPopulares[maisDoFornecedorIndex[1]].Titulo},
                };
            }

            var precoIlimitado = _random.Next(2000, 5000);
            var precos = new Precos()
            {
                PrecoIlimitado = precoIlimitado,
                PrecoIlimitadoLabel = string.Format("R$ {0}/mês", precoIlimitado.ToString("0.00")),
                IdDoConteudo = idDoConteudo,
                PaginaDeDetalhes = paginaDeDetalhes,
            };

            precos.PrecosPorQuantidadeDeUsuarios = new List<PrecoPorQuantidadeDeUsuarios>()
                {
                    new PrecoPorQuantidadeDeUsuarios() {IdDoConteudo = idDoConteudo, IdPrecoPorQuantidadeDeUsuarios = System.Guid.NewGuid(), Precos = precos, 
                        Preco = _random.Next(8, 14), LicencasDe = 61},
                    new PrecoPorQuantidadeDeUsuarios() {IdDoConteudo = idDoConteudo, IdPrecoPorQuantidadeDeUsuarios = System.Guid.NewGuid(), Precos = precos, 
                        Preco = _random.Next(16, 22), LicencasDe = 31, LicencasAte = 60},
                    new PrecoPorQuantidadeDeUsuarios() {IdDoConteudo = idDoConteudo, IdPrecoPorQuantidadeDeUsuarios = System.Guid.NewGuid(), Precos = precos, 
                        Preco = _random.Next(24, 30), LicencasDe = 0, LicencasAte = 30},
                };

            paginaDeDetalhes.Precos = precos;

            return paginaDeDetalhes;
        }

        private Lancamento addLancamento(System.Guid idDoCliente, ReadModel.PaginaInicial.PaginaInicial paginaInicial){
            return new Lancamento()
            {
                Avaliacao = Math.Round(Convert.ToDecimal(_random.NextDouble() * 5), 2),
                Custo = _random.Next(300),
                CustoLabel = string.Format("R$ {0},00", _random.Next(300)),
                IdDoCliente = idDoCliente,
                IdDoConteudo = Guid.NewGuid(),
                IdDaEscola = Guid.NewGuid(),
                PaginaInicial = paginaInicial,
                SiglaDoIdioma = "pt-BR",
                Titulo = string.Format("Teste_{0}", _random.Next(300)),
                Tipo = "Teste Técnico (eguru)"
            };


        }

        private MaisPopular addMaisPopular(System.Guid idDoCliente, ReadModel.PaginaInicial.PaginaInicial paginaInicial)
        {
            return new MaisPopular()
            {
                Avaliacao = Math.Round(Convert.ToDecimal(_random.NextDouble() * 5), 2),
                Custo = _random.Next(300),
                CustoLabel = string.Format("R$ {0},00", _random.Next(300)),
                IdDoCliente = idDoCliente,
                IdDoConteudo = Guid.NewGuid(),
                IdDaEscola = Guid.NewGuid(),
                PaginaInicial = paginaInicial,
                SiglaDoIdioma = "pt-BR",
                Titulo = string.Format("Teste_{0}", _random.Next(300)),
                Tipo = "Teste Comportamental (eguru)"
            };
        }

        private RecomendadoParaOCliente addRecomendadosCliente(System.Guid idDoCliente, ReadModel.PaginaInicial.PaginaInicial paginaInicial)
        {

            return new RecomendadoParaOCliente()
            {
                Avaliacao = Math.Round(Convert.ToDecimal(_random.NextDouble() * 5), 2),
                Custo = _random.Next(300),
                CustoLabel = string.Format("R$ {0},00", _random.Next(300)),
                IdDoCliente = idDoCliente,
                IdDoConteudo = Guid.NewGuid(),
                IdDaEscola = Guid.NewGuid(),
                PaginaInicial = paginaInicial,
                SiglaDoIdioma = "pt-BR",
                Titulo = string.Format("Teste_{0}", _random.Next(300)),
                Tipo = "Game (eguru)"
            };
        }

        private RecomendadoParaSuaEmpresa addRecomendadosEmpresa(System.Guid idDoCliente, ReadModel.PaginaInicial.PaginaInicial paginaInicial)
        {
            return new RecomendadoParaSuaEmpresa()
            {
                Avaliacao = Math.Round(Convert.ToDecimal(_random.NextDouble() * 5), 2),
                Custo = _random.Next(300),
                CustoLabel = string.Format("R$ {0},00", _random.Next(300)),
                IdDoCliente = idDoCliente,
                IdDoConteudo = Guid.NewGuid(),
                IdDaEscola = Guid.NewGuid(),
                PaginaInicial = paginaInicial,
                SiglaDoIdioma = "pt-BR",
                Titulo = string.Format("Teste_{0}", _random.Next(300)),
                Tipo = "Game (eguru)"
            };
        }

        private RecomendadoParaSuaEquipe addRecomendadosEquipe(System.Guid idDoCliente, ReadModel.PaginaInicial.PaginaInicial paginaInicial)
        {
            return new RecomendadoParaSuaEquipe()
            {
                Avaliacao = Math.Round(Convert.ToDecimal(_random.NextDouble() * 5), 2),
                Custo = _random.Next(300),
                CustoLabel = string.Format("R$ {0},00", _random.Next(300)),
                IdDoCliente = idDoCliente,
                IdDoConteudo = Guid.NewGuid(),
                IdDaEscola = Guid.NewGuid(),
                PaginaInicial = paginaInicial,
                SiglaDoIdioma = "pt-BR",
                Titulo = string.Format("Teste_{0}", _random.Next(300)),
                Tipo = "Game (eguru)"
            };
        }

        private DestaqueDaSemana addDestaquesSemana(System.Guid idDoCliente, ReadModel.PaginaInicial.PaginaInicial paginaInicial)
        {
            return new DestaqueDaSemana()
            {
                IdDoCliente = idDoCliente,
                IdDaEscola = Guid.NewGuid(),
                PaginaInicial = paginaInicial,
                SiglaDoIdioma = "pt-BR",
                IdDaPromocao = Guid.NewGuid(),
                LarguraMinima = "600"
            };
        }

        private Licenca addLicenca(System.Guid idDoCliente, Lancamento lancamento)
        {
            var licenca = new Licenca()
            {
                IdDoCliente = idDoCliente,
                SiglaDoIdioma = "pt-BR",
                Titulo = lancamento.Titulo,
                QuantidadeDeUsuarios = _random.Next(100),
                Objetivo = "Desta maneira, a execução dos pontos do programa aponta para a melhoria de todos os recursos funcionais envolvidos.",
                Descricao = "As experiências acumuladas demonstram que o desenvolvimento contínuo de distintas formas de atuação não pode mais se dissociar do levantamento das variáveis envolvidas.",
                IdDaLicenca = System.Guid.NewGuid(),
                Banner = "http://loremflickr.com/1200/400/",
                ImagemIlustrativa = "http://loremflickr.com/300/300/",
                CargaHoraria = "02:00 hrs",
                IdDoConteudo = lancamento.IdDoConteudo,
                Link = "http://temp.teste.com.br",
                DataDeInicio = DateTime.Today.AddDays(_random.Next(10) - 5),
                QuantidadeDeMeses = _random.Next(5)
            };

            return licenca;
        }

        protected override void Seed(LojaContext context)
        {
            var idDoCliente = Guid.Parse("76f91d5c-f602-4823-9086-c8d7678e9557");
            var idDoUsuario = Guid.Parse("76f91d5c-f602-4823-9086-c8d7678e9566");

            var paginaInicialDefault = new ReadModel.PaginaInicial.PaginaInicial()
            {
                IdDoCliente = idDoCliente,
                SiglaDoIdioma = "pt-BR",
            };

            paginaInicialDefault.Lancamentos = new List<Lancamento>(){
                addLancamento(idDoCliente, paginaInicialDefault),
                addLancamento(idDoCliente, paginaInicialDefault),
                addLancamento(idDoCliente, paginaInicialDefault),
                addLancamento(idDoCliente, paginaInicialDefault)
            };

            paginaInicialDefault.MaisPopulares = new List<MaisPopular>(){
                addMaisPopular(idDoCliente, paginaInicialDefault),
                addMaisPopular(idDoCliente, paginaInicialDefault),
                addMaisPopular(idDoCliente, paginaInicialDefault),
                addMaisPopular(idDoCliente, paginaInicialDefault)
            };

            paginaInicialDefault.RecomendadosParaOCliente = new List<RecomendadoParaOCliente>(){
                addRecomendadosCliente(idDoCliente, paginaInicialDefault),
                addRecomendadosCliente(idDoCliente, paginaInicialDefault),
                addRecomendadosCliente(idDoCliente, paginaInicialDefault),
                addRecomendadosCliente(idDoCliente, paginaInicialDefault)
            };

            paginaInicialDefault.RecomendadosParaSuaEmpresa = new List<RecomendadoParaSuaEmpresa>(){
                addRecomendadosEmpresa(idDoCliente, paginaInicialDefault),
                addRecomendadosEmpresa(idDoCliente, paginaInicialDefault),
                addRecomendadosEmpresa(idDoCliente, paginaInicialDefault),
                addRecomendadosEmpresa(idDoCliente, paginaInicialDefault)
            };

            paginaInicialDefault.RecomendadosParaSuaEquipe = new List<RecomendadoParaSuaEquipe>(){
                addRecomendadosEquipe(idDoCliente, paginaInicialDefault),
                addRecomendadosEquipe(idDoCliente, paginaInicialDefault),
                addRecomendadosEquipe(idDoCliente, paginaInicialDefault),
                addRecomendadosEquipe(idDoCliente, paginaInicialDefault)
            };

            paginaInicialDefault.DestaquesDaSemana = new List<DestaqueDaSemana>(){
                addDestaquesSemana(idDoCliente, paginaInicialDefault),
                addDestaquesSemana(idDoCliente, paginaInicialDefault),
            };

            context.PaginaInicial.AddOrUpdate(paginaInicialDefault);

            paginaInicialDefault.Lancamentos.ForEach(x =>
            {
                 context.PaginaDeDetalhesDoConteudo.AddOrUpdate(AddDetalhe(idDoCliente, idDoUsuario, x.IdDoConteudo, x.IdDaEscola, x.Avaliacao, x.CustoLabel, x.Tipo, x.Titulo, null, paginaInicialDefault.MaisPopulares.ToArray()));
            });

            paginaInicialDefault.RecomendadosParaOCliente.ForEach(x =>
            {
                context.PaginaDeDetalhesDoConteudo.AddOrUpdate(AddDetalhe(idDoCliente, idDoUsuario, x.IdDoConteudo, x.IdDaEscola, x.Avaliacao, x.CustoLabel, x.Tipo, x.Titulo, paginaInicialDefault.RecomendadosParaOCliente.ToArray()));
            });

            paginaInicialDefault.RecomendadosParaSuaEmpresa.ForEach(x =>
            {
                context.PaginaDeDetalhesDoConteudo.AddOrUpdate(AddDetalhe(idDoCliente, idDoUsuario, x.IdDoConteudo, x.IdDaEscola, x.Avaliacao, x.CustoLabel, x.Tipo, x.Titulo, paginaInicialDefault.RecomendadosParaOCliente.ToArray()));
            });

            paginaInicialDefault.RecomendadosParaSuaEquipe.ForEach(x =>
            {
                context.PaginaDeDetalhesDoConteudo.AddOrUpdate(AddDetalhe(idDoCliente, idDoUsuario, x.IdDoConteudo, x.IdDaEscola, x.Avaliacao, x.CustoLabel, x.Tipo, x.Titulo, paginaInicialDefault.RecomendadosParaOCliente.ToArray()));
            });

            paginaInicialDefault.MaisPopulares.ForEach(x =>
            {
                context.PaginaDeDetalhesDoConteudo.AddOrUpdate(AddDetalhe(idDoCliente, idDoUsuario, x.IdDoConteudo, x.IdDaEscola, x.Avaliacao, x.CustoLabel, x.Tipo, x.Titulo, paginaInicialDefault.RecomendadosParaOCliente.ToArray()));
            });

            //var licencas = new List<Licenca>() {
            //    addLicenca(idDoCliente, paginaInicialDefault.Lancamentos.First()),
            //    addLicenca(idDoCliente, paginaInicialDefault.Lancamentos.First()),
            //    addLicenca(idDoCliente, paginaInicialDefault.Lancamentos.First()),
            //    addLicenca(idDoCliente, paginaInicialDefault.Lancamentos.Last())
            //};
            //
            //licencas.ForEach(x =>
            //{
            //    context.Licencas.AddOrUpdate(x);
            //});

        }
    }
}

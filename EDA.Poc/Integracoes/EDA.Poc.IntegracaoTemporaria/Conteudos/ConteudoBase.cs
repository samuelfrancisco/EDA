using System;
using System.Collections.Generic;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;
using EDA.Poc.Pagamentos.ReadModel.PaginaInicial;

namespace EDA.Poc.IntegracaoTemporaria.Conteudos
{
    public abstract class ConteudoBase
    {
        protected abstract Guid IdDoConteudo { get; set; }
        protected abstract string Titulo { get; set; }
        protected abstract string Objetivo { get; set; }
        protected abstract string Descricao { get; set; }
        protected abstract string Conteudo { get; set; }
        protected abstract string Tipo { get; set; }
        protected abstract decimal Custo { get; set; }
        protected abstract string CustoLabel { get; set; }
        protected abstract decimal PrecoIlimitado { get; set; }
        protected abstract string PrecoIlimitadoLabel { get; set; }
        protected abstract string Link { get; set; }
        protected abstract string CargaHoraria { get; set; }

        public virtual Lancamento ToLancamento()
        {
            return new Lancamento
                       {
                           Custo = Custo,
                           CustoLabel = CustoLabel,
                           IdDoCliente = Constantes.IdDoCliente,
                           IdDoConteudo = IdDoConteudo,
                           IdDaEscola = Constantes.IdDaEscola,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           Titulo = Titulo,
                           Tipo = Tipo
                       };
        }

        public virtual MaisPopular ToMaisPopular()
        {
            return new MaisPopular
                       {
                           Custo = Custo,
                           CustoLabel = CustoLabel,
                           IdDoCliente = Constantes.IdDoCliente,
                           IdDoConteudo = IdDoConteudo,
                           IdDaEscola = Constantes.IdDaEscola,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           Titulo = Titulo,
                           Tipo = Tipo
                       };
        }

        public virtual RecomendadoParaOCliente ToRecomendadoParaOCliente()
        {
            return new RecomendadoParaOCliente
                       {
                           Custo = Custo,
                           CustoLabel = CustoLabel,
                           IdDoCliente = Constantes.IdDoCliente,
                           IdDoConteudo = IdDoConteudo,
                           IdDaEscola = Constantes.IdDaEscola,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           Titulo = Titulo,
                           Tipo = Tipo
                       };
        }

        public virtual RecomendadoParaSuaEmpresa ToRecomendadoParaSuaEmpresa()
        {
            return new RecomendadoParaSuaEmpresa
                       {
                           Custo = Custo,
                           CustoLabel = CustoLabel,
                           IdDoCliente = Constantes.IdDoCliente,
                           IdDoConteudo = IdDoConteudo,
                           IdDaEscola = Constantes.IdDaEscola,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           Titulo = Titulo,
                           Tipo = Tipo
                       };
        }

        public virtual RecomendadoParaSuaEquipe ToRecomendadoParaSuaEquipe()
        {
            return new RecomendadoParaSuaEquipe
                       {
                           
                           Custo = Custo,
                           CustoLabel = CustoLabel,
                           IdDoCliente = Constantes.IdDoCliente,
                           IdDoConteudo = IdDoConteudo,
                           IdDaEscola = Constantes.IdDaEscola,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           Titulo = Titulo,
                           Tipo = Tipo
                       };
        }

        public virtual Semelhante ToSemelhante()
        {
            return new Semelhante
            {
                Custo = Custo,
                CustoLabel = CustoLabel,
                IdDoConteudo = IdDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                Titulo = Titulo,
                Tipo = Tipo
            };
        }

        public virtual MaisDoFornecedor ToMaisDoFornecedor()
        {
            return new MaisDoFornecedor
            {
                Custo = Custo,
                CustoLabel = CustoLabel,
                IdDoConteudo = IdDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                Titulo = Titulo,
                Tipo = Tipo
            };
        }

        public virtual PaginaDeDetalhesDoConteudo ToPaginaDeDetalhesDoConteudo()
        {
            return new PaginaDeDetalhesDoConteudo
                       {
                           Version = 1,
                           IdDoConteudo = IdDoConteudo,
                           ConsolidadoAvaliacao = new ConsolidadoAvaliacao
                                                      {
                                                          IdDoConteudo = IdDoConteudo,
                                                          QuantidadeDeAvaliacoesPorValorDeAvaliacao = new Dictionary<int, int>()
                                                      },
                           Precos = new Precos
                                        {
                                            IdDoConteudo = IdDoConteudo,
                                            PrecoIlimitado = PrecoIlimitado,
                                            PrecoIlimitadoLabel = PrecoIlimitadoLabel,
                                            PrecosPorQuantidadeDeUsuarios = GerarPrecosPorQuantidadeDeUsuarios()
                                        },
                           Detalhes = new List<Detalhes>
                                          {
                                              new Detalhes
                                                  {
                                                      IdDoConteudo = IdDoConteudo,
                                                      SiglaDoIdioma = Constantes.SiglaDoIdioma,
                                                      IdDaEscola = Constantes.IdDaEscola,
                                                      CustoLabel = CustoLabel,
                                                      Tipo = Tipo,
                                                      Titulo = Titulo,
                                                      Objetivo = Objetivo,
                                                      Descricao = Descricao,
                                                      Link = Link,
                                                      Conteudo = Conteudo,
                                                      CargaHoraria = CargaHoraria,
                                                      PalavrasChave = GerarPalavrasChave()
                                                  }
                                          },
                           Previews = GerarPreviews(),
                           CompetenciasParaOCliente = GerarCompetenciasParaOCliente(),
                           CompetenciasParaSuaEmpresa = GerarCompetenciasParaSuaEmpresa(),
                           Semelhantes = ObterSemelhantes(),
                           MaisDoFornecedor = ObterMaisDoFornecedor()
                       };
        }

        protected virtual List<MaisDoFornecedor> ObterMaisDoFornecedor()
        {
            return new List<MaisDoFornecedor>();
        }

        protected virtual List<Semelhante> ObterSemelhantes()
        {
            return new List<Semelhante>();
        }

        protected virtual List<PrecoPorQuantidadeDeUsuarios> GerarPrecosPorQuantidadeDeUsuarios()
        {
            return new List<PrecoPorQuantidadeDeUsuarios>
                       {
                           new PrecoPorQuantidadeDeUsuarios
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   IdPrecoPorQuantidadeDeUsuarios = Guid.NewGuid(),
                                   Preco = 150,
                                   LicencasDe = 1,
                                   LicencasAte = 30
                               },
                           new PrecoPorQuantidadeDeUsuarios
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   IdPrecoPorQuantidadeDeUsuarios = Guid.NewGuid(),
                                   Preco = 125,
                                   LicencasDe = 31,
                                   LicencasAte = 50
                               },
                           new PrecoPorQuantidadeDeUsuarios
                               {
                                   IdDoConteudo = IdDoConteudo,
                                   IdPrecoPorQuantidadeDeUsuarios = Guid.NewGuid(),
                                   Preco = 100,
                                   LicencasDe = 51
                               }
                       };
        }

        protected virtual List<CompetenciaParaOCliente> GerarCompetenciasParaOCliente()
        {
            return new List<CompetenciaParaOCliente>();
        }

        protected virtual List<CompetenciaParaSuaEmpresa> GerarCompetenciasParaSuaEmpresa()
        {
            return new List<CompetenciaParaSuaEmpresa>();
        }

        protected virtual List<PalavraChave> GerarPalavrasChave()
        {
            return new List<PalavraChave>();
        }

        protected virtual List<Preview> GerarPreviews()
        {
            return new List<Preview>();
        }
    }
}

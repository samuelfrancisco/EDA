using System;
using System.Data.Entity;
using System.Linq;
using EGuru.MarketPlace.Loja.Consultas.Entity.Contextos;
using EGuru.MarketPlace.Loja.Consultas.PaginaDeDetalhesDoConteudo;
using EGuru.MarketPlace.Loja.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EGuru.MarketPlace.Loja.Consultas.Entity.PaginaDeDetalhesDoConteudo
{
    public class ServicoPaginaDeDetalhesDoConteudo : IServicoPaginaDeDetalhesDoConteudo
    {
        public ReadModel.PaginaDeDetalhesDoConteudo.PaginaDeDetalhesDoConteudo ObterAPaginaDeDetalhesDoConteudo(Guid idDoConteudo)
        {
            using (var contexto = new LojaContext())
            {
                var detalheDoConteudo = contexto.PaginaDeDetalhesDoConteudo
                    .Include(x => x.Detalhes)
                    .Include(x => x.CompetenciasParaOCliente)
                    .Include(x => x.CompetenciasParaSuaEquipe)
                    .Include(x => x.CompetenciasParaSuaEmpresa)
                    .Include(x => x.ConsolidadoAvaliacao)
                    .Include(x => x.Previews)
                    .Include(x => x.Resenhas)
                    .Include(x => x.MaisDoFornecedor)
                    .Include(x => x.Semelhantes)
                    .Include(x => x.Precos)
                    .Include(x => x.Precos.PrecosPorQuantidadeDeUsuarios)
                    .FirstOrDefault(x => x.IdDoConteudo == idDoConteudo);

                return detalheDoConteudo;
            }       
        }
    }
}
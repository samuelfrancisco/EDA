using System;
using System.Data.Entity;
using System.Linq;
using EGuru.MarketPlace.Loja.Consultas.Entity.Contextos;
using EGuru.MarketPlace.Loja.Consultas.PaginaInicial;

namespace EGuru.MarketPlace.Loja.Consultas.Entity.PaginaInicial
{
    public class ServicoPaginaInicial : IServicoPaginaInicial
    {
        public ReadModel.PaginaInicial.PaginaInicial ObterAPaginaInicial(Guid idDoCliente, string siglaDoIdioma)
        {
            using (var contexto = new LojaContext())
            {
                var paginaInicial = contexto.PaginaInicial
                    .Include(x => x.Lancamentos)
                    .Include(x => x.DestaquesDaSemana)
                    .Include(x => x.MaisPopulares)
                    .Include(x => x.RecomendadosParaOCliente)
                    .Include(x => x.RecomendadosParaSuaEmpresa)
                    .Include(x => x.RecomendadosParaSuaEquipe)
                    .Single(x => x.IdDoCliente == idDoCliente && x.SiglaDoIdioma == siglaDoIdioma);

                return paginaInicial;
            }       
        }
    }
}
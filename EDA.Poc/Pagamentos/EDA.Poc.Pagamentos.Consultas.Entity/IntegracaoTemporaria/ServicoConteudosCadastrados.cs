using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EGuru.MarketPlace.Loja.Consultas.Entity.Contextos;
using EGuru.MarketPlace.Loja.Consultas.IntegracaoTemporaria;
using EGuru.MarketPlace.Loja.ReadModel.IntegracaoTemporaria;

namespace EGuru.MarketPlace.Loja.Consultas.Entity.IntegracaoTemporaria
{
    public class ServicoConteudosCadastrados : IServicoConteudosCadastrados
    {
        public IEnumerable<ConteudoCadastrado> ObterTodosOsConteudosCadastrados()
        {
            using (var contexto = new LojaContext())
            {
                var conteudos = contexto.PaginaDeDetalhesDoConteudo
                    .Include(x => x.Precos)
                    .Include(x => x.Precos.PrecosPorQuantidadeDeUsuarios)
                    .Select(x => new ConteudoCadastrado
                                     {
                                         IdDoConteudo = x.IdDoConteudo,
                                         PrecoIlimitado = x.Precos.PrecoIlimitado,
                                         FaixaDePrecos = x.Precos.PrecosPorQuantidadeDeUsuarios
                                             .Select(y => new ConteudoCadastradoFaixaDePreco
                                                              {
                                                                  Preco = y.Preco,
                                                                  QuantidadeInicialDeLicencas = y.LicencasDe,
                                                                  QuantidadeFinalDeLicencas = y.LicencasAte
                                                              })
                                     })
                    .ToList();

                return conteudos;
            }
        }
    }
}
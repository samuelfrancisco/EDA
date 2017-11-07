using System;
using System.Threading.Tasks;

namespace EDA.Poc.Pagamentos.Consultas.PaginaDeDetalhesDoConteudo
{
    public interface IServicoPaginaDeDetalhesDoConteudo
    {
        Task<ReadModel.PaginaDeDetalhesDoConteudo.PaginaDeDetalhesDoConteudo> ObterAPaginaDeDetalhesDoConteudo(Guid idDoConteudo);
    }
}

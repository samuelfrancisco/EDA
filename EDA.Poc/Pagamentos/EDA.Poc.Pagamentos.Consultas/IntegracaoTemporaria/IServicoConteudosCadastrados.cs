using System.Collections.Generic;
using System.Threading.Tasks;
using EDA.Poc.Pagamentos.ReadModel.IntegracaoTemporaria;

namespace EDA.Poc.Pagamentos.Consultas.IntegracaoTemporaria
{
    public interface IServicoConteudosCadastrados
    {
        Task<List<ConteudoCadastrado>> ObterTodosOsConteudosCadastrados();
    }
}

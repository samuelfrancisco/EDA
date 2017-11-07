using EDA.Poc.Pagamentos.ReadModel.Usuarios;
using System;
using System.Threading.Tasks;

namespace EDA.Poc.Pagamentos.Consultas.Usuarios
{
    public interface IServicoUsuario
    {
        Task<Usuario> ObterUsuarioPorId(Guid id);
    }
}

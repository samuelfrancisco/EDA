using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDA.Poc.Infraestrutura.Messaging.Interfaces
{
    public interface ICommandBus
    {
        Task SendOne<T>(T command) where T : class, ICommand;
        Task SendMany<T>(IEnumerable<T> commands) where T : class, ICommand;
    }
}

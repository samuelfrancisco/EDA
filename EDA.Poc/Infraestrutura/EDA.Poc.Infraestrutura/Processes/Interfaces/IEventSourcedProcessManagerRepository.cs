using System;
using System.Threading.Tasks;

namespace EDA.Poc.Infraestrutura.Processes.Interfaces
{
    public interface IEventSourcedProcessManagerRepository<T> where T : EventSourcedProcessManager
    {
        Task<T> GetById(Guid id);
        Task Save(T processManager, Guid commitId);
    }
}

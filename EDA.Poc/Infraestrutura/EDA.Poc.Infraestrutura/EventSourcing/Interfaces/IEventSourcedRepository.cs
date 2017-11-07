using System;
using System.Threading.Tasks;

namespace EDA.Poc.Infraestrutura.EventSourcing.Interfaces
{
    public interface IEventSourcedRepository<T> where T : class, IEventSourced
    {
        Task<T> GetById(Guid id);
        Task<T> GetById(Guid id, string bucketId);
        Task Save(T aggregate, Guid commitId);
        Task Save(T aggregate, Guid commitId, string bucketId);
    }
}

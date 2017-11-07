using System;
using System.Threading.Tasks;

namespace EDA.Poc.Infraestrutura.ReadModel.Interfaces
{
    public interface IVersionedReadModelRepository<T> where T : VersionedReadModel
    {
        Task<T> GetByIdentity(Guid identity, DateTime eventDate);
        Task Save(T readModel);
    }
}

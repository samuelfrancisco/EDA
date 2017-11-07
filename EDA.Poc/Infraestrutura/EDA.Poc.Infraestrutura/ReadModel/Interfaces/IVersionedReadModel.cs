using System;

namespace EDA.Poc.Infraestrutura.ReadModel.Interfaces
{
    public interface IVersionedReadModel
    {
        VersionedReadModelIdentity Id { get; }
        Guid Identity { get; set; }
        long Version { get; set; }
    }
}

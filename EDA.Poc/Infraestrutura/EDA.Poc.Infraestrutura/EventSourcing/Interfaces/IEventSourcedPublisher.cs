using System.Threading.Tasks;

namespace EDA.Poc.Infraestrutura.EventSourcing.Interfaces
{
    public interface IEventSourcedPublisher<T> where T : class, IEventSourced
    {
        Task SaveUnpublishedEvents(T aggregate);
        Task PublishUnpublishedEvents(T aggregate);
        Task RemoveUnpublishedEvents(T aggregate);
    }
}

using System;

namespace EventStorePoc.Microservico
{
    public interface IEvent
    {
        Guid Id { get; set; }
        DateTime Data { get; set; }
    }
}

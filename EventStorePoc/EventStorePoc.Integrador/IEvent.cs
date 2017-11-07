using System;

namespace EventStorePoc.Integrador
{
    public interface IEvent
    {
        Guid Id { get; set; }
        DateTime Data { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.Messaging.Exceptions;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;
using MassTransit;

namespace EDA.Poc.Infraestrutura.Messaging
{
    public class EventBus : IEventBus
    {
        private readonly IBusControl _busControl;

        public EventBus(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public Task PublishOne<T>(T @event) where T : class, IEvent
        {
            return PublishMany<T>(new[] { @event });
        }

        public Task PublishMany<T>(IEnumerable<T> events) where T : class, IEvent
        {
            return Task.Run(async () =>
                                      {
                                          EventPublishException exception = null;

                                          foreach (var @event in events)
                                          {
                                              try
                                              {
                                                  await _busControl.Publish(@event).ConfigureAwait(false);
                                              }
                                              catch (Exception ex)
                                              {
                                                  if (exception == null)
                                                      exception = new EventPublishException("Some errors occurs publishing events.");

                                                  exception.AddNotPublishedEvent(new NotPublishedEvent(@event, ex));
                                              }
                                          }

                                          if (exception != null) throw exception;
                                      });
        }
    }
}
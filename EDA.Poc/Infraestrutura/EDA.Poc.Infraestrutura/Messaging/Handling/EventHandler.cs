using System;
using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;
using EDA.Poc.Infraestrutura.ReadModel.Exceptions;
using NEventStore;

namespace EDA.Poc.Infraestrutura.Messaging.Handling
{
    public abstract class EventHandler
    {
        private readonly IEventHandlerRepository _eventHandlerRepository;

        protected EventHandler(IEventHandlerRepository eventHandlerRepository)
        {
            _eventHandlerRepository = eventHandlerRepository;
        }

        protected Task ExecuteConsume<T>(T @event, Func<T, Task> actionToExecute) where T : class, IEvent
        {
            return Task.Run(async () =>
                                      {
                                          var retry = true;

                                          while (retry)
                                          {
                                              try
                                              {
                                                  var succeededEvent = await _eventHandlerRepository.GetSucceededEventById(this.GetType(), @event.Id).ConfigureAwait(false);

                                                  if (succeededEvent != null) return;

                                                  await SaveReceivedEvent(@event).ConfigureAwait(false);

                                                  await actionToExecute(@event).ConfigureAwait(false);

                                                  retry = false;

                                                  try
                                                  {
                                                      await SaveSucceededEvent(@event).ConfigureAwait(false);
                                                  }
                                                  catch (Exception)
                                                  {

                                                  }
                                              }
                                              catch (ConcurrencyException)
                                              {

                                              }
                                              catch (ReadModelConcurrencyException)
                                              {

                                              }
                                              catch (Exception ex)
                                              {
                                                  await SaveFailedEvent(@event, ex).ConfigureAwait(false);

                                                  throw;
                                              }
                                          }
                                      });
        }

        protected Task SaveReceivedEvent<T>(T @event) where T : class, IEvent
        {
            return Task.Run(async () =>
                                      {
                                          var receivedEvent = await _eventHandlerRepository.GetReceivedEventById(this.GetType(), @event.Id).ConfigureAwait(false);

                                          if (receivedEvent == null)
                                          {
                                              receivedEvent = new ReceivedEvent(this.GetType(), @event);
                                          }
                                          else
                                          {
                                              receivedEvent.AddNewReceiveDate();
                                          }

                                          await _eventHandlerRepository.SaveReceivedEvent(this.GetType(), receivedEvent).ConfigureAwait(false);
                                      });
        }

        protected Task SaveSucceededEvent<T>(T @event) where T : class, IEvent
        {
            return Task.Run(async () =>
                                      {
                                          var failedEvent = await _eventHandlerRepository.GetFailedEventById(this.GetType(), @event.Id).ConfigureAwait(false);

                                          var succeededEvent = failedEvent == null
                                                                   ? new SucceededEvent(this.GetType(), @event)
                                                                   : new SucceededEvent(this.GetType(), @event, failedEvent.Fails);

                                          var tasks = new[]
                                                          {
                                                              _eventHandlerRepository.SaveSucceededEvent(this.GetType(), succeededEvent),

                                                              Task.Run(async () =>
                                                                                 {
                                                                                     if (failedEvent == null) return;

                                                                                     await _eventHandlerRepository.RemoveFailedEvent(this.GetType(), failedEvent).ConfigureAwait(false);
                                                                                 })
                                                          };

                                          await Task.WhenAll(tasks).ConfigureAwait(false);
                                      });
        }

        protected Task SaveFailedEvent<T>(T @event, Exception exception) where T : class, IEvent
        {
            return Task.Run(async () =>
                                      {
                                          var failedEvent = await _eventHandlerRepository.GetFailedEventById(this.GetType(), @event.Id).ConfigureAwait(false);

                                          if (failedEvent == null)
                                          {
                                              failedEvent = new FailedEvent(this.GetType(), @event, exception);
                                          }
                                          else
                                          {
                                              failedEvent.AddNewFail(exception);
                                          }

                                          await _eventHandlerRepository.SaveFailedEvent(this.GetType(), failedEvent).ConfigureAwait(false);
                                      });
        }
    }
}

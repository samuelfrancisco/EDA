using System;
using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.Messaging.Exceptions;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;
using MassTransit;
using NEventStore;

namespace EDA.Poc.Infraestrutura.Messaging.Handling
{
    public abstract class CommandHandler<T> : ICommandHandler<T> where T : class, ICommand
    {
        private readonly ICommandHandlerRepository _commandHandlerRepository;

        protected CommandHandler(ICommandHandlerRepository commandHandlerRepository)
        {
            _commandHandlerRepository = commandHandlerRepository;
        }

        public abstract Task Consume(ConsumeContext<T> context);

        protected Task ExecuteConsume(T command, Func<T, Task> actionToExecute)
        {
            return Task.Run(async () =>
                                      {
                                          var retry = true;

                                          while (retry)
                                          {
                                              try
                                              {
                                                  var succeededCommand = await _commandHandlerRepository.GetSucceededCommandById(this.GetType(), command.Id).ConfigureAwait(false);

                                                  if (succeededCommand != null) throw new DuplicatedCommandException();

                                                  await SaveReceivedCommand(command).ConfigureAwait(false);

                                                  await actionToExecute(command).ConfigureAwait(false);

                                                  retry = false;

                                                  try
                                                  {
                                                      await SaveSucceededCommand(command).ConfigureAwait(false);
                                                  }
                                                  catch (Exception)
                                                  {

                                                  }
                                              }
                                              catch (ConcurrencyException)
                                              {

                                              }
                                              catch (Exception ex)
                                              {
                                                  await SaveFailedCommand(command, ex).ConfigureAwait(false);

                                                  throw;
                                              }
                                          }
                                      });
        }

        protected Task SaveReceivedCommand(T command)
        {
            return Task.Run(async () =>
                                      {
                                          var receivedCommand = await _commandHandlerRepository.GetReceivedCommandById(this.GetType(), command.Id).ConfigureAwait(false);

                                          if (receivedCommand == null)
                                          {
                                              receivedCommand = new ReceivedCommand(this.GetType(), command);
                                          }
                                          else
                                          {
                                              receivedCommand.AddNewReceiveDate();
                                          }

                                          await _commandHandlerRepository.SaveReceivedCommand(this.GetType(), receivedCommand).ConfigureAwait(false);
                                      });
        }

        protected Task SaveSucceededCommand(T command)
        {
            return Task.Run(async () =>
                                      {
                                          var failedCommand = await _commandHandlerRepository.GetFailedCommandById(this.GetType(), command.Id).ConfigureAwait(false);

                                          var succeededCommand = failedCommand == null
                                                                     ? new SucceededCommand(this.GetType(), command)
                                                                     : new SucceededCommand(this.GetType(), command, failedCommand.Fails);

                                          var tasks = new[]
                                                          {
                                                              _commandHandlerRepository.SaveSucceededCommand(this.GetType(), succeededCommand),

                                                              Task.Run(async () =>
                                                                                 {
                                                                                     if (failedCommand == null) return;

                                                                                     await _commandHandlerRepository.RemoveFailedCommand(this.GetType(), failedCommand).ConfigureAwait(false);
                                                                                 })
                                                          };

                                          await Task.WhenAll(tasks).ConfigureAwait(false);
                                      });
        }

        protected Task SaveFailedCommand(T command, Exception exception)
        {
            return Task.Run(async () =>
                                      {
                                          var failedCommand = await _commandHandlerRepository.GetFailedCommandById(this.GetType(), command.Id).ConfigureAwait(false);

                                          if (failedCommand == null)
                                          {
                                              failedCommand = new FailedCommand(this.GetType(), command, exception);
                                          }
                                          else
                                          {
                                              failedCommand.AddNewFail(exception);
                                          }

                                          await _commandHandlerRepository.SaveFailedCommand(this.GetType(), failedCommand).ConfigureAwait(false);
                                      });
        }
    }
}

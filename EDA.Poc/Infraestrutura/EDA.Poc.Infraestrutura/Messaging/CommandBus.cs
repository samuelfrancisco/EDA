using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.Messaging.Exceptions;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;
using MassTransit;

namespace EDA.Poc.Infraestrutura.Messaging
{
    public class CommandBus : ICommandBus
    {
        private readonly IBusControl _busControl;
        private readonly Uri _commandQueueUri;
        private readonly Uri _schedulerCommandQueueUri;

        public CommandBus(IBusControl busControl)
        {
            _busControl = busControl;
            _commandQueueUri = new Uri(ConfigurationManager.AppSettings["CommandQueueFullUri"]);
            _schedulerCommandQueueUri = new Uri(ConfigurationManager.AppSettings["CommandSchedulerQueueFullUri"]);
        }

        public Task SendOne<T>(T command) where T : class, ICommand
        {
            return SendMany(new[] { command });
        }

        public Task SendMany<T>(IEnumerable<T> commands) where T: class, ICommand
        {
            return Task.Run(async () =>
                                      {
                                          CommandSendException exception = null;

                                          var incommingCommands = commands.ToList();

                                          foreach (var command in incommingCommands)
                                          {
                                              try
                                              {
                                                  var timeToSchedule = DateTime.Now.Add(command.Delay);

                                                  if (timeToSchedule > DateTime.Now)
                                                  {
                                                      var sendEndpoint = await _busControl.GetSendEndpoint(_schedulerCommandQueueUri).ConfigureAwait(false);

                                                      await sendEndpoint.ScheduleSend(_commandQueueUri, timeToSchedule, command).ConfigureAwait(false);
                                                  }
                                                  else
                                                  {
                                                      var sendEndpoint = await _busControl.GetSendEndpoint(_commandQueueUri).ConfigureAwait(false);

                                                      await sendEndpoint.Send(command).ConfigureAwait(false);
                                                  }
                                              }
                                              catch (Exception ex)
                                              {
                                                  if (exception == null)
                                                      exception = new CommandSendException("Some errors occurs sending commands.");

                                                  exception.AddNotSentCommand(new NotSentCommand(command, ex));
                                              }
                                          }

                                          if (exception != null) throw exception;

                                      });
        }
    }
}

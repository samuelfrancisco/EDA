using EventStore.ClientAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventStorePoc.Microservico
{
    class Program
    {
        private static readonly IPEndPoint _tcpEndPoint = new IPEndPoint(IPAddress.Loopback, 1113);        

        private static IEventStoreConnection _eventStoreConnection;

        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando...");

            _eventStoreConnection = EventStoreConnection.Create(_tcpEndPoint);

            _eventStoreConnection.ConnectAsync().Wait();

            var cancelationTokenSource = new CancellationTokenSource();

            Iniciar(cancelationTokenSource.Token);

            Console.ReadLine();

            cancelationTokenSource.Cancel();

            _eventStoreConnection.Close();

            Console.WriteLine("Finalizando...");
        }

        private static void Iniciar(CancellationToken token)
        {
            _eventStoreConnection.SubscribeToStreamFrom("Agencias", null, false, (sub, @event) =>
            {
                return Task.Run(() =>
                {
                    Console.WriteLine("Recebendo evento...");

                    var eventTypeName = (string)JObject.Parse(Encoding.UTF8.GetString(@event.OriginalEvent.Metadata)).Property("EventTypeName").Value;

                    switch (eventTypeName)
                    {
                        case "NovaAgenciaRegistrada":
                            {
                                var evento = JsonConvert.DeserializeObject<NovaAgenciaRegistrada>(Encoding.UTF8.GetString(@event.OriginalEvent.Data));

                                using (var contexto = new MicroservicoContex())
                                {
                                    var agencia = contexto.Agencias.FirstOrDefault(x => x.IdDaAgencia == evento.IdDaAgencia);

                                    if(agencia == null)
                                    {
                                        contexto.Agencias.Add(new Agencia
                                        {
                                            IdDaAgencia = evento.IdDaAgencia,
                                            Nome = evento.NomeDaAgencia,
                                            Status = evento.StatusDaAgencia
                                        });

                                        contexto.SaveChanges();
                                    }
                                }
                                break;
                            }
                        case "InformacoesDaAgenciaAtualizadas":
                            {
                                var evento = JsonConvert.DeserializeObject<InformacoesDaAgenciaAtualizadas>(Encoding.UTF8.GetString(@event.OriginalEvent.Data));

                                using (var contexto = new MicroservicoContex())
                                {
                                    var agencia = contexto.Agencias.FirstOrDefault(x => x.IdDaAgencia == evento.IdDaAgencia);

                                    if (agencia != null)
                                    {
                                        agencia.Nome = evento.NomeDaAgencia;
                                        agencia.Status = evento.StatusDaAgencia;

                                        contexto.SaveChanges();
                                    }
                                }
                                break;
                            }
                        default:
                            break;
                    }
                });
            });
        }
    }
}

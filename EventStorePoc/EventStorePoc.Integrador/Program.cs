using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventStorePoc.Integrador
{
    class Program
    {
        private static readonly IPEndPoint _tcpEndPoint = new IPEndPoint(IPAddress.Loopback, 1113);

        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None
        };

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

        public static void Iniciar(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    AtualizarAgencias();
                }

            }, cancellationToken);
        }

        private static void AtualizarAgencias()
        {
            try
            {
                Console.WriteLine("Atualizando as agências...");

                var agencias = ObterAgencias();

                var agenciasEnviadas = ObterAgenciasEnviadas();

                var novasAgencias = agencias.Where(x => agenciasEnviadas.All(y => x.Id != y.IdDaAgencia)).ToList();

                if (novasAgencias.Any())
                    RegistrarNovasAgencias(novasAgencias);

                var agenciasAlteradas = agencias
                    .Where(x => agenciasEnviadas.Any(y => x.Id == y.IdDaAgencia && (x.Nome != y.Nome || x.Status != y.Status)))
                    .ToList();

                if (agenciasAlteradas.Any())
                    RegistrarAgenciasAlteradas(agenciasAlteradas);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static IEnumerable<Agencia> ObterAgencias()
        {
            using (var contexto = new IntegradorContex())
            {
                return contexto.Agencias.ToList();
            }
        }

        private static IEnumerable<AgenciaEnviada> ObterAgenciasEnviadas()
        {
            using (var contexto = new IntegradorContex())
            {
                var agenciasEnviadas = contexto.AgenciasEnviadas.ToList();

                var agenciasEnviadasAgrupadas = agenciasEnviadas
                    .GroupBy(x => x.IdDaAgencia);

                return agenciasEnviadasAgrupadas.Select(x => x.OrderByDescending(y => y.EnviadoEm).First()).ToList();
            }
        }

        private static void RegistrarNovasAgencias(List<Agencia> novasAgencias)
        {
            Console.WriteLine("Registrando as novas agências...");

            var data = DateTime.UtcNow;

            using (var contexto = new IntegradorContex())
            {
                SalvarEventos(novasAgencias.Select(x => new NovaAgenciaRegistrada
                {
                    IdDaAgencia = x.Id,
                    NomeDaAgencia = x.Nome,
                    StatusDaAgencia = x.Status
                }));

                foreach (var agencia in novasAgencias)
                {
                    contexto.AgenciasEnviadas.Add(new AgenciaEnviada
                    {
                        IdDaAgencia = agencia.Id,
                        Nome = agencia.Nome,
                        Status = agencia.Status,
                        CriadoEm = data,
                        EnviadoEm = data
                    });
                }

                contexto.SaveChanges();
            }
        }

        private static void RegistrarAgenciasAlteradas(List<Agencia> agenciasAlteradas)
        {
            Console.WriteLine("Registrando as agências alteradas...");

            var data = DateTime.UtcNow;

            using (var contexto = new IntegradorContex())
            {
                SalvarEventos(agenciasAlteradas.Select(x => new InformacoesDaAgenciaAtualizadas
                {
                    IdDaAgencia = x.Id,
                    NomeDaAgencia = x.Nome,
                    StatusDaAgencia = x.Status
                }));

                foreach (var agencia in agenciasAlteradas)
                {
                    contexto.AgenciasEnviadas.Add(new AgenciaEnviada
                    {
                        IdDaAgencia = agencia.Id,
                        Nome = agencia.Nome,
                        Status = agencia.Status,
                        CriadoEm = data,
                        EnviadoEm = data
                    });
                }

                contexto.SaveChanges();
            }
        }

        private static void SalvarEventos(IEnumerable<IEvent> eventos)
        {
            var streamId = "Agencias";

            var commitHeaders = new Dictionary<string, object>
            {
                { "CommitId", Guid.NewGuid() }
            };

            var eventsToSave = eventos.Select(x => ToEventData(x.Id, x, commitHeaders)).ToList();

            if (eventsToSave.Count < 100)
            {
                _eventStoreConnection.AppendToStreamAsync(streamId, ExpectedVersion.Any, eventsToSave).Wait();
            }
            else
            {
                var transaction = _eventStoreConnection.StartTransactionAsync(streamId, ExpectedVersion.Any).Result;

                var position = 0;

                while (position < eventsToSave.Count)
                {
                    var pageEvents = eventsToSave.Skip(position).Take(100);

                    transaction.WriteAsync(pageEvents).Wait();

                    position += 100;
                }

                transaction.CommitAsync().Wait();
            }
        }

        private static EventData ToEventData(Guid eventId, object @event, IDictionary<string, object> headers)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event, _serializerSettings));

            var eventHeaders = new Dictionary<string, object>(headers)
            {
                { "EventId", eventId },
                { "EventAssemblyName", @event.GetType().Assembly.FullName },
                { "EventNamespace", @event.GetType().Namespace },
                { "EventFullName", @event.GetType().FullName },
                { "EventTypeName", @event.GetType().Name },
                { "EventClrTypeName", @event.GetType().AssemblyQualifiedName }
            };

            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventHeaders, _serializerSettings));

            var typeName = @event.GetType().Name;

            return new EventData(eventId, typeName, true, data, metadata);
        }
    }
}

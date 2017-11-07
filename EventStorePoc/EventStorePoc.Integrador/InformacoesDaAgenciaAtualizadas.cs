using System;

namespace EventStorePoc.Integrador
{
    public class InformacoesDaAgenciaAtualizadas : IEvent
    {
        public InformacoesDaAgenciaAtualizadas()
        {
            Id = Guid.NewGuid();
            Data = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public int IdDaAgencia { get; set; }
        public string NomeDaAgencia { get; set; }
        public bool StatusDaAgencia { get; set; }
        public DateTime Data { get; set; }
    }
}

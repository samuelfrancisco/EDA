using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventStorePoc.Integrador
{
    [Table("AgenciasEnviadas")]
    public class AgenciaEnviada
    {
        [Key]
        public int Id { get; set; }
        public int IdDaAgencia { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? EnviadoEm { get; set; }
    }
}

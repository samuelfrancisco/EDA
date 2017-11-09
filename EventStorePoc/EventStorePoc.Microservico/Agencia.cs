using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventStorePoc.Microservico
{
    [Table("Microservico.Agencias")]
    public class Agencia
    {
        [Key]
        public int Id { get; set; }
        public int IdDaAgencia { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
    }
}

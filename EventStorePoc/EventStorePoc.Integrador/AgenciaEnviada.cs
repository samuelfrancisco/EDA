using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStorePoc.Integrador
{
    public class AgenciaEnviada
    {
        public int Id { get; set; }
        public int IdDaAgencia { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? EnviadoEm { get; set; }
    }
}

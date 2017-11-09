using System.Data.Entity;

namespace EventStorePoc.Microservico
{
    public class MicroservicoContex : DbContext
    {
        public MicroservicoContex()
            : base("name=Default")
        {

        }

        public DbSet<Agencia> Agencias { get; set; }
    }
}

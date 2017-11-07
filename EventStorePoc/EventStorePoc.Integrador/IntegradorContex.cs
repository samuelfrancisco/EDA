using System.Data.Entity;

namespace EventStorePoc.Integrador
{
    public class IntegradorContex : DbContext
    {
        public IntegradorContex()
            : base("name=Default")
        {

        }

        public DbSet<Agencia> Agencias { get; set; }
        public DbSet<AgenciaEnviada> AgenciasEnviadas { get; set; }
    }
}

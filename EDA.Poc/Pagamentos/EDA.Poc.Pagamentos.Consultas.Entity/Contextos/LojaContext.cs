using System.Data.Entity;
using EGuru.MarketPlace.Loja.ReadModel.PaginaInicial;
using EGuru.MarketPlace.Loja.ReadModel.PaginaDeDetalhesDoConteudo;
using EGuru.MarketPlace.Loja.ReadModel.WebAPI;

namespace EGuru.MarketPlace.Loja.Consultas.Entity.Contextos
{
    public class LojaContext : DbContext
    {
        public LojaContext()
            : base("name=Default")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ReadModel.PaginaInicial.PaginaInicial> PaginaInicial { get; set; }
        public DbSet<ReadModel.PaginaDeDetalhesDoConteudo.PaginaDeDetalhesDoConteudo> PaginaDeDetalhesDoConteudo { get; set; }
        public DbSet<ReadModel.PaginaDeLicencas.PaginaDeLicencas> PaginaDeLicencas { get; set; }
        public DbSet<Licenca> Licencas { get; set; }
    }
}

using System;
using System.Linq;
using EGuru.MarketPlace.Loja.ReadModel.WebAPI;
using EGuru.MarketPlace.Loja.Consultas.WebAPI;
using EGuru.MarketPlace.Loja.Consultas.Entity.Contextos;

namespace EGuru.MarketPlace.Loja.Consultas.Entity.WebAPI
{
    public class ServicoLicencasAdquiridas : IServicoLicencasAdquiridas
    {
        public IQueryable<Licenca> ObterLicencas(Guid idDoCliente, string siglaDoIdioma)
        {
            using (var contexto = new LojaContext())
            {
                contexto.Configuration.ProxyCreationEnabled = false;

                var licencas = contexto.Licencas
                    .Where(x => x.IdDoCliente == idDoCliente && x.SiglaDoIdioma == siglaDoIdioma)
                    .ToList().AsQueryable();

                return licencas;
            }
        }

        public Licenca ObterLicencaPeloId(Guid idDaLicenca)
        {
            using (var contexto = new LojaContext())
            {
                contexto.Configuration.ProxyCreationEnabled = false;

                var licenca = contexto.Licencas.FirstOrDefault(y => y.IdDaLicenca == idDaLicenca);

                return licenca;
            }
        }

        public void Adicionar(Licenca licenca)
        {
            using (var contexto = new LojaContext())
            {
                contexto.Licencas.Add(licenca);

                contexto.SaveChanges();
            }
        }
    }
}

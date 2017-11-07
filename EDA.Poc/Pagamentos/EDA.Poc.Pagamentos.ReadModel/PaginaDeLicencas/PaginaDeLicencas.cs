using System.Collections.Generic;
using System.Linq;
using EDA.Poc.Infraestrutura.ReadModel;
using System;
using EDA.Poc.Infraestrutura.Utils;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeLicencas.Eventos;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeLicencas
{
    public class PaginaDeLicencas : VersionedReadModel
    {
        public Guid IdDoCliente { get; set; }
        public Guid? IdDaEmpresa { get; set; }
        public Guid? IdDoUsuario { get; set; }
        public List<PaginaDeLicencasFaturaMensal> FaturasMensais { get; set; }

        public PaginaDeLicencas()
        {
            FaturasMensais = new List<PaginaDeLicencasFaturaMensal>();
        }

        public void RegistrarDebito(PaginaDeLicencasDebitoRegistradoNaFaturaDoCliente evento)
        {
            Update(evento);
        }

        public PaginaDeLicencasFaturaMensal ObterFaturaDoMes(int mes, int ano)
        {
            return FaturasMensais.SingleOrDefault(x => x.Mes == new Mes(mes, ano));
        }

        protected override void SetHandlers()
        {
            Handles<PaginaDeLicencasDebitoRegistradoNaFaturaDoCliente>(When);
        }

        protected override void SetReverseHandlers()
        {
            HandlesReverse<PaginaDeLicencasDebitoRegistradoNaFaturaDoCliente>(WhenReverse);
        }

        private void When(PaginaDeLicencasDebitoRegistradoNaFaturaDoCliente @event)
        {
            var debito = @event.ToPaginaDeLicencasFaturaMensalDebito();

            var faturaMensal = FaturasMensais.SingleOrDefault(x => x.Mes == debito.MesDaFatura);

            if (faturaMensal == null)
            {
                faturaMensal = new PaginaDeLicencasFaturaMensal(debito.MesDaFatura);

                FaturasMensais.Add(faturaMensal);
            }

            faturaMensal.RegistrarDebito(debito);
        }

        private void WhenReverse(PaginaDeLicencasDebitoRegistradoNaFaturaDoCliente @event)
        {
            var debito = @event.ToPaginaDeLicencasFaturaMensalDebito();

            var faturaMensal = FaturasMensais.SingleOrDefault(x => x.Mes == debito.MesDaFatura);

            if (faturaMensal != null)
            {
                faturaMensal.RemoverDebito(debito);
            }            
        }
    }
}

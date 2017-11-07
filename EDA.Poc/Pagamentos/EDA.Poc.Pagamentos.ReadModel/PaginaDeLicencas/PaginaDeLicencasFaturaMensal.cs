using System.Collections.Generic;
using System.Linq;
using EDA.Poc.Infraestrutura.Utils;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeLicencas
{
    public class PaginaDeLicencasFaturaMensal
    {
        public Mes Mes { get; set; }
        public List<PaginaDeLicencasFaturaMensalDebito> Debitos { get; set; }
        public decimal TotalDaFatura { get; set; }

        public string TotalDaFaturaLabel
        {
            get
            {
                return string.Format("R$ {0}", TotalDaFatura.ToString("F2"));
            }
        }

        public PaginaDeLicencasFaturaMensal(Mes mes)
        {
            Mes = mes;
            Debitos = new List<PaginaDeLicencasFaturaMensalDebito>();
        }

        public void RegistrarDebito(PaginaDeLicencasFaturaMensalDebito debito)
        {
            if (debito == null)
                return;

            if (debito.MesDaFatura != Mes)
                return;

            if (Debitos.Any(x => x.IdDoDebito == debito.IdDoDebito))
                return;

            Debitos.Add(debito);

            RecalcularTotalDaFatura();
        }

        private void RecalcularTotalDaFatura()
        {
            TotalDaFatura = Debitos.Sum(x => x.Valor);
        }

        public void RemoverDebito(PaginaDeLicencasFaturaMensalDebito debito)
        {
            if (debito == null)
                return;

            if (debito.MesDaFatura != Mes)
                return;

            if (Debitos.All(x => x.IdDoDebito != debito.IdDoDebito))
                return;

            Debitos.Remove(debito);

            RecalcularTotalDaFatura();
        }
    }
}
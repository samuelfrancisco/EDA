using System;
using System.Collections.Generic;
using System.Linq;
using EDA.Poc.Infraestrutura.Utils;

namespace EDA.Poc.Pagamentos.Domain
{
    public class FaturaMensal
    {
        public Mes Mes { get; private set; }
        public List<FaturaMensalDebito> Debitos { get; set; }

        public decimal TotalDaFatura
        {
            get { return Debitos.Sum(x => x.Valor); }
        }

        public FaturaMensal(Mes mes)
        {
            Mes = mes;
            Debitos = new List<FaturaMensalDebito>();
        }

        public void RegistrarDebito(FaturaMensalDebito debito)
        {
            if (debito == null) throw new ArgumentNullException("debito");

            if (Debitos.Any(x => x.Id == debito.Id))
                throw new InvalidOperationException(string.Format("Já foi registrado um débito com Id = {0}", debito.Id));

            if (Debitos.Any(x => x.IdDaOrdemDeCompra == debito.IdDaOrdemDeCompra))
                throw new InvalidOperationException(string.Format("Já foi registrado o débito do mês {0} referente à ordem de compra {1}", Mes, debito.IdDaOrdemDeCompra));

            Debitos.Add(debito);
        }
    }
}
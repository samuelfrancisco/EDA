using System;
using EDA.Poc.Infraestrutura.Utils;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeLicencas
{
    public class PaginaDeLicencasFaturaMensalDebito
    {
        public Guid IdDoDebito { get; set; }
        public Guid IdDoUsuario { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataDaCompra { get; set; }
        public Mes MesDaFatura { get; set; }
    }
}
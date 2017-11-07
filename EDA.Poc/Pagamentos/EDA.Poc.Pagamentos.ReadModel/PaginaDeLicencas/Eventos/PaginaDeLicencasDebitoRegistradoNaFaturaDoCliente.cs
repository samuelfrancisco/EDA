using System;
using EDA.Poc.Infraestrutura.ReadModel;
using EDA.Poc.Infraestrutura.Utils;
using EDA.Poc.Pagamentos.Events;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeLicencas.Eventos
{
    public class PaginaDeLicencasDebitoRegistradoNaFaturaDoCliente : ReadModelProcessedEvent
    {
        public Guid IdDoDebito { get; private set; }
        public Guid IdDoUsuario { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataDaCompra { get; private set; }
        public Mes MesDaFatura { get; private set; }

        private PaginaDeLicencasDebitoRegistradoNaFaturaDoCliente()
        {
            
        }

        public PaginaDeLicencasDebitoRegistradoNaFaturaDoCliente(PaginaDeLicencas paginaDeLicencas, 
                                                                 DebitoRegistradoNaFaturaDoCliente debitoRegistradoNaFaturaDoCliente)
            : base(paginaDeLicencas, debitoRegistradoNaFaturaDoCliente)
        {
            IdDoDebito = debitoRegistradoNaFaturaDoCliente.IdDoDebito;
            IdDoUsuario = debitoRegistradoNaFaturaDoCliente.IdDoUsuario;
            Valor = debitoRegistradoNaFaturaDoCliente.Valor;
            DataDaCompra = debitoRegistradoNaFaturaDoCliente.DataDaCompra;
            MesDaFatura = debitoRegistradoNaFaturaDoCliente.MesDaFatura;
        }

        public PaginaDeLicencasFaturaMensalDebito ToPaginaDeLicencasFaturaMensalDebito()
        {
            return new PaginaDeLicencasFaturaMensalDebito
                       {
                           IdDoDebito = IdDoDebito,
                           IdDoUsuario = IdDoUsuario,
                           Valor = Valor,
                           DataDaCompra = DataDaCompra,
                           MesDaFatura = MesDaFatura
                       };
        }
    }
}
using EDA.Poc.Infraestrutura.Utils;
using System;

namespace EDA.Poc.Pagamentos.Domain
{
    public class ProcessoDeCobrancaRecorrenteDebito
    {
        public Guid IdDoDebito { get; private set; }        
        public decimal Valor { get; private set; }
        public Mes MesDaFatura { get; set; }

        public ProcessoDeCobrancaRecorrenteDebito(Guid idDoDebito, decimal valor, Mes mesDaFatura)
        {
            IdDoDebito = idDoDebito;
            Valor = valor;
            MesDaFatura = mesDaFatura;
        }
    }
}

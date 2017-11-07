using System;

namespace EDA.Poc.Pagamentos.ReadModel.WebAPI
{
    public class LicencaId
    {
        public Guid IdDoCliente { get; set; }
        public string SiglaDoIdioma { get; set; }
        public Guid IdDaLicenca { get; set; }
        public Guid IdDoConteudo { get; set; }
    }
}

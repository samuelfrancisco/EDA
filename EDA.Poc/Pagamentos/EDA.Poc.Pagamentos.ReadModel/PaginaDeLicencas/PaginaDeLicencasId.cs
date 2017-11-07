using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeLicencas
{
    public class PaginaDeLicencasId
    {
        public Guid? IdDoCliente { get; set; }
        public Guid? IdDoUsuario { get; set; }
        public Guid? IdDaEmpresa { get; set; }
    }
}

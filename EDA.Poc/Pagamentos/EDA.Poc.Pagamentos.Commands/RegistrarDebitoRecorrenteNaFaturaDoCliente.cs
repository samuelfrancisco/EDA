﻿using EDA.Poc.Infraestrutura.Messaging;
using EDA.Poc.Infraestrutura.Utils;
using System;

namespace EDA.Poc.Pagamentos.Commands
{
    public class RegistrarDebitoRecorrenteNaFaturaDoCliente : Command
    {
        public Guid IdDoDebito { get; set; }
        public Guid IdDoProcessoDeCobrancaRecorrente { get; set; }
        public Guid IdDaCobrancaRecorrente { get; set; }
        public Guid IdDaCobranca { get; set; }
        public Guid IdDaOrdemDeCompra { get; set; }       
        public Guid IdDoUsuario { get; set; }
        public Guid IdDoCliente { get; set; }
        public Guid IdDoConteudo { get; set; }
        public DateTime DataDaCompra { get; set; }
        public decimal Valor { get; set; }
        public Mes MesDaFatura { get; set; }
    }
}

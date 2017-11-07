using System;
using EDA.Poc.Infraestrutura.Messaging;

namespace EDA.Poc.Pagamentos.Commands
{
    public class AvaliarConteudo : Command
    {
        public Guid IdDoCliente { get; set; }
        public Guid IdDoUsuario { get; set; }
        public Guid IdDoConteudo { get; set; }
        public int Avaliacao { get; set; }
        public string Conteudo { get; set; }
        public string NomeDoUsuario { get; set; }
    }
}


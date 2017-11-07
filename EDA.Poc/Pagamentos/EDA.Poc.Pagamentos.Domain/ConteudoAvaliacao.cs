using System;

namespace EDA.Poc.Pagamentos.Domain
{
    public class ConteudoAvaliacao
    {
        public Guid IdDoCliente { get; set; }
        public Guid IdDoConteudo { get; set; }
        public Guid IdDoUsuario { get; set; }
        public int Avaliacao { get; set; }
    }
}
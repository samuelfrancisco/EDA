using System;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo
{
    public class PalavraChave
    {
        public Guid IdDoConteudo { get; set; }

        public string SiglaDoIdioma { get; set; }

        public Guid IdDaPalavraChave { get; set; }

        public string Titulo { get; set; }
    }
}


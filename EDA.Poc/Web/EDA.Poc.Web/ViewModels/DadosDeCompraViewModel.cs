using EDA.Poc.Pagamentos.Domain.Enums;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;
using EDA.Poc.Web.Extensions.MvcExtensions.ValidationAttributes;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EDA.Poc.Web.ViewModels
{
    public class DadosDeCompraViewModel 
    {
        public DadosDeCompraViewModel()
        {
        }

        public DadosDeCompraViewModel(Precos precos)
        {
            IdDoConteudo = precos.IdDoConteudo;
            PrecosPorQuantidadeDeUsuarios = precos.PrecosPorQuantidadeDeUsuarios;
            PrecoIlimitado = precos.PrecoIlimitado;
            PrecoIlimitadoLabel = precos.PrecoIlimitadoLabel;
        }

        public Guid IdDoConteudo { get; set; }

        [MaiorQueZeroIf("TipoDeLicenciamento", TipoDeLicenciamento.QuantidadeLimitadaDeUsuarios, ErrorMessage = "Por favor, informe um número válido de licenças")]
        [RegularExpressionIf("^[0-9]+$","TipoDeLicenciamento", TipoDeLicenciamento.QuantidadeLimitadaDeUsuarios, ErrorMessage = "Por favor, informe um valor válido")]
        public int? QuantidadeDeUsuarios { get; set; }

        [MaiorQueZeroIf("TipoDeValidade", TipoDeValidade.QuantidadeLimitadaDeMeses, ErrorMessage = "Por favor, informe um número válido de meses")]
        [RegularExpressionIf("^[0-9]+$", "TipoDeValidade", TipoDeValidade.QuantidadeLimitadaDeMeses, ErrorMessage = "Por favor, informe um valor válido")]
        public int? QuantidadeDeMesesDeValidade { get; set; }

        public string PrecoIlimitadoLabel { get; set; }

        public decimal PrecoIlimitado { get; set; }

        [Required(ErrorMessage = "Por favor, informar a data de libreração do conteúdo")]
        public DateTime DisponibilizarAPartirDe { get; set; }

        public TipoDeLicenciamento TipoDeLicenciamento { get; set; }
        
        public TipoDeValidade TipoDeValidade { get; set; }

        public List<PrecoPorQuantidadeDeUsuarios> PrecosPorQuantidadeDeUsuarios { get; set; }
    }
}

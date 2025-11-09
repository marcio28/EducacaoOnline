using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.PagamentoEFaturamento.Domain.Validators;

namespace EducacaoOnline.PagamentoEFaturamento.Domain
{
    public class DadosCartao : ValueObject
    {
        public string NomeTitular { get; }
        public string NumeroCartao { get; }
        public string CodigoSeguranca { get; }
        public DateTime DataValidade { get; }

        public DadosCartao(string nomeTitular,
                           string numeroCartao,
                           string codigoSeguranca,
                           DateTime dataValidade)
        {
            NomeTitular = nomeTitular;
            NumeroCartao = numeroCartao;
            CodigoSeguranca = codigoSeguranca;
            DataValidade = dataValidade;
        }

        public override bool EhValido()
        {
            ValidationResult = new DadosCartaoValidator().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
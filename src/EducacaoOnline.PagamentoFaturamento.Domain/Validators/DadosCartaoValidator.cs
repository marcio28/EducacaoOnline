using FluentValidation;

namespace EducacaoOnline.PagamentoEFaturamento.Domain.Validators
{
    public class DadosCartaoValidator : AbstractValidator<DadosCartao>
    {
        public static string TamanhoNomeErroMsg => "O nome do titular do cartão deve conter 2 a 100 caracteres.";
        public static string NumeroCartaoErroMsg => "Número do cartão inválido.";
        public static string CodigoSegurancaErroMsg => "O código de segurança do cartão deve conter exatamente 3 números.";
        public static string DataValidadeErroMsg => "A data de validade do cartão deve ser uma data futura.";

        public DadosCartaoValidator()
        {
            RuleFor(d => d.NomeTitular)
                .Length(2, 100)
                .WithMessage(TamanhoNomeErroMsg);

            RuleFor(d => d.NumeroCartao)  // this regex that matches Visa, MasterCard, American Express, Diners Club, Discover, and JCB cards
                .Matches(@"^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$")
                .WithMessage(NumeroCartaoErroMsg);

            RuleFor(d => d.CodigoSeguranca)
                .Matches(@"^\d{3}$")
                .WithMessage(CodigoSegurancaErroMsg);

            RuleFor(d => d.DataValidade)
                .GreaterThan(DateTime.Now)
                .WithMessage(DataValidadeErroMsg);
        }
    }
}

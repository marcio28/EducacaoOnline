using EducacaoOnline.PagamentoEFaturamento.Application.Commands;
using FluentValidation;

namespace EducacaoOnline.PagamentoEFaturamento.Application.Validators
{
    public class RealizarPagamentoValidator : AbstractValidator<RealizarPagamentoCommand>
    {
        public static string DadosCartaoErroMsg => "Dados do cartão inválidos.";

        public RealizarPagamentoValidator()
        {
            RuleFor(c => c.ErrosCartao.Count)
                .Equal(0)
                .WithMessage(DadosCartaoErroMsg);
        }
    }
}
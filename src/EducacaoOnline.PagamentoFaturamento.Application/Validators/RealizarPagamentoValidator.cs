using EducacaoOnline.PagamentoEFaturamento.Application.Commands;
using EducacaoOnline.PagamentoEFaturamento.Domain;
using FluentValidation;

namespace EducacaoOnline.PagamentoEFaturamento.Application.Validators
{
    public class RealizarPagamentoValidator : AbstractValidator<RealizarPagamentoCommand>
    {
        public static string MatriculaNaoAguarandoPagamentoErroMsg => "Esta matrícula não está aguardando pagamento.";
        public static string DadosCartaoErroMsg => "Dados do cartão inválidos.";

        public RealizarPagamentoValidator()
        {
            RuleFor(c => c.StatusMatricula)
                .Equal(StatusMatricula.AguardandoPagamento)
                .WithMessage(MatriculaNaoAguarandoPagamentoErroMsg);

            RuleFor(c => c.ErrosCartao.Count)
                .Equal(0)
                .WithMessage(DadosCartaoErroMsg);
        }
    }
}
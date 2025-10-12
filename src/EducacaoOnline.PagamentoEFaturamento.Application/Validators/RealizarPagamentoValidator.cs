using FluentValidation;

namespace EducacaoOnline.PagamentoEFaturamento.Application.Validators
{
    public class RealizarPagamentoValidator : AbstractValidator<RealizarPagamentoCommand>
    {
        public static string IdMatriculaErroMsg => "Id da matrícula inválido.";
        public static string DadosCartaoErroMsg => "Dados do cartão inválidos.";

        public RealizarPagamentoValidator()
        {
            RuleFor(c => c.IdMatricula)
                .NotEqual(Guid.Empty)
                .WithMessage(IdMatriculaErroMsg);

            RuleFor(c => c.ErrosCartao.Count)
                .Equal(0)
                .WithMessage(DadosCartaoErroMsg);
        }
    }
}

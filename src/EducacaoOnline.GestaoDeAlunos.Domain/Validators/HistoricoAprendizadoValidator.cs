using FluentValidation;

namespace EducacaoOnline.GestaoDeAlunos.Domain.Validators
{
    public class HistoricoAprendizadoValidator : AbstractValidator<HistoricoAprendizado>
    {
        public static string IdMatriculaObrigatorioErroMsg => "O Id da matrícula deve ser informado.";

        public HistoricoAprendizadoValidator()
        {
            RuleFor(h => h.IdMatricula)
                .NotEqual(Guid.Empty).WithMessage(IdMatriculaObrigatorioErroMsg);
        }
    }
}
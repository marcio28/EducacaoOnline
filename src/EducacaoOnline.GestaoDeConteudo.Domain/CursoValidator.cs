using FluentValidation;

namespace EducacaoOnline.GestaoDeConteudo.Domain
{
    public class CursoValidator : AbstractValidator<Curso>
    {
        public CursoValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo Nome é obrigatório.")
                .Length(2, 100).WithMessage("O nome deve conter 2 a 100 caracteres.");

            RuleFor(c => c.ConteudoProgramatico)
                .NotNull().WithMessage("O conteúdo programático deve ser descrito.");
        }
    }
}

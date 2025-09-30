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

            RuleFor(c => c.ConteudoProgramatico.Descricao)
                .NotEmpty().WithMessage("A descrição do conteúdo programático é obrigatória.")
                .Length(10, 1000).WithMessage("A descrição deve conter 10 a 1000 caracteres.");
        }
    }
}

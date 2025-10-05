using FluentValidation;

namespace EducacaoOnline.GestaoDeConteudo.Domain.Validators
{
    public class AulaValidator : AbstractValidator<Aula>
    {
        public AulaValidator()
        {
            RuleFor(a => a.Id)
                .NotEqual(Guid.Empty);

            RuleFor(a => a.IdCurso)
                .NotEqual(Guid.Empty).WithMessage("O Id do curso deve ser informado.");

            RuleFor(a => a.Titulo)
                .NotEmpty().WithMessage("O campo Título é obrigatório.")
                .Length(2, 100).WithMessage("O título deve conter 2 a 100 caracteres.");

            RuleFor(a => a.Conteudo)
                .NotEmpty().WithMessage("É obrigatório descrever o conteúdo da aula.")
                .Length(10, 500).WithMessage("A descrição deve conter 10 a 500 caracteres.");
        }
    }
}
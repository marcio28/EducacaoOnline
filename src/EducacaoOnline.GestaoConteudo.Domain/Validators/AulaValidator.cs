using FluentValidation;

namespace EducacaoOnline.GestaoConteudo.Domain.Validators
{
    public class AulaValidator : AbstractValidator<Aula>
    {
        public static string IdCursoObrigatorioErroMsg => "O Id do curso deve ser informado.";
        public static string TamanhoTituloErroMsg => "O título deve conter 2 a 100 caracteres.";
        public static string TamanhoConteudoErroMsg => "O conteúdo deve conter 10 a 500 caracteres.";

        public AulaValidator()
        {
            RuleFor(a => a.IdCurso)
                .NotEqual(Guid.Empty).WithMessage(IdCursoObrigatorioErroMsg);

            RuleFor(a => a.Titulo)
                .Length(2, 100).WithMessage(TamanhoTituloErroMsg);

            RuleFor(a => a.Conteudo)
                .Length(10, 500).WithMessage(TamanhoConteudoErroMsg);
        }
    }
}
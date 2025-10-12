using FluentValidation;

namespace EducacaoOnline.GestaoDeConteudo.Domain.Validators
{
    public class CursoValidator : AbstractValidator<Curso>
    {
        public static string TamanhoNomeErroMsg => "O nome deve conter 2 a 100 caracteres.";
        public static string TamanhoConteudoErroMsg => "O conteúdo programático deve conter 10 a 1000 caracteres.";

        public CursoValidator()
        {
            RuleFor(c => c.Nome)
                .Length(2, 100)
                .WithMessage(TamanhoNomeErroMsg);

            RuleFor(c => c.ConteudoProgramatico.Descricao)
                .Length(10, 1000)
                .WithMessage(TamanhoConteudoErroMsg);
        }
    }
}

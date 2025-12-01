using FluentValidation;

namespace EducacaoOnline.GestaoConteudo.Domain.Validators
{
    public class CursoValidator : AbstractValidator<Curso>
    {
        public const int TamanhoNomeMinimo = 2;
        public const int TamanhoNomeMaximo = 100;

        public const int TamanhoConteudoMinimo = 10;
        public const int TamanhoConteudoMaximo = 1000;

        public const string TamanhoNomeErroMsg = "O nome do curso deve conter 2 a 100 caracteres.";

        public const string TamanhoConteudoErroMsg = "O conteúdo programático deve conter 10 a 1000 caracteres.";

        public CursoValidator()
        {
            RuleFor(c => c.Nome)
                .Length(TamanhoNomeMinimo, TamanhoNomeMaximo)
                .WithMessage(TamanhoNomeErroMsg);

            RuleFor(c => c.ConteudoProgramatico.Descricao)
                .Length(TamanhoConteudoMinimo, TamanhoConteudoMaximo)
                .WithMessage(TamanhoConteudoErroMsg);
        }
    }
}
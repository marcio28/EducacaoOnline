using FluentValidation;

namespace EducacaoOnline.GestaoConteudo.Domain.Validators
{
    public class CursoValidator : AbstractValidator<Curso>
    {
        public const int TamanhoNomeMinimo = 2;
        public const int TamanhoNomeMaximo = 100;

        public const int TamanhoConteudoMinimo = 10;
        public const int TamanhoConteudoMaximo = 1000;

        public static string TamanhoNomeErroMsg 
            => string.Format("O nome deve conter {0} a {1} caracteres.", TamanhoNomeMinimo, TamanhoNomeMaximo);

        public static string TamanhoConteudoErroMsg 
            => string.Format("O conteúdo programático deve conter {0} a {1} caracteres.", TamanhoConteudoMinimo, TamanhoConteudoMaximo);

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

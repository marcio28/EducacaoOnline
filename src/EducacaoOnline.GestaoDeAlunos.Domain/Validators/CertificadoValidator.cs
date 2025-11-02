using FluentValidation;

namespace EducacaoOnline.GestaoDeAlunos.Domain.Validators
{
    public class CertificadoValidator : AbstractValidator<Certificado>
    {
        public static string IdAlunoObrigatorioErroMsg => "O Id do aluno deve ser informado.";
        public static string IdCursoObrigatorioErroMsg => "O Id do curso deve ser informado.";

        public CertificadoValidator()
        {
            RuleFor(c => c.IdAluno)
                .NotEqual(Guid.Empty).WithMessage(IdAlunoObrigatorioErroMsg);

            RuleFor(c => c.IdCurso)
                .NotEqual(Guid.Empty).WithMessage(IdCursoObrigatorioErroMsg);
        }
    }
}
using FluentValidation;

namespace EducacaoOnline.GestaoAlunos.Domain.Validators
{
    public class CertificadoValidator : AbstractValidator<Certificado>
    {
        public static string IdMatriculaObrigatoriaErroMsg => "O Id da matrícula deve ser informado.";
        public static string IdAlunoObrigatorioErroMsg => "O Id do aluno deve ser informado.";
        public static string IdCursoObrigatorioErroMsg => "O Id do curso deve ser informado.";
        public static string DataEmissaoNaoFuturaErroMsg => "A data de emissão não pode ser no futuro.";

        public CertificadoValidator()
        {
            RuleFor(c => c.IdMatricula)
                .NotEqual(Guid.Empty).WithMessage(IdMatriculaObrigatoriaErroMsg);

            RuleFor(c => c.IdAluno)
                .NotEqual(Guid.Empty).WithMessage(IdAlunoObrigatorioErroMsg);

            RuleFor(c => c.IdCurso)
                .NotEqual(Guid.Empty).WithMessage(IdCursoObrigatorioErroMsg);

            RuleFor(c => c.DataDeEmissao)
                .LessThanOrEqualTo(DateTime.Now).WithMessage(DataEmissaoNaoFuturaErroMsg);
        }
    }
}
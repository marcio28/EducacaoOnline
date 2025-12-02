using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.GestaoAlunos.Domain.Validators;

namespace EducacaoOnline.GestaoAlunos.Domain
{
    public class Certificado : Entity
    {
        public Guid IdMatricula { get; private set; } 
        public Guid IdAluno { get; private set; }
        public Guid IdCurso { get; private set; }
        public DateTime DataDeEmissao { get; private set; }

        public Certificado(Guid idMatricula, Guid idAluno, Guid idCurso, DateTime dataDeEmissao)
        {
            IdMatricula = idMatricula;
            IdAluno = idAluno;
            IdCurso = idCurso;
            DataDeEmissao = dataDeEmissao;
        }

        public override bool EhValido()
        {
            ValidationResult = new CertificadoValidator().Validate(this);

            var ehValido = ValidationResult.IsValid;

            return ehValido;
        }
    }
}
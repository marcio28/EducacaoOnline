using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.GestaoAlunos.Domain.Validators;

namespace EducacaoOnline.GestaoAlunos.Domain
{
    public class Certificado : Entity
    {
        public Guid IdAluno { get; private set; }
        public Guid IdCurso { get; private set; }

        public Certificado(Guid idAluno, Guid idCurso)
        {
            IdAluno = idAluno;
            IdCurso = idCurso;
        }

        public override bool EhValido()
        {
            ValidationResult = new CertificadoValidator().Validate(this);

            var ehValido = ValidationResult.IsValid;

            return ehValido;
        }
    }
}
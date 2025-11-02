
using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.GestaoDeAlunos.Domain.Validators;

namespace EducacaoOnline.GestaoDeAlunos.Domain
{
    public class HistoricoAprendizado : Entity
    {
        public Guid IdMatricula { get; private set; }

        public HistoricoAprendizado(Guid idMatricula)
        {
            IdMatricula = idMatricula;
        }

        public override bool EhValido()
        {
            ValidationResult = new HistoricoAprendizadoValidator().Validate(this);

            var ehValido = ValidationResult.IsValid;

            return ehValido;
        }
    }
}

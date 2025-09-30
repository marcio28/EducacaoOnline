using FluentValidation.Results;

namespace EducacaoOnline.GestaoDeConteudo.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public ValidationResult? ValidationResult { get; protected set; }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}

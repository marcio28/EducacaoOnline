using FluentValidation.Results;

namespace EducacaoOnline.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public ValidationResult? ValidationResult { get; protected set; }
        public List<ValidationFailure> Erros => ValidationResult?.Errors ?? [];
        public int QuantidadeErros => Erros.Count;

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}


using FluentValidation.Results;

namespace EducacaoOnline.Core.DomainObjects.Messages
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult? ValidationResult { get; set; }
        public List<ValidationFailure> Erros => ValidationResult?.Errors ?? [];
        public int QuantidadeErros => Erros.Count;

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}

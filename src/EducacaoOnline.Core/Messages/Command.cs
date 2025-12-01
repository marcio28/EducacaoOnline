using FluentValidation.Results;
using MediatR;

namespace EducacaoOnline.Core.Messages
{
    public abstract class Command : Message, IRequest<bool>
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
            return true;
        }
    }
}
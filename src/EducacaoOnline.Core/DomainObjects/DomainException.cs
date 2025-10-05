using FluentValidation.Results;

namespace EducacaoOnline.Core.DomainObjects
{
    [Serializable]
    public class DomainException : Exception
    {
        public string[] RegrasVioladas = [];

        public DomainException() { }

        public DomainException(string? message) : base(message) { }

        public DomainException(string? message, Exception? innerException) : base(message, innerException) { }

        public DomainException(List<ValidationFailure>? validationFailures) : base()
        {
            ExtrairRegrasVioladas(validationFailures);
        }

        public DomainException(string? message, List<ValidationFailure>? validationFailures) : base(message)
        {
            ExtrairRegrasVioladas(validationFailures);
        }

        private void ExtrairRegrasVioladas(List<ValidationFailure>? validationFailures)
        {
            if (validationFailures is not null)
                RegrasVioladas = [.. validationFailures.Select(vf => vf.ErrorMessage)];
        }
    }
}
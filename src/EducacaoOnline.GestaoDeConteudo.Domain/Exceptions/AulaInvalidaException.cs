
using EducacaoOnline.Core.DomainObjects;
using FluentValidation.Results;

namespace EducacaoOnline.GestaoDeConteudo.Domain.Exceptions
{
    public class AulaInvalidaException : DomainException
    {
        public AulaInvalidaException(List<ValidationFailure>? validationFailures) : base("Aula inválida.", validationFailures) { }
    }
}

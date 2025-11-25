using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducacaoOnline.Core.DomainObjects
{
    public abstract class ValueObject
    {
        [NotMapped]
        public ValidationResult? ValidationResult { get; protected set; }
        
        [NotMapped]
        public List<ValidationFailure> Erros => ValidationResult?.Errors ?? [];
        
        public int QuantidadeErros => Erros.Count;

        public virtual bool EhValido()
        {
            return true;
        }
    }
}

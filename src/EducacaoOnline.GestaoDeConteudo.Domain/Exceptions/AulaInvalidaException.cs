
using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.GestaoDeConteudo.Domain.Exceptions
{
    public class AulaInvalidaException : DomainException
    {
        public AulaInvalidaException() : base("Aula inválida.") { }
    }
}

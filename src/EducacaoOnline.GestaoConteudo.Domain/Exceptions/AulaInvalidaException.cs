
using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.GestaoConteudo.Domain.Exceptions
{
    public class AulaInvalidaException : DomainException
    {
        public AulaInvalidaException() : base("Aula inválida.") { }
    }
}

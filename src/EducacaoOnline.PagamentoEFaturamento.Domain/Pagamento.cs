
using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.PagamentoEFaturamento.Domain
{
    public class Pagamento : Entity
    {
        public Guid IdMatricula { get; }

        public Pagamento(Guid idMatricula)
        {
            IdMatricula = idMatricula;
        }
    }
}

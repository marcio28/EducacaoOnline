
using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.PagamentoEFaturamento.Domain
{
    public class Matricula : Entity
    {
        public StatusMatricula Status { get; private set; }

        public Matricula(StatusMatricula statusMatricula)
        {
            Status = statusMatricula;
        }

        public void Ativar()
        {
            Status = StatusMatricula.Ativa;
        }
    }
}

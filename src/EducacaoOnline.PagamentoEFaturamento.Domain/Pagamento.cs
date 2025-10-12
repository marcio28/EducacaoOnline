using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.PagamentoEFaturamento.Domain
{
    public class Pagamento : Entity, IAggregateRoot
    {
        public Guid IdMatricula { get; }
        public StatusPagamento Status { get; private set; }

        public Pagamento(Guid idMatricula)
        {
            IdMatricula = idMatricula;
            Status = StatusPagamento.AguardandoConfirmacao;
        }

        public void AtualizarStatus(StatusPagamento status)
        {
            Status = status;
        }
    }
}
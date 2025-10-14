using EducacaoOnline.Core.Messages;

namespace EducacaoOnline.PagamentoEFaturamento.Application.Events
{
    public class PagamentoRealizadoEvent : Event
    {
        public Guid IdPagamento { get; }
        public Guid IdMatricula { get; }
        public PagamentoRealizadoEvent(Guid idPagamento, Guid idMatricula)
        {
            AggregateId = idPagamento;
            IdPagamento = idPagamento;
            IdMatricula = idMatricula;
        }
    }
}
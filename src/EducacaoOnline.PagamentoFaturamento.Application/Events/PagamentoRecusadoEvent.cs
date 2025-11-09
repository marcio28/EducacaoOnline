using EducacaoOnline.Core.Messages;
using FluentValidation.Results;

namespace EducacaoOnline.PagamentoEFaturamento.Application.Events
{
    public class PagamentoRecusadoEvent : Event
    {
        public Guid IdPagamento { get; }
        public Guid IdMatricula { get; }
        public List<ValidationFailure> ErrosCartao { get; }
        public PagamentoRecusadoEvent(Guid idPagamento,
                                      Guid idMatricula,
                                      List<ValidationFailure> errosCartao)
        {
            AggregateId = idPagamento;
            IdPagamento = idPagamento;
            IdMatricula = idMatricula;
            ErrosCartao = errosCartao;
        }
    }
}
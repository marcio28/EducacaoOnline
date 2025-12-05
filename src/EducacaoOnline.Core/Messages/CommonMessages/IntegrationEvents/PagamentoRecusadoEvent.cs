using FluentValidation.Results;

namespace EducacaoOnline.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PagamentoRecusadoEvent : Event
    {
        public Guid IdPagamento { get; }
        public Guid IdAluno { get; }
        public Guid IdMatricula { get; }
        public List<ValidationFailure> ErrosCartao { get; }

        public PagamentoRecusadoEvent(
            Guid idPagamento, Guid idAluno, Guid idMatricula, List<ValidationFailure> errosCartao)
        {
            AggregateId = idPagamento;
            IdAluno = idAluno;
            IdMatricula = idMatricula;
            IdPagamento = idPagamento;
            ErrosCartao = errosCartao;
        }
    }
}
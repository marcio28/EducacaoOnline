namespace EducacaoOnline.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PagamentoRealizadoEvent : Event
    {
        public Guid IdPagamento { get; }
        public Guid IdAluno { get; }
        public Guid IdMatricula { get; }

        public PagamentoRealizadoEvent(
            Guid idPagamento, Guid idAluno, Guid idMatricula)
        {
            AggregateId = idPagamento;
            IdAluno = idAluno;
            IdMatricula = idMatricula;
            IdPagamento = idPagamento;
        }
    }
}
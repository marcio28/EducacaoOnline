using EducacaoOnline.Core.Messages.CommonMessages.IntegrationEvents;

namespace EducacaoOnline.PagamentoFaturamento.Application.Tests.Events
{
    public class PagamentoRealizadoEventTests
    {
        [Fact(DisplayName = "PagamentoRealizadoEvent, propriedades preenchidas")]
        [Trait("Categoria", "Pagamento e Faturamento - PagamentoRealizadoEvent")]
        public void PagamentoRealizadoEvent_Propriedades_DevemEstarPreenchidas()
        {
            // Arrange
            var idPagamento = Guid.NewGuid();
            var idAluno = Guid.NewGuid();
            var idMatricula = Guid.NewGuid();

            // Act
            var pagamentoRealizadoEvent = new PagamentoRealizadoEvent(idPagamento, idAluno, idMatricula);

            // Assert
            Assert.Equal(idPagamento, pagamentoRealizadoEvent.IdPagamento);
            Assert.Equal(idAluno, pagamentoRealizadoEvent.IdAluno);
            Assert.Equal(idMatricula, pagamentoRealizadoEvent.IdMatricula);
        }
    }
}
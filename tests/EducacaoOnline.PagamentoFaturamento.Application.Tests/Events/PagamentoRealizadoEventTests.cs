using EducacaoOnline.PagamentoEFaturamento.Application.Events;

namespace EducacaoOnline.PagamentoEFaturamento.Application.Tests.Events
{
    public class PagamentoRealizadoEventTests
    {
        [Fact(DisplayName = "PagamentoRealizadoEvent, propriedades preenchidas")]
        [Trait("Categoria", "Pagamento e Faturamento - PagamentoRealizadoEvent")]
        public void PagamentoRealizadoEvent_Propriedades_DevemEstarPreenchidas()
        {
            // Arrange
            var idPagamento = Guid.NewGuid();
            var idMatricula = Guid.NewGuid();

            // Act
            var pagamentoRealizadoEvent = new PagamentoRealizadoEvent(idPagamento, idMatricula);

            // Assert
            Assert.Equal(idPagamento, pagamentoRealizadoEvent.IdPagamento);
            Assert.Equal(idMatricula, pagamentoRealizadoEvent.IdMatricula);
        }
    }
}
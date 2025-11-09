using EducacaoOnline.PagamentoEFaturamento.Application.Events;
using EducacaoOnline.PagamentoEFaturamento.Domain;

namespace EducacaoOnline.PagamentoEFaturamento.Application.Tests.Events
{
    public class PagamentoRecusadoEventTests
    {
        [Fact]
        [Trait("Categoria", "Pagamento e Faturamento - PagamentoRecusadoEvent")]
        public void PagamentoRecusadoEvent_Propriedades_DevemEstarPreenchidas()
        {
            // Arrange
            var idPagamento = Guid.NewGuid();
            var idMatricula = Guid.NewGuid();
            var dadosCartao = new DadosCartao(nomeTitular: "", numeroCartao: "", codigoSeguranca: "", dataValidade: DateTime.Now.AddDays(-1));

            // Act
            var pagamentoRecusadoEvent = new PagamentoRecusadoEvent(idPagamento, idMatricula, dadosCartao.Erros);

            // Assert
            Assert.Equal(idPagamento, pagamentoRecusadoEvent.IdPagamento);
            Assert.Equal(idMatricula, pagamentoRecusadoEvent.IdMatricula);
            Assert.Equal(dadosCartao.Erros, pagamentoRecusadoEvent.ErrosCartao);
        }
    }
}

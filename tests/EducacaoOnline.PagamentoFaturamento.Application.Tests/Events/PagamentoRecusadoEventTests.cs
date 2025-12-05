using EducacaoOnline.Core.Messages.CommonMessages.IntegrationEvents;
using EducacaoOnline.PagamentoEFaturamento.Domain;

namespace EducacaoOnline.PagamentoFaturamento.Application.Tests.Events
{
    public class PagamentoRecusadoEventTests
    {
        [Fact(DisplayName = "PagamentoRecusadoEvent, propriedades preenchidas")]
        [Trait("Categoria", "Pagamento e Faturamento - PagamentoRecusadoEvent")]
        public void PagamentoRecusadoEvent_Propriedades_DevemEstarPreenchidas()
        {
            // Arrange
            var idPagamento = Guid.NewGuid();
            var idAluno = Guid.NewGuid();
            var idMatricula = Guid.NewGuid();
            var dadosCartao = new DadosCartao(nomeTitular: "", numeroCartao: "", codigoSeguranca: "", dataValidade: DateTime.Now.AddDays(-1));

            // Act
            var pagamentoRecusadoEvent = new PagamentoRecusadoEvent(idPagamento, idAluno, idMatricula, dadosCartao.Erros);

            // Assert
            Assert.Equal(idPagamento, pagamentoRecusadoEvent.IdPagamento);
            Assert.Equal(idMatricula, pagamentoRecusadoEvent.IdMatricula);
            Assert.Equal(dadosCartao.Erros, pagamentoRecusadoEvent.ErrosCartao);
        }
    }
}
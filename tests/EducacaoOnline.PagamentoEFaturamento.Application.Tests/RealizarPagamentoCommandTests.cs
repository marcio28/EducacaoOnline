using EducacaoOnline.PagamentoEFaturamento.Domain;

namespace EducacaoOnline.PagamentoEFaturamento.Application.Tests
{
    public class RealizarPagamentoCommandTests
    {
        [Fact(DisplayName = "Realizar Pagamento Sem Violações É Válido")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento Commands")]
        public void RealizarPagamentoCommand_SemViolacoes_EhValido()
        {
            // Arrange
            var dadosCartao = new DadosCartao(nomeTitular: "Nome Teste",
                                              numeroCartao: "1234567890123456",
                                              codigoSeguranca: "123",
                                              dataValidade: DateTime.Now.AddYears(1));
            
            // Act
            var realizarPagamentoCommand = new RealizarPagamentoCommand(idMatricula: Guid.NewGuid(),
                                                                        dadosCartao: dadosCartao);

            // Assert
            Assert.True(realizarPagamentoCommand.EhValido());
        }
    }
}


using Xunit;

namespace EducacaoOnline.PagamentoEFaturamento.Domain.Tests
{
    public class PagamentoTests
    {
        [Fact(DisplayName = "Realizar Pagamento Sem Erros Ativa Matrícula")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento")]
        public void RealizarPagamento_SemErros_AtivaMatricula()
        {
            // Arrange
            var matricula = new Matricula();
            var dadosCartao = new DadosCartao(nomeTitular: "Nome Teste",
                                              numeroCartao: "1234567890123456",
                                              codigoSeguranca: "123",
                                              dataValidade: DateTime.Now.AddYears(1));
            var pagamento = new Pagamento(idMatricula: matricula.Id);

            // Act
            pagamento.Realizar(dadosCartao);

            // Assert
            Assert.Equal(0, pagamento.QuantidadeErros);
            Assert.True(pagamento.EhValido());
            // TODO: continuar
        }
    }
}

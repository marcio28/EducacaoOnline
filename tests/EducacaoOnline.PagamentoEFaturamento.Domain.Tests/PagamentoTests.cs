
using Xunit;

namespace EducacaoOnline.PagamentoEFaturamento.Domain.Tests
{
    public class PagamentoTests
    {
        [Fact(DisplayName = "Realizar Pagamento Sem Violacoes Ativa Matrícula")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento")]
        public void Trocar_Nome_Metodo()
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
            var semViolacoes = (pagamento.ValidationResult?.Errors.Count == 0);
            Assert.True(semViolacoes);

            Assert.True(pagamento.EhValido());
        }
    }
}

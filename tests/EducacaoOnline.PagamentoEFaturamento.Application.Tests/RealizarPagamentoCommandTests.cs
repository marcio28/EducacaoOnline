using EducacaoOnline.PagamentoEFaturamento.Application.Validators;
using EducacaoOnline.PagamentoEFaturamento.Domain;

namespace EducacaoOnline.PagamentoEFaturamento.Application.Tests
{
    public class RealizarPagamentoCommandTests
    {
        [Fact(DisplayName = "Realizar Pagamento Sem Erros É Válido")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento Commands")]
        public void RealizarPagamentoCommand_SemErros_EhValido()
        {
            // Arrange
            var dadosCartao = new DadosCartao(nomeTitular: "Nome Teste",
                                              numeroCartao: "4024007164015884",
                                              codigoSeguranca: "123",
                                              dataValidade: DateTime.Now.AddYears(1));
            
            // Act
            var realizarPagamentoCommand = new RealizarPagamentoCommand(idMatricula: Guid.NewGuid(),
                                                                        dadosCartao: dadosCartao);

            // Assert
            Assert.True(realizarPagamentoCommand.EhValido());
        }

        [Fact(DisplayName = "Realizar Pagamento Com Erros É Inválido")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento Commands")]
        public void RealizarPagamentoCommand_ComErros_EhInvalido()
        {
            // Arrange
            var dadosCartao = new DadosCartao(nomeTitular: "",
                                              numeroCartao: "",
                                              codigoSeguranca: "",
                                              dataValidade: DateTime.Now.AddDays(-1));

            // Act
            var realizarPagamentoCommand = new RealizarPagamentoCommand(idMatricula: Guid.Empty,
                                                                        dadosCartao: dadosCartao);

            // Assert
            Assert.False(realizarPagamentoCommand.EhValido());
            Assert.Equal(2, realizarPagamentoCommand.QuantidadeErros);
            Assert.Contains(RealizarPagamentoValidator.IdMatriculaErroMsg, realizarPagamentoCommand.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(RealizarPagamentoValidator.DadosCartaoErroMsg, realizarPagamentoCommand.Erros.Select(c => c.ErrorMessage));
        }
    }
}

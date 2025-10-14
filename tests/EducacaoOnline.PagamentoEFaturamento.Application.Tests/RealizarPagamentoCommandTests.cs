using EducacaoOnline.PagamentoEFaturamento.Application.Commands;
using EducacaoOnline.PagamentoEFaturamento.Application.Validators;
using EducacaoOnline.PagamentoEFaturamento.Domain;

namespace EducacaoOnline.PagamentoEFaturamento.Application.Tests
{
    public class RealizarPagamentoCommandTests
    {
        [Fact(DisplayName = "Realizar Pagamento Sem Erros É Válido")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento Commands")]
        public void RealizarPagamento_SemErros_DeveSerValido()
        {
            // Arrange
            var matricula = new Matricula(statusMatricula: StatusMatricula.AguardandoPagamento);
            var dadosCartao = new DadosCartao(nomeTitular: "Nome Teste",
                                              numeroCartao: "4024007164015884",
                                              codigoSeguranca: "123",
                                              dataValidade: DateTime.Now.AddYears(1));
            
            // Act
            var realizarPagamentoCommand = new RealizarPagamentoCommand(matricula: matricula,
                                                                        dadosCartao: dadosCartao);

            // Assert
            Assert.True(realizarPagamentoCommand.EhValido());
            Assert.Equal(0, realizarPagamentoCommand.QuantidadeErros);
        }

        [Fact(DisplayName = "Realizar Pagamento Matricula Não Aguarda Pagamento É Inválido")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento Commands")]
        public void RealizarPagamento_MatriculaNaoAguardaPagamento_DeveSerInvalido()
        {
            // Arrange
            var matricula = new Matricula(statusMatricula: StatusMatricula.Ativa);
            var dadosCartao = new DadosCartao(nomeTitular: "Nome Teste",
                                              numeroCartao: "4024007164015884",
                                              codigoSeguranca: "123",
                                              dataValidade: DateTime.Now.AddYears(1));

            // Act
            var realizarPagamentoCommand = new RealizarPagamentoCommand(matricula: matricula,
                                                                        dadosCartao: dadosCartao);

            // Assert
            Assert.False(realizarPagamentoCommand.EhValido());
            Assert.Equal(1, realizarPagamentoCommand.QuantidadeErros);
            Assert.Contains(RealizarPagamentoValidator.MatriculaNaoAguarandoPagamentoErroMsg, realizarPagamentoCommand.Erros.Select(c => c.ErrorMessage));
        }

        [Fact(DisplayName = "Realizar Pagamento Dados Cartão Com Erros É Inválido")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento Commands")]
        public void RealizarPagamento_DadosCartaoComErros_DeveSerInvalido()
        {
            // Arrange
            var matricula = new Matricula(statusMatricula: StatusMatricula.AguardandoPagamento);
            var dadosCartao = new DadosCartao(nomeTitular: "",
                                              numeroCartao: "",
                                              codigoSeguranca: "",
                                              dataValidade: DateTime.Now.AddDays(-1));

            // Act
            var realizarPagamentoCommand = new RealizarPagamentoCommand(matricula: matricula,
                                                                        dadosCartao: dadosCartao);

            // Assert
            Assert.False(realizarPagamentoCommand.EhValido());
            Assert.Equal(1, realizarPagamentoCommand.QuantidadeErros);
            Assert.Contains(RealizarPagamentoValidator.DadosCartaoErroMsg, realizarPagamentoCommand.Erros.Select(c => c.ErrorMessage));
        }
    }
}
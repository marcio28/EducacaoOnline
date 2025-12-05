using EducacaoOnline.PagamentoEFaturamento.Application.Commands;
using EducacaoOnline.PagamentoEFaturamento.Application.Validators;
using EducacaoOnline.PagamentoEFaturamento.Domain;

namespace EducacaoOnline.PagamentoEFaturamento.Application.Tests.Commands
{
    public class RealizarPagamentoCommandTests
    {
        private DadosCartao? _dadosCartao;
        private RealizarPagamentoCommand? _realizarPagamentoCommand;

        [Fact(DisplayName = "Realizar Pagamento Sem Erros É Válido")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento Commands")]
        public void RealizarPagamento_SemErros_DeveSerValido()
        {
            // Arrange
            Guid idAluno = Guid.NewGuid();
            Guid idMatricula = Guid.NewGuid();

            _dadosCartao = new(
                nomeTitular: "Nome Teste",
                numeroCartao: "4024007164015884",
                codigoSeguranca: "123",
                dataValidade: DateTime.Now.AddYears(1));

            // Act
            _realizarPagamentoCommand = new(idAluno, idMatricula, _dadosCartao);

            // Assert
            Assert.True(_realizarPagamentoCommand.EhValido());
            Assert.Equal(0, _realizarPagamentoCommand.QuantidadeErros);
        }

        [Fact(DisplayName = "Realizar Pagamento Dados Cartão Com Erros É Inválido")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento Commands")]
        public void RealizarPagamento_DadosCartaoComErros_DeveSerInvalido()
        {
            // Arrange
            Guid idAluno = Guid.NewGuid();
            Guid idMatricula = Guid.NewGuid();

            _dadosCartao = new(
                nomeTitular: "",
                numeroCartao: "",
                codigoSeguranca: "",
                dataValidade: DateTime.Now.AddDays(-1));

            // Act
            _realizarPagamentoCommand = new(idAluno, idMatricula, _dadosCartao);

            // Assert
            Assert.False(_realizarPagamentoCommand.EhValido());
            Assert.Equal(1, _realizarPagamentoCommand.QuantidadeErros);
            Assert.Contains(RealizarPagamentoValidator.DadosCartaoErroMsg, _realizarPagamentoCommand.Erros.Select(c => c.ErrorMessage));
        }
    }
}
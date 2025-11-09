using EducacaoOnline.PagamentoEFaturamento.Domain.Validators;

namespace EducacaoOnline.PagamentoEFaturamento.Domain.Tests
{
    public class DadosCartaoTests
    {
        private DadosCartao? _dadosCartao;

        [Fact(DisplayName = "Dados de Cartão Novo Sem Erros É Válido")]
        [Trait("Categoria", "Pagamento e Faturamento - Dados de Cartão")]
        public void DadosCartaoNovo_SemErros_DeveSerValido()
        {
            // Arrange & Act
            _dadosCartao = new(
                nomeTitular: "Nome Teste",
                numeroCartao: "4024007164015884",
                codigoSeguranca: "123",
                dataValidade: DateTime.Now.AddYears(1));

            // Assert
            Assert.True(_dadosCartao.EhValido());
        }

        [Fact(DisplayName = "Dados de Cartão Novo Com Erros É Inválido")]
        [Trait("Categoria", "Pagamento e Faturamento - Dados de Cartão")]
        public void DadosCartaoNovo_ComErros_DeveSerInvalido()
        {
            // Arrange & Act
            _dadosCartao = new(
                nomeTitular: "",
                numeroCartao: "",
                codigoSeguranca: "",
                dataValidade: DateTime.Now.AddDays(-1));

            // Assert
            Assert.False(_dadosCartao.EhValido());
            Assert.Equal(4, _dadosCartao.QuantidadeErros);
            Assert.Contains(DadosCartaoValidator.TamanhoNomeErroMsg, _dadosCartao.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(DadosCartaoValidator.NumeroCartaoErroMsg, _dadosCartao.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(DadosCartaoValidator.CodigoSegurancaErroMsg, _dadosCartao.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(DadosCartaoValidator.DataValidadeErroMsg, _dadosCartao.Erros.Select(c => c.ErrorMessage));
        }
    }
}
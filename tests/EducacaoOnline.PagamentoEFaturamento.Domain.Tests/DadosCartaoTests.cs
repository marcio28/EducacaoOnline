using EducacaoOnline.PagamentoEFaturamento.Domain.Validators;

namespace EducacaoOnline.PagamentoEFaturamento.Domain.Tests
{
    public class DadosCartaoTests
    {
        [Fact(DisplayName = "Dados de Cartão Novo Sem Erros É Válido")]
        [Trait("Categoria", "Pagamento e Faturamento - Dados de Cartão")]
        public void DadosCartaoNovo_SemErros_DeveSerValido()
        {
            // Arrange & Act
            var dadosCartao = new DadosCartao(nomeTitular: "Nome Teste",
                                              numeroCartao: "4024007164015884",
                                              codigoSeguranca: "123",
                                              dataValidade: DateTime.Now.AddYears(1));

            // Assert
            Assert.True(dadosCartao.EhValido());
        }

        [Fact(DisplayName = "Dados de Cartão Novo Com Erros É Inválido")]
        [Trait("Categoria", "Pagamento e Faturamento - Dados de Cartão")]
        public void DadosCartaoNovo_ComErros_DeveSerInvalido()
        {
            // Arrange & Act
            var dadosCartao = new DadosCartao(nomeTitular: "",
                                              numeroCartao: "",
                                              codigoSeguranca: "",
                                              dataValidade: DateTime.Now.AddDays(-1));

            // Assert
            Assert.False(dadosCartao.EhValido());
            Assert.Equal(4, dadosCartao.QuantidadeErros);
            Assert.Contains(DadosCartaoValidator.TamanhoNomeErroMsg, dadosCartao.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(DadosCartaoValidator.NumeroCartaoErroMsg, dadosCartao.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(DadosCartaoValidator.CodigoSegurancaErroMsg, dadosCartao.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(DadosCartaoValidator.DataValidadeErroMsg, dadosCartao.Erros.Select(c => c.ErrorMessage));
        }
    }
}
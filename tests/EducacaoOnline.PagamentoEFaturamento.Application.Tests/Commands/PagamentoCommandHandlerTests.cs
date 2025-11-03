using EducacaoOnline.PagamentoEFaturamento.Application.Commands;
using EducacaoOnline.PagamentoEFaturamento.Domain;
using Moq;
using Moq.AutoMock;

namespace EducacaoOnline.PagamentoEFaturamento.Application.Tests.Commands
{
    public class PagamentoCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly PagamentoCommandHandler _pagamentoCommandHandler;
        private Matricula? _matricula;
        private DadosCartao? _dadosCartao;
        private RealizarPagamentoCommand? _realizarPagamentoCommand;

        public PagamentoCommandHandlerTests() 
        {
            _mocker = new();
            _pagamentoCommandHandler = _mocker.CreateInstance<PagamentoCommandHandler>();
        }

        [Fact(DisplayName = "Realizar Pagamento Matrícula Aguardando Pagamento Deve Ativar Matrícula")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento Command Handler")]
        public async Task RealizarPagamento_MatriculaAguardandoPagamento_DeveAtivarMatriculaELancarEventoPagamentoRealizado()
        {
            // Arrange
            _matricula = new(StatusMatricula.AguardandoPagamento);

            _dadosCartao = new(
                nomeTitular: "Nome Teste",
                numeroCartao: "4024007164015884",
                codigoSeguranca: "123",
                dataValidade: DateTime.Now.AddYears(1));

            _realizarPagamentoCommand = new(_matricula, _dadosCartao);

            _mocker.GetMock<IPagamentoRepository>()
                .Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await _pagamentoCommandHandler.Handle(_realizarPagamentoCommand, CancellationToken.None);

            // Assert
            Assert.True(resultado);
            Assert.Equal(StatusMatricula.Ativa, _matricula.Status);
            _mocker.GetMock<IPagamentoRepository>().Verify(r => r.Adicionar(It.IsAny<Pagamento>()), Times.Once);
            _mocker.GetMock<IPagamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }


        [Fact(DisplayName = "Cartão Inválido Não Ativa Matrícula")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento Command Handler")]
        public async Task RealizarPagamento_CartaoInvalido_DeveNaoAtivarMatricula()
        {
            // Arrange
            _matricula = new(StatusMatricula.AguardandoPagamento);

            _dadosCartao = new(
                nomeTitular: "",
                numeroCartao: "",
                codigoSeguranca: "",
                dataValidade: DateTime.Now.AddDays(-1));

            _realizarPagamentoCommand = new(_matricula, _dadosCartao);

            _mocker.GetMock<IPagamentoRepository>()
                .Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await _pagamentoCommandHandler.Handle(_realizarPagamentoCommand, CancellationToken.None);

            // Assert
            Assert.False(resultado);
            Assert.Equal(StatusMatricula.AguardandoPagamento, _matricula.Status);
        }

        [Fact(DisplayName = "Pagamento Recusado Não Ativa Matrícula e Lança Exceção")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento Command Handler")]
        public async Task RealizarPagamento_PagamentoRecusado_DeveNaoAtivarMatriculaELancarEventoPagamentoRecusado()
        {
            // Arrange
            _matricula = new(StatusMatricula.AguardandoPagamento);

            _dadosCartao = new(
                nomeTitular: "Teste Falha",
                numeroCartao: "4024007164015884",
                codigoSeguranca: "123",
                dataValidade: DateTime.Now.AddYears(1));

            _realizarPagamentoCommand = new(_matricula, _dadosCartao);

            _mocker.GetMock<IPagamentoRepository>()
                .Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await _pagamentoCommandHandler.Handle(_realizarPagamentoCommand, CancellationToken.None);

            // Assert
            Assert.False(resultado);
            Assert.Equal(StatusMatricula.AguardandoPagamento, _matricula.Status);
            _mocker.GetMock<IPagamentoRepository>().Verify(r => r.Adicionar(It.IsAny<Pagamento>()), Times.Once);
            _mocker.GetMock<IPagamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
    }
}
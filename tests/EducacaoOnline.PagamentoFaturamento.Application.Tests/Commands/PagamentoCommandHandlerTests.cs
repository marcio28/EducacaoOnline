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
        private DadosCartao? _dadosCartao;
        private RealizarPagamentoCommand? _realizarPagamentoCommand;

        public PagamentoCommandHandlerTests() 
        {
            _mocker = new();
            _pagamentoCommandHandler = _mocker.CreateInstance<PagamentoCommandHandler>();
        }

        [Fact(DisplayName = "Realizar pagamento, cartão válido, salva pagamento e lança evento pagamento realizado")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento Command Handler")]
        public async Task RealizarPagamento_CartaoValido_DeveSalvarPagamentoERetornarVerdadeiro()
        {
            // Arrange
            Guid idAluno = Guid.NewGuid();
            Guid idMatricula = Guid.NewGuid();

            _dadosCartao = new(
                nomeTitular: "Nome Teste",
                numeroCartao: "4024007164015884",
                codigoSeguranca: "123",
                dataValidade: DateTime.Now.AddYears(1));

            _realizarPagamentoCommand = new(idAluno, idMatricula, _dadosCartao);

            _mocker.GetMock<IPagamentoRepository>()
                .Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await _pagamentoCommandHandler.Handle(_realizarPagamentoCommand, CancellationToken.None);

            // Assert
            Assert.True(resultado);
            _mocker.GetMock<IPagamentoRepository>().Verify(r => r.Adicionar(It.IsAny<Pagamento>()), Times.Once);
            _mocker.GetMock<IPagamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
            // TODO: Também poderia verificar se o evento "PagamentoRealizado" foi lançado
        }


        [Fact(DisplayName = "Realizar pagamento, cartão inválido, retorna falso")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento Command Handler")]
        public async Task RealizarPagamento_CartaoInvalido_DeveRetornarFalso()
        {
            // Arrange
            Guid idAluno = Guid.NewGuid();
            Guid idMatricula = Guid.NewGuid();

            _dadosCartao = new(
                nomeTitular: "",
                numeroCartao: "",
                codigoSeguranca: "",
                dataValidade: DateTime.Now.AddDays(-1));

            _realizarPagamentoCommand = new(idAluno, idMatricula, _dadosCartao);

            _mocker.GetMock<IPagamentoRepository>()
                .Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await _pagamentoCommandHandler.Handle(_realizarPagamentoCommand, CancellationToken.None);

            // Assert
            Assert.False(resultado);
        }

        [Fact(DisplayName = "Realizar pagamento, recusado, salva pagamento e retorna falso")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento Command Handler")]
        public async Task RealizarPagamento_PagamentoRecusado_DeveSalvarPagamentoERetornarFalso()
        {
            // Arrange
            Guid idAluno = Guid.NewGuid();
            Guid idMatricula = Guid.NewGuid();

            _dadosCartao = new(
                nomeTitular: "Teste Falha",
                numeroCartao: "4024007164015884",
                codigoSeguranca: "123",
                dataValidade: DateTime.Now.AddYears(1));

            _realizarPagamentoCommand = new(idAluno, idMatricula, _dadosCartao);

            _mocker.GetMock<IPagamentoRepository>()
                .Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await _pagamentoCommandHandler.Handle(_realizarPagamentoCommand, CancellationToken.None);

            // Assert
            Assert.False(resultado);
            _mocker.GetMock<IPagamentoRepository>().Verify(r => r.Adicionar(It.IsAny<Pagamento>()), Times.Once);
            _mocker.GetMock<IPagamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
            // TODO: Também poderia verificar se o status do pagamento é "Recusado"
        }
    }
}
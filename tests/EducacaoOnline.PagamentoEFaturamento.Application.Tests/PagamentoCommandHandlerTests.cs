using EducacaoOnline.PagamentoEFaturamento.Application.Commands;
using EducacaoOnline.PagamentoEFaturamento.Application.Events;
using EducacaoOnline.PagamentoEFaturamento.Domain;
using MediatR;
using Moq;
using Moq.AutoMock;

namespace EducacaoOnline.PagamentoEFaturamento.Application.Tests
{
    public class PagamentoCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly PagamentoCommandHandler _pagamentoCommandHandler;

        public PagamentoCommandHandlerTests() 
        {
            _mocker = new AutoMocker();
            _pagamentoCommandHandler = _mocker.CreateInstance<PagamentoCommandHandler>();
        }

        [Fact(DisplayName = "Realizar Pagamento Matrícula Aguardando Pagamento Deve Ativar Matrícula")]
        [Trait("Categoria", "Pagamento e Faturamento - Pagamento Command Handler")]
        public async Task RealizarPagamento_MatriculaAguardandoPagamento_DeveAtivarMatriculaELancarEventoPagamentoRealizado()
        {
            // Arrange
            var matricula = new Matricula(statusMatricula: StatusMatricula.AguardandoPagamento);
            var dadosCartao = new DadosCartao(nomeTitular: "Nome Teste",
                                              numeroCartao: "4024007164015884",
                                              codigoSeguranca: "123",
                                              dataValidade: DateTime.Now.AddYears(1));
            var realizarPagamentoCommand = new RealizarPagamentoCommand(matricula: matricula,
                                                                        dadosCartao: dadosCartao);

            _mocker.GetMock<IPagamentoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var resultado = await _pagamentoCommandHandler.Handle(realizarPagamentoCommand, CancellationToken.None);

            // Assert
            Assert.True(resultado);
            Assert.Equal(StatusMatricula.Ativa, matricula.Status);
            _mocker.GetMock<IPagamentoRepository>().Verify(r => r.Adicionar(It.IsAny<Pagamento>()), Times.Once);
            //_mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<PagamentoRealizadoEvent>(), It.IsAny<CancellationToken>()), Times.Once);
            _mocker.GetMock<IPagamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
    }
}
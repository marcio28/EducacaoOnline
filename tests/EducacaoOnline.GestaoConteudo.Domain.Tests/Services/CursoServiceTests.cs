using EducacaoOnline.Core.Tests;
using EducacaoOnline.GestaoConteudo.Domain.Repositories;
using EducacaoOnline.GestaoConteudo.Domain.Services;
using MediatR;
using Moq;
using Moq.AutoMock;

namespace EducacaoOnline.GestaoConteudo.Domain.Tests.Services
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    public class CursoServiceTests
    {
        private readonly AutoMocker _mocker;
        private Curso _cursoInvalido;
        private Curso _cursoValido;
        private ICursoService _cursoService;
        
        public CursoServiceTests()
        {
            // Arrange
            _mocker = new AutoMocker();
            _cursoInvalido = new(
                nome: "",
                conteudoProgramatico: new ConteudoProgramatico(""));
            _cursoValido = new(
                nome: $"Curso Válido {Guid.NewGuid()}",
                conteudoProgramatico: new ConteudoProgramatico("Conteúdo de Curso Válido"));
            _cursoService = _mocker.CreateInstance<CursoService>();
        }

        [Fact(DisplayName = "Incluir curso, inválido, não inclui")]
        [Trait("Categoria", "Gestão de Conteúdo - CursoService")]
        public async Task IncluirCurso_CursoInvalido_DeveNaoIncluir()
        {
            // Act
            await _cursoService.Incluir(_cursoInvalido, CancellationToken.None);

            // Assert
            Assert.False(_cursoInvalido.EhValido());
            _mocker.GetMock<ICursoRepository>().Verify(
                r => r.Incluir(_cursoInvalido, CancellationToken.None), Times.Never);
            _mocker.GetMock<IMediator>().Verify(
                m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Incluir curso, já existente, não inclui")]
        [Trait("Categoria", "Gestão de Conteúdo - CursoService")]
        public async Task IncluirCurso_JaExistente_DeveNaoIncluir()
        {
            // Arrange
            _mocker.GetMock<ICursoRepository>().Setup(
                r => r.ObterPorId(_cursoValido.Id, CancellationToken.None))
                .ReturnsAsync(_cursoValido);

            // Act
            await _cursoService.Incluir(_cursoValido, CancellationToken.None);

            // Assert
            Assert.True(_cursoValido.EhValido());
            _mocker.GetMock<ICursoRepository>().Verify(
                r => r.Incluir(_cursoValido, CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Incluir curso, nome já cadastrado, não inclui")]
        [Trait("Categoria", "Gestão de Conteúdo - CursoService")]
        public async Task IncluirCurso_NomeJaCadastrado_DeveNaoIncluir()
        {
            // Arrange
            _mocker.GetMock<ICursoRepository>().Setup(
                r => r.Existe(_cursoValido.Nome!, CancellationToken.None))
                .ReturnsAsync(true);

            // Act
            await _cursoService.Incluir(_cursoValido, CancellationToken.None);

            // Assert
            Assert.True(_cursoValido.EhValido());
            _mocker.GetMock<ICursoRepository>().Verify(
                r => r.Incluir(_cursoValido, CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Incluir curso, válido, inclui")]
        [Trait("Categoria", "Gestão de Conteúdo - CursoService")]
        public async Task IncluirCurso_CursoValido_DeveIncluir()
        {
            // Arrange
            _mocker.GetMock<ICursoRepository>().Setup(
                r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            await _cursoService.Incluir(_cursoValido, CancellationToken.None);

            // Assert
            Assert.True(_cursoValido.EhValido());
            _mocker.GetMock<ICursoRepository>().Verify(
                r => r.Incluir(_cursoValido, CancellationToken.None), Times.Once);
            _mocker.GetMock<ICursoRepository>().Verify(
                r => r.UnitOfWork.Commit(), Times.Once);
            // TODO: mocker.GetMock<IMediator>().Verify(
            //    m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Alterar curso, inválido, não altera")]
        [Trait("Categoria", "Gestão de Conteúdo - CursoService")]
        public async Task AlterarCurso_Invalido_DeveNaoAlterar()
        {
            // Act
            await _cursoService.Alterar(_cursoInvalido, CancellationToken.None);

            // Assert
            Assert.False(_cursoInvalido.EhValido());
            _mocker.GetMock<ICursoRepository>().Verify(
                r => r.Alterar(_cursoInvalido, CancellationToken.None), Times.Never);
            _mocker.GetMock<IMediator>().Verify(
                m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Alterar curso, inexistente, não altera")]
        [Trait("Categoria", "Gestão de Conteúdo - CursoService")]
        public async Task AlterarCurso_Inexistente_DeveNaoAlterar()
        {
            // Arrange
            _mocker.GetMock<ICursoRepository>().Setup(
                r => r.ObterPorId(_cursoValido.Id, CancellationToken.None))
                .ReturnsAsync((Curso?)null);

            // Act
            await _cursoService.Alterar(_cursoValido, CancellationToken.None);

            // Assert
            Assert.True(_cursoValido.EhValido());
            _mocker.GetMock<ICursoRepository>().Verify(
                r => r.Incluir(_cursoValido, CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Alterar curso, nome já cadastrado, não altera")]
        [Trait("Categoria", "Gestão de Conteúdo - CursoService")]
        public async Task AlterarCurso_NomeJaCadastrado_DeveNaoAlterar()
        {
            // Arrange
            var cursoExistente = new Curso(_cursoValido.Id);
            cursoExistente.Atualizar(
                nome: "Nome original",
                conteudoProgramatico: new ConteudoProgramatico("Conteúdo original"));
            _mocker.GetMock<ICursoRepository>().Setup(
                r => r.ObterPorId(_cursoValido.Id, CancellationToken.None))
                .ReturnsAsync(cursoExistente);
            _mocker.GetMock<ICursoRepository>().Setup(
                r => r.Existe(_cursoValido.Nome!, CancellationToken.None))
                .ReturnsAsync(true);

            // Act
            await _cursoService.Alterar(_cursoValido, CancellationToken.None);

            // Assert
            Assert.True(_cursoValido.EhValido());
            _mocker.GetMock<ICursoRepository>().Verify(
                r => r.Alterar(_cursoValido, CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Excluir curso, inexistente, não exclui")]
        [Trait("Categoria", "Gestão de Conteúdo - CursoService")]
        public async Task ExcluirCurso_Inexistente_DeveNaoExcluir()
        {
            // Arrange
            _mocker.GetMock<ICursoRepository>().Setup(
                r => r.ObterPorId(Guid.Empty, CancellationToken.None))
                .ReturnsAsync((Curso?)null);

            // Act
            await _cursoService.Excluir(_cursoValido.Id, CancellationToken.None);

            // Assert
            Assert.True(_cursoValido.EhValido());
            _mocker.GetMock<ICursoRepository>().Verify(
                r => r.Excluir(_cursoValido, CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter curso por id, inexistente, notifica insucesso")]
        [Trait("Categoria", "Gestão de Conteúdo - CursoService")]
        public async Task ObterPorId_Inexistente_DeveNotificarInsucesso()
        {
            // Arrange
            _mocker.GetMock<ICursoRepository>().Setup(
                r => r.ObterPorId(Guid.Empty, CancellationToken.None))
                .ReturnsAsync((Curso?)null);

            // Act
            await _cursoService.ObterPorId(Guid.Empty, CancellationToken.None);

            // Assert
            _mocker.GetMock<IMediator>().Verify(
                m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }
    }
}

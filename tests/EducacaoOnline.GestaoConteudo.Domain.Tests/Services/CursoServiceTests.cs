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
        private Curso? _curso;
        private ICursoService _cursoService;
        
        public CursoServiceTests()
        {
            _mocker = new AutoMocker();
            _cursoService = _mocker.CreateInstance<CursoService>();
        }

        [Fact(DisplayName = "Incluir curso, inválido, não inclui"), TestPriority(1)]
        [Trait("Categoria", "Gestão de Conteúdo - CursoService")]
        public async Task IncluirCurso_CursoInvalido_DeveNaoIncluir()
        {
            // Arrange
            _curso = new(
                nome: "",
                conteudoProgramatico: new ConteudoProgramatico(""));

            // Act
            await _cursoService.Incluir(_curso, CancellationToken.None);

            // Assert
            Assert.False(_curso.EhValido());
            _mocker.GetMock<ICursoRepository>().Verify(
                r => r.Incluir(_curso, CancellationToken.None), Times.Never);
            _mocker.GetMock<IMediator>().Verify(
                m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Incluir curso, válido, inclui"), TestPriority(2)]
        [Trait("Categoria", "Gestão de Conteúdo - CursoService")]
        public async Task IncluirCurso_CursoValido_DeveIncluir()
        {
            // Arrange
            _curso = new(
                nome: "Curso Válido",
                conteudoProgramatico: new ConteudoProgramatico("Conteúdo de Curso Válido"));
            _mocker.GetMock<ICursoRepository>().Setup(
                r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            await _cursoService.Incluir(_curso, CancellationToken.None);

            // Assert
            Assert.True(_curso.EhValido());
            _mocker.GetMock<ICursoRepository>().Verify(
                r => r.Incluir(_curso, CancellationToken.None), Times.Once);
            _mocker.GetMock<ICursoRepository>().Verify(
                r => r.UnitOfWork.Commit(), Times.Once);
            // TODO: mocker.GetMock<IMediator>().Verify(
            //    m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }
    }
}

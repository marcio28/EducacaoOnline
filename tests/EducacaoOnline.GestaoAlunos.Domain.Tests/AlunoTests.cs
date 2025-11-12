using EducacaoOnline.GestaoAlunos.Domain.Exceptions;

namespace EducacaoOnline.GestaoAlunos.Domain.Tests
{
    public class AlunoTests
    {
        private readonly Aluno _aluno;
        private Curso? _curso;

        public AlunoTests()
        {
            _aluno = new(Guid.NewGuid());
        }

        [Fact(DisplayName = "Iniciar Matrícula Sem Erros Aguarda Pagamento")]
        [Trait("Categoria", "Gestão de Alunos - Aluno")]
        public void IniciarMatricula_SemErros_DeveAguardarPagamento()
        {
            // Arrange
            _curso = new(disponivelMatricula: true);
            var quatidadeMatriculasAntes = _aluno.QuantidadeMatriculas;

            // Act
            var matricula = _aluno.IniciarMatricula(_curso);

            // Assert
            Assert.Equal(0, _aluno.QuantidadeErros);
            Assert.NotNull(matricula);
            Assert.Equal(_aluno.Id, matricula?.IdAluno);
            Assert.Equal(_curso.Id, matricula?.IdCurso);
            Assert.Equal(StatusMatricula.AguardandoPagamento, matricula?.Status);
            Assert.Equal(quatidadeMatriculasAntes + 1, _aluno.QuantidadeMatriculas);
        }

        [Fact(DisplayName = "Iniciar Matrícula Indisponível Lança Exceção")]
        [Trait("Categoria", "Gestão de Alunos - Aluno")]
        public void IniciarMatricula_Indisponivel_DeveLancarExcecao()
        {
            // Arrange
            _curso = new(disponivelMatricula: false);

            // Act && Assert
            Assert.Throws<MatriculaCursoIndisponivelException>(() => _aluno.IniciarMatricula(_curso));
        }
    }
}
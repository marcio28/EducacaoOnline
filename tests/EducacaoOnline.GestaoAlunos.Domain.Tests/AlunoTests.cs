using EducacaoOnline.GestaoAlunos.Domain.Exceptions;

namespace EducacaoOnline.GestaoAlunos.Domain.Tests
{
    public class AlunoTests
    {
        private readonly Aluno _aluno;
        private Guid _idCurso;

        public AlunoTests()
        {
            _aluno = new(Guid.NewGuid());
        }

        [Fact(DisplayName = "Iniciar matrícula, sem erros, aguarda pagamento")]
        [Trait("Categoria", "Gestão de Alunos - Aluno")]
        public void IniciarMatricula_SemErros_DeveAguardarPagamento()
        {
            // Arrange
            _idCurso = Guid.NewGuid();
            var quatidadeMatriculasAntes = _aluno.QuantidadeMatriculas;

            // Act
            var matricula = _aluno.IniciarMatricula(_idCurso);

            // Assert
            Assert.Equal(0, _aluno.QuantidadeErros);
            Assert.NotNull(matricula);
            Assert.Equal(_aluno.Id, matricula?.IdAluno);
            Assert.Equal(_idCurso, matricula?.IdCurso);
            Assert.Equal(StatusMatricula.AguardandoPagamento, matricula?.Status);
            Assert.Equal(quatidadeMatriculasAntes + 1, _aluno.QuantidadeMatriculas);
        }

        [Fact(DisplayName = "Iniciar matrícula, curso inválido, lança exceção")]
        [Trait("Categoria", "Gestão de Alunos - Aluno")]
        public void IniciarMatricula_CursoInvalido_DeveLancarExcecao()
        {
            // Arrange
            _idCurso = Guid.Empty;

            // Act && Assert
            Assert.Throws<MatriculaCursoInvalidoException>(() => _aluno.IniciarMatricula(_idCurso));
        }
    }
}
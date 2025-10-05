
using EducacaoOnline.GestaoDeAlunos.Domain.Exceptions;

namespace EducacaoOnline.GestaoDeAlunos.Domain.Tests
{
    public class AlunoTests
    {
        [Fact(DisplayName = "Iniciar Matrícula Sem Violações Aguarda Pagamento")]
        [Trait("Categoria", "Gestão de Alunos - Aluno")]
        public void IniciarMatricula_SemViolacoes_AguardaPagamento()
        {
            // Arrange
            var aluno = new Aluno();
            var cursoSelecionado = new Curso(disponivelMatricula: true);
            var quatidadeMatriculasAntes = aluno.Matriculas.Count;

            // Act
            var matricula = aluno.IniciarMatricula(cursoSelecionado);

            // Assert
            var semViolacoes = (aluno.ValidationResult?.Errors.Count == 0);
            Assert.True(semViolacoes);

            var matriculaCriada = (matricula is not null);
            Assert.True(matriculaCriada);

            var matriculaDoAlunoCorreto = (matricula?.IdAluno.Equals(aluno.Id));
            Assert.True(matriculaDoAlunoCorreto);

            var matriculaDoCursoCorreto = (matricula?.IdCurso.Equals(cursoSelecionado.Id));
            Assert.True(matriculaDoCursoCorreto);

            var matriculaAguardandoPagamento = (matricula?.Status.Equals(StatusMatricula.AguardandoPagamento));
            Assert.True(matriculaAguardandoPagamento);

            var umaMatriculaAMais = (aluno.Matriculas.Count.Equals(quatidadeMatriculasAntes + 1));
            Assert.True(umaMatriculaAMais);
        }

        [Fact(DisplayName = "Iniciar Matrícula Indisponível Lança Exceção")]
        [Trait("Categoria", "Gestão de Alunos - Aluno")]
        public void IniciarMatricula_Indisponivel_LancaExcecao()
        {
            // Arrange
            var aluno = new Aluno();
            var cursoSelecionado = new Curso(disponivelMatricula: false);

            // Act && Assert
            Assert.Throws<MatriculaCursoIndisponivelException>(() => aluno.IniciarMatricula(cursoSelecionado));
        }
    }
}

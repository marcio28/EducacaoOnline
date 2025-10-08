
using EducacaoOnline.GestaoDeAlunos.Domain.Exceptions;

namespace EducacaoOnline.GestaoDeAlunos.Domain.Tests
{
    public class AlunoTests
    {
        [Fact(DisplayName = "Iniciar Matrícula Sem Erros Aguarda Pagamento")]
        [Trait("Categoria", "Gestão de Alunos - Aluno")]
        public void IniciarMatricula_SemErros_AguardaPagamento()
        {
            // Arrange
            var aluno = new Aluno();
            var cursoSelecionado = new Curso(disponivelMatricula: true);
            var quatidadeMatriculasAntes = aluno.QuantidadeMatriculas;

            // Act
            var matricula = aluno.IniciarMatricula(cursoSelecionado);

            // Assert
            Assert.Equal(0, aluno.QuantidadeErros);
            Assert.NotNull(matricula);
            Assert.Equal(aluno.Id, matricula?.IdAluno);
            Assert.Equal(cursoSelecionado.Id, matricula?.IdCurso);
            Assert.Equal(StatusMatricula.AguardandoPagamento, matricula?.Status);
            Assert.Equal(quatidadeMatriculasAntes + 1, aluno.QuantidadeMatriculas);
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

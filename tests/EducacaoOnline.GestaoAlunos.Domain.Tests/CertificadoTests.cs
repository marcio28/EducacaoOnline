using EducacaoOnline.GestaoAlunos.Domain.Validators;

namespace EducacaoOnline.GestaoAlunos.Domain.Tests
{
    public class CertificadoTests
    {
        [Fact(DisplayName = "Certificado, sem erros, é válido")]
        [Trait("Categoria", "Gestão de Alunos - Certificado")]
        public void Certificado_SemErros_DeveSerValido()
        {
            // Arrange & Act
            var idMatricula = Guid.NewGuid();
            var idAluno = Guid.NewGuid();
            var idCurso = Guid.NewGuid();
            var dataDeEmissao = DateTime.Now;
            var certificado = new Certificado(idMatricula, idAluno, idCurso, dataDeEmissao);

            // Assert
            Assert.NotNull(certificado);
            Assert.Equal(0, certificado.QuantidadeErros);
            Assert.Equal(idAluno, certificado.IdAluno);
            Assert.Equal(idCurso, certificado.IdCurso);
            Assert.True(certificado.EhValido());
        }

        [Fact(DisplayName = "Certificado, com erros, é inválido")]
        [Trait("Categoria", "Gestão de Alunos - Certificado")]
        public void Certificado_ComErros_DeveSerInvalido()
        {
            // Arrange && Act
            var idMatricula = Guid.Empty;
            var idAluno = Guid.Empty;
            var idCurso = Guid.Empty;
            var dataDeEmissao = DateTime.Now.AddDays(1);
            var certificado = new Certificado(idMatricula, idAluno, idCurso, dataDeEmissao);

            // Assert
            Assert.False(certificado.EhValido());
            Assert.Equal(4, certificado.QuantidadeErros);
            Assert.Contains(CertificadoValidator.IdMatriculaObrigatoriaErroMsg, certificado.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(CertificadoValidator.IdAlunoObrigatorioErroMsg, certificado.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(CertificadoValidator.IdCursoObrigatorioErroMsg, certificado.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(CertificadoValidator.DataEmissaoNaoFuturaErroMsg, certificado.Erros.Select(c => c.ErrorMessage));
        }
    }
}
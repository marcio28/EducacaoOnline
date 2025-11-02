
using EducacaoOnline.GestaoDeAlunos.Domain.Validators;

namespace EducacaoOnline.GestaoDeAlunos.Domain.Tests
{
    public class CertificadoTests
    {
        [Fact(DisplayName = "Certificado Sem Erros É Válido")]
        [Trait("Categoria", "Gestão de Alunos - Certificado")]
        public void Certificado_SemErros_DeveSerValido()
        {
            // Arrange & Act
            var idAluno = Guid.NewGuid();
            var idCurso = Guid.NewGuid();
            var certificado = new Certificado(idAluno, idCurso);

            // Assert
            Assert.NotNull(certificado);
            Assert.Equal(0, certificado.QuantidadeErros);
            Assert.Equal(idAluno, certificado.IdAluno);
            Assert.Equal(idCurso, certificado.IdCurso);
            Assert.True(certificado.EhValido());
        }

        [Fact(DisplayName = "Certificado Com Erros É Inválido")]
        [Trait("Categoria", "Gestão de Alunos - Certificado")]
        public void Certificado_ComErros_DeveSerInvalido()
        {
            // Arrange && Act
            var idAluno = Guid.Empty;
            var idCurso = Guid.Empty;
            var certificado = new Certificado(idAluno, idCurso);

            // Assert
            Assert.False(certificado.EhValido());
            Assert.Equal(2, certificado.QuantidadeErros);
            Assert.Contains(CertificadoValidator.IdAlunoObrigatorioErroMsg, certificado.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(CertificadoValidator.IdCursoObrigatorioErroMsg, certificado.Erros.Select(c => c.ErrorMessage));
        }
    }
}
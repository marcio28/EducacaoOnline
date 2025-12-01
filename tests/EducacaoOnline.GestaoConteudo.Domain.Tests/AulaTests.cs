using EducacaoOnline.GestaoConteudo.Domain.Validators;

namespace EducacaoOnline.GestaoConteudo.Domain.Tests
{
    public class AulaTests
    {
        private Aula? _aula;

        [Fact(DisplayName = "Aula Nova Sem Erros É Válida")]
        [Trait("Categoria", "Gestão de Conteúdo - Aula")]
        public void AulaNova_SemErros_DeveSerValida()
        {
            // Arrange & Act
            _aula = new(
                idCurso: Guid.NewGuid(),
                titulo: "Introdução",
                conteudo: "Apresentação do curso, do professor e dos objetivos do curso.",
                nomeArquivoMaterial: default);

            // Assert
            Assert.True(_aula.EhValido());
            Assert.Equal(0, _aula.QuantidadeErros);
        }

        [Fact(DisplayName = "Aula Nova Com Erros É Inválida")]
        [Trait("Categoria", "Gestão de Conteúdo - Aula")]
        public void AulaNova_ComErros_DeveSerInvalida()
        {
            // Arrange & Act
            _aula = new(
                idCurso: Guid.Empty,
                titulo: "I",
                conteudo: "A",
                nomeArquivoMaterial: default);

            // Assert
            Assert.False(_aula.EhValido());
            Assert.Equal(3, _aula.QuantidadeErros);
            Assert.Contains(AulaValidator.IdCursoObrigatorioErroMsg, _aula.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(AulaValidator.TamanhoTituloErroMsg, _aula.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(AulaValidator.TamanhoConteudoErroMsg, _aula.Erros.Select(c => c.ErrorMessage));
        }

        [Fact(DisplayName = "Aula nova, com propriedades nulas, é inválida")]
        [Trait("Categoria", "Gestão de Conteúdo - Aula")]
        public void AulaNova_ComPropriedadesNulas_DeveSerInvalida()
        {
            // Arrange & Act
            _aula = new(
                idCurso: Guid.NewGuid(),
                titulo: null!,
                conteudo: null!,
                nomeArquivoMaterial: null);

            // Assert
            Assert.False(_aula.EhValido());
            Assert.Equal(2, _aula.QuantidadeErros);
            Assert.Contains(AulaValidator.TituloObrigatorioErroMsg, _aula.Erros.Select(c => c.ErrorMessage));
            Assert.Contains(AulaValidator.ConteudoObrigatorioErroMsg, _aula.Erros.Select(c => c.ErrorMessage));
        }

        [Fact(DisplayName = "Alterar propriedades, com dados válidos, deve alterar valores")]
        [Trait("Categoria", "Gestão de Conteúdo - Aula")]
        public void AlterarPropriedades_ComDadosValidos_DeveAlterarValores()
        {
            // Arrange
            var idCursoInicial = Guid.NewGuid();
            var tituloInicial = "Introdução";
            var conteudoInicial = "Apresentação do curso, do professor e dos objetivos do curso.";
            var nomeArquivoMaterialInicial = "material.pdf";
            _aula = new(
                idCurso: idCursoInicial,
                titulo: tituloInicial,
                conteudo: conteudoInicial,
                nomeArquivoMaterial: nomeArquivoMaterialInicial);
            var tituloNovo = "Aula 1 - Conceitos Básicos";
            var conteudoNovo = "Nesta aula, abordaremos os conceitos básicos necessários para entender o tema principal do curso.";
            var nomeArquivoMaterialNovo = "aula1_material.pdf";

            // Act
            var tipoAula = typeof(Aula);
            tipoAula.GetProperty("Titulo")!.SetValue(_aula, tituloNovo);
            tipoAula.GetProperty("Conteudo")!.SetValue(_aula, conteudoNovo);
            tipoAula.GetProperty("NomeArquivoMaterial")!.SetValue(_aula, nomeArquivoMaterialNovo);

            // Assert
            Assert.Equal(tituloNovo, _aula.Titulo);
            Assert.Equal(conteudoNovo, _aula.Conteudo);
            Assert.Equal(nomeArquivoMaterialNovo, _aula.NomeArquivoMaterial);
        }
    }
}
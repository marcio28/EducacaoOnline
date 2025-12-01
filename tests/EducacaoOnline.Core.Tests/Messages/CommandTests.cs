

using EducacaoOnline.Core.Messages;

namespace EducacaoOnline.Core.Tests.Messages
{
    public class CommandTests
    {
        [Fact(DisplayName = "Command, com validação inválida, é inválido")]
        [Trait("Categoria", "Core Messages - Command")]
        public void Command_ComValidacaoInvalida_DeveSerInvalido()
        {
            // Arrange
            var fakeCommand = new FakeCommand
            {
                TestePropriedade = "A"
            };

            // Act && Assert
            Assert.False(fakeCommand.EhValido());
            Assert.Equal(1, fakeCommand.QuantidadeErros);
            Assert.Contains("TestePropriedade deve ter mais de 2 caracteres.", fakeCommand.Erros.Select(c => c.ErrorMessage));
        }

        // Teste adicional para validação válida
        [Fact(DisplayName = "Command, com validação válida, é válido")]
        [Trait("Categoria", "Core Messages - Command")]
        public void Command_ComValidacaoValida_DeveSerValido()
        {
            // Arrange
            var fakeCommand = new FakeCommand
            {
                TestePropriedade = "TesteVálido"
            };

            // Act && Assert
            Assert.True(fakeCommand.EhValido());
            Assert.Equal(0, fakeCommand.QuantidadeErros);
        }
    }
    public class FakeCommand : Command
    {
        public string TestePropriedade { get; set; } = string.Empty;

        public override bool EhValido()
        {
            if (TestePropriedade.Length < 2)
            {
                ValidationResult = new FluentValidation.Results.ValidationResult(new[]
                {
                    new FluentValidation.Results.ValidationFailure(nameof(TestePropriedade), "TestePropriedade deve ter mais de 2 caracteres.")
                });
            }

            return Erros.Count == 0;
        }
    }

}
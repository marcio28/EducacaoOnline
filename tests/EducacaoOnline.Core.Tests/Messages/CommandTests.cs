

namespace EducacaoOnline.Core.Tests.Messages
{
    public class CommandTests
    {
        [Fact(DisplayName = "Command Sem Validação É Inválido")]
        [Trait("Categoria", "Core Messages - Command")]
        public void Command_SemValidacao_DeveSerInvalido()
        {
            // Arrange
            var testeCommand = new TesteCommand();

            // Act && Assert
            Assert.False(testeCommand.EhValido());
        }
    }
}
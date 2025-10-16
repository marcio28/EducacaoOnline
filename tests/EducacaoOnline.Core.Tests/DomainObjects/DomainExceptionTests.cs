using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.Core.Tests.DomainObjects
{
    public class DomainExceptionTests
    {
        private readonly string _message = "Esta é mensagem de exceção de domínio.";
        private DomainException? _domainException;

        [Fact(DisplayName = "DomainException Message")]
        [Trait("Categoria", "Core Domain Objects - DomainException")]
        public void DomainException_Message_DeveSerPassada()
        {
            // Arrange feito na declaração da propriedade

            // Act
            _domainException = new(_message);

            // Assert
            Assert.Equal(_message, _domainException.Message);
        }

        [Fact(DisplayName = "DomainException Inner Exception")]
        [Trait("Categoria", "Core Domain Objects - DomainException")]
        public void DomainException_InnerException_DeveSerPassada()
        {
            // Arrange
            var innerException = new Exception();

            // Act
            _domainException = new(_message, innerException);

            // Assert
            Assert.Equal(innerException, _domainException.InnerException);
        }
    }
}

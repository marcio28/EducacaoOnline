using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.Core.Tests.DomainObjects
{
    public class ValueObjectsTests
    {
        [Fact(DisplayName = "ValueObject Sem Validação É Inválido")]
        [Trait("Categoria", "Core Domain Objects - ValueObject")]
        public void ValueObject_SemValidacaoImplementada_DeveSerInvalido()
        {
            // Arrange
            var valueObjectFake = new ValueObjectFake();

            // Act & Assert
            Assert.False(valueObjectFake.EhValido());
        }
    }

    public class ValueObjectFake : ValueObject { }
}

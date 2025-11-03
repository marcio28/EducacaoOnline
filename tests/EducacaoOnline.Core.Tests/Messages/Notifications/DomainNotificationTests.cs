using EducacaoOnline.Core.Messages.DomainNotifications;

namespace EducacaoOnline.Core.Tests.Messages.Notifications
{
    public class DomainNotificationTests
    {
        [Fact(DisplayName = "DomainNotification Gets")]
        [Trait("Categoria", "Core Messages - DomainNotification")]
        public void DomainNotification_Propriedades_DevemEstarPreenchidas()
        {
            // Arrange
            var chave = "ChaveTeste";
            var valor = "ValorTeste";

            // Act
            var domainNotification = new DomainNotification(chave, valor);

            // Assert
            Assert.NotEqual(Guid.Empty, domainNotification.Id);
            var dataHoraEhRecente = (DateTime.Now - domainNotification.DataHora).TotalSeconds < 1;
            Assert.True(dataHoraEhRecente);
            Assert.Equal(chave, domainNotification.Chave);
            Assert.Equal(valor, domainNotification.Valor);
            Assert.Equal(1, domainNotification.Versao);
        }
    }
}
using EducacaoOnline.Core.Messages.Notifications;

namespace EducacaoOnline.Core.Tests.Messages.Notifications
{
    public class DomainNotificationHandlerTests
    {
        [Fact(DisplayName = "DomainNotificationHandler Gets")]
        [Trait("Categoria", "Core Messages - DomainNotificationHandler")]
        public void DomainNotificationHandler_Propriedades_DevemEstarPreenchidas()
        {
            // Arrange & Act
            var domainNotificationHandler = new DomainNotificationHandler();

            // Assert
            Assert.NotNull(domainNotificationHandler);
            Assert.Empty(domainNotificationHandler.ObterNotificacoes());
            Assert.False(domainNotificationHandler.TemNotificacoes());
        }
    }
}

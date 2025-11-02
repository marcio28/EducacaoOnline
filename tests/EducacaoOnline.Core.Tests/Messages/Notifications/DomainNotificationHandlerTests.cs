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

        [Fact(DisplayName = "DomainNotificationHandler Handle Adiciona Notificação")]
        [Trait("Categoria", "Core Messages - DomainNotificationHandler")]
        public async Task DomainNotificationHandler_Handle_DeveAdicionarNotificacao()
        {
            // Arrange
            var domainNotificationHandler = new DomainNotificationHandler();
            var notification = new DomainNotification("CHAVE_TESTE", "VALOR_TESTE");

            // Act
            await domainNotificationHandler.Handle(notification, CancellationToken.None);

            // Assert
            var notificacoes = domainNotificationHandler.ObterNotificacoes();
            Assert.NotNull(notificacoes);
            Assert.Single(notificacoes);
            Assert.Contains(notification, notificacoes);
            Assert.True(domainNotificationHandler.TemNotificacoes());
        }

    }
}

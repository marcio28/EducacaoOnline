
namespace EducacaoOnline.Core.Messages.ApplicationNotifications
{
    public class ApplicationNotification(string message)
    {
        public string Message { get; private set; } = message;
    }
}
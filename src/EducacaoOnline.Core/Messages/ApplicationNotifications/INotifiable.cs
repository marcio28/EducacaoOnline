
namespace EducacaoOnline.Core.Messages.ApplicationNotifications
{
    public interface INotifiable
    {
        bool IsValid();
        bool IsInvalid();
        List<ApplicationNotification> GetNotifications();
        void AddNotification(ApplicationNotification notification);
    }
}
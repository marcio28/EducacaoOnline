
namespace EducacaoOnline.Core.Messages.ApplicationNotifications
{
    public class Notifiable : INotifiable
    {
        private readonly List<ApplicationNotification> _notifications;
        
        public Notifiable()
        {
            _notifications = [];
        }
        
        public void AddNotification(ApplicationNotification notification)
        {
            _notifications.Add(notification);
        }

        public List<ApplicationNotification> GetNotifications()
        {
            return _notifications;
        }

        public bool IsInvalid()
        {
            return _notifications.Count is not 0;
        }

        public bool IsValid()
        {
            return _notifications.Count is 0;
        }
    }
}
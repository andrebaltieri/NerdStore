using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NerdStore.OrderContext.Shared.Notifications
{
    public class NotifiableObject
    {
        public NotifiableObject()
        {
            _notifications = new List<DomainNotification>();
        }

        private List<DomainNotification> _notifications { get; set; }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }

        public void AddNotification(DomainNotification notification)
        {
            if (notification == null)
                return;

            _notifications.Add(notification);
        }

        public void AddNotifications(List<DomainNotification> notifications)
        {
            if (notifications == null)
                return;

            _notifications.AddRange(notifications);
        }

        public void AddNotifications(ReadOnlyCollection<DomainNotification> notifications)
        {
            if (notifications == null)
                return;

            _notifications.AddRange(notifications);
        }

        public ReadOnlyCollection<DomainNotification> GetNotifications()
        {
            return _notifications.AsReadOnly();
        }
    }
}

using System;

namespace NerdStore.OrderContext.Shared.Notifications
{
    public class DomainNotification
    {
        public DomainNotification()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
        }

        public DomainNotification(string key, string value)
        {
            Id = Guid.NewGuid();
            Key = key;
            Value = value;
            Date = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public DateTime Date { get; private set; }
    }
}

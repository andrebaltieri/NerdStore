using NerdStore.OrderContext.Domain.ValueObjects;
using NerdStore.OrderContext.Shared.Entities;

namespace NerdStore.OrderContext.Domain.Entities
{
    public class Customer : Entity
    {
        public Customer(Name name, Age age, Document document)
        {
            Name = name;
            Age = age;
            Document = document;

            AddNotifications(name.GetNotifications());
            AddNotifications(age.GetNotifications());
            AddNotifications(document.GetNotifications());
        }

        public Name Name { get; private set; }
        public Age Age { get; private set; }
        public Document Document { get; private set; }
    }
}

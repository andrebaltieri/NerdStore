using NerdStore.OrderContext.Domain.Resources;
using NerdStore.OrderContext.Shared.Validation;
using NerdStore.OrderContext.Shared.ValueObjects;
using System;

namespace NerdStore.OrderContext.Domain.ValueObjects
{
    public class Name : ValueObject<Name>
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotification(Assert.NotEmpty(FirstName, "FirstName", Notifications.FirstNameIsInvalid));
            AddNotification(Assert.NotEmpty(LastName, "LastName", Notifications.LastNameIsInvalid));
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        protected override bool EqualsCore(Name other)
        {            
            return Equals(other.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }

        // Precisa melhorar este método
        protected override int GetHashCodeCore()
        {
            unchecked
            {
                var hashcode = FirstName.GetHashCode();
                hashcode = (hashcode * 397) ^ LastName.GetHashCode();

                return hashcode;
            }
        }
    }
}

using NerdStore.OrderContext.Domain.Resources;
using NerdStore.OrderContext.Shared.Validation;
using NerdStore.OrderContext.Shared.ValueObjects;
using System;

namespace NerdStore.OrderContext.Domain.ValueObjects
{
    public class Age : ValueObject<Age>
    {
        public DateTime BirthDate { get; }
        public int Value => DateTime.Now.Year - BirthDate.Year;

        public Age(DateTime dataDeNascimento)
        {
            BirthDate = dataDeNascimento;
            AddNotification(Assert.IsNotNull(BirthDate, "BirthDate", Notifications.BirthDateIsInvalid));
        }

        protected override bool EqualsCore(Age other)
        {
            return BirthDate == other.BirthDate;
        }

        protected override int GetHashCodeCore()
        {
            return BirthDate.GetHashCode();
        }
    }
}

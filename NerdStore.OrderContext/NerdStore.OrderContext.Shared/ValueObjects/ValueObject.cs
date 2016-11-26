using NerdStore.OrderContext.Shared.Notifications;

namespace NerdStore.OrderContext.Shared.ValueObjects
{
    public abstract class ValueObject<T> : NotifiableObject
        where T : ValueObject<T>
    {
        public override bool Equals(object valueObject)
        {
            var vo = valueObject as T;

            if (ReferenceEquals(vo, null))
                return false;

            return EqualsCore(vo);
        }

        protected abstract bool EqualsCore(T other);

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected abstract int GetHashCodeCore();

        public static bool operator ==(ValueObject<T> objectA,
            ValueObject<T> objectB)
        {
            if (ReferenceEquals(objectA, null) && ReferenceEquals(objectB, null))
                return true;

            if (ReferenceEquals(objectA, null) || ReferenceEquals(objectB, null))
                return false;

            return objectA.Equals(objectB);
        }

        public static bool operator !=(ValueObject<T> objectA,
            ValueObject<T> objectB)
        {
            return !(objectA == objectB);
        }
    }
}

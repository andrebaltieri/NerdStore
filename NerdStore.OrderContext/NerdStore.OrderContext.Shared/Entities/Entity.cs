using NerdStore.OrderContext.Shared.Notifications;
using System;

namespace NerdStore.OrderContext.Shared.Entities
{
    public abstract class Entity : NotifiableObject
    {
        public Guid Id { get; private set; }

        public override bool Equals(object entity)
        {
            var tempEntity = entity as Entity;

            if (ReferenceEquals(tempEntity, null))
                return false;

            if (ReferenceEquals(this, tempEntity))
                return true;

            if (GetType() != tempEntity.GetType())
                return false;

            if (Id == Guid.Empty)
                return false;

            return Id == tempEntity.Id;
        }

        public static bool operator ==(Entity entityA, Entity entityB)
        {
            if (ReferenceEquals(entityA, null) && ReferenceEquals(entityB, null))
                return true;

            if (ReferenceEquals(entityA, null) || ReferenceEquals(entityB, null))
                return false;

            return entityA.Equals(entityB);
        }

        public static bool operator !=(Entity entityA, Entity entityB)
        {
            return !(entityA == entityB);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}

using System;

namespace Code.Core
{
    /// <summary>
    /// An EntityId represents an dynamic object within the game.
    /// It has been instantiated and it has an EntityType
    /// </summary>
    [Serializable]
    public struct EntityId
    {
        public static EntityId Invalid = default;

        public readonly long ReferenceId;

        public EntityId(long referenceId)
        {
            ReferenceId = referenceId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is EntityId entityId && Equals(entityId);
        }

        public override int GetHashCode()
        {
            return (int)ReferenceId;
        }

        public bool Equals(EntityId other)
        {
            return ReferenceId == other.ReferenceId;
        }

        public static bool operator ==(EntityId p1, EntityId p2)
        {
            return p1.ReferenceId == p2.ReferenceId;
        }

        public static bool operator !=(EntityId p1, EntityId p2)
        {
            return p1.ReferenceId != p2.ReferenceId;
        }

        public override string ToString()
        {
            return $"EntityId: '{ReferenceId}'";
        }

        public bool IsValid()
        {
            return this != default;
        }

        public static void Validate(long referenceId)
        {
            if (Invalid.ReferenceId == referenceId)
            {
                throw new Exception(
                    $"The EntityId {referenceId} is invalid. Please use the EntityIdFactory to create a valid Id.");
            }
        }
        public static void Validate(EntityId referenceId)
        {
            if (Invalid == referenceId)
            {
                throw new Exception(
                    $"The EntityId {referenceId} is invalid. Please use the EntityIdFactory to create a valid Id.");
            }
        }
    }
}
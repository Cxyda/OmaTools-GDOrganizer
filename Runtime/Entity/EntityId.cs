using System;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// An RoomId represents an dynamic object within the game.
    /// It has been instantiated and it has an EntityType
    /// </summary>
    [Serializable]
    public struct EntityId
    {
        public static EntityId Invalid = default(EntityId);

        public uint ReferenceId;

        public EntityId(uint referenceId)
        {
            ReferenceId = referenceId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is EntityId && Equals((EntityId)obj);
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
            return $"RoomId: '{ReferenceId}'";
        }
    }
}
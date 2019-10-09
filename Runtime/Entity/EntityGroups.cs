using System;
using System.Collections;
using Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    [Serializable]
    public struct EntityGroups
    {
        public ulong Value;

        public BitArray bits;
        public bool HasFlag(EntityGroup group)
        {
            var groupFlag = (ulong) group >> 1;
            return (Value & groupFlag) == 1L;
        }

        public void SetGroup(EntityGroup group)
        {
            Value |= (ulong) group;
        }
        
        public static implicit operator EntityGroup (EntityGroups groups)
        {
            return (EntityGroup) groups.Value;
        }
    }
}
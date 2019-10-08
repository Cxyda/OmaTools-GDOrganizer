using System;
using System.Collections;
using System.Collections.Generic;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Generated;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    [Serializable]
    public struct LongEntityGroup
    {
        public ulong Value;

        public BitArray bits;
        public bool HasFlag(EntityGroup groupFlag)
        {
            if(bits == null)
                bits = new BitArray(BitConverter.GetBytes(Value));

            return bits[(int) groupFlag -1];
        }
        public static implicit operator EntityGroup (LongEntityGroup group)
        {
            return (EntityGroup) group.Value;
        }
    }

    [Serializable]
    public struct AnotherEntityGroup
    {
        public List<string> Groups;

        public bool HasFlag(params EntityGroup[] groups)
        {
            var contains = false;
            foreach (var group in groups)
            {
                var containsCurrentGroup = Groups.Contains(group.ToString());
                if (contains && !containsCurrentGroup)
                {
                    contains = false;
                    break;
                }

                contains = containsCurrentGroup;
            }

            return contains;
        }
    }
}
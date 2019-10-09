using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.ExtensionMethods
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static class EnumExtensions
    {
        public static IEnumerable<EntityGroup> GetFlags(this EntityGroup flags)
        {
            ulong flag = 1;
            foreach (var value in Enum.GetValues(flags.GetType()).Cast<EntityGroup>())
            {
                ulong bits = Convert.ToUInt64(value);
                while (flag < bits)
                {
                    flag <<= 1;
                }

                if (flag == bits && flags.HasFlag(value))
                {
                    yield return value;
                }
            }
        }
    }
}
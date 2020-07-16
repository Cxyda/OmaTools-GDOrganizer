using System;
using System.Collections.Generic;
using O.M.A.Games.GDOrganizer.Generated;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.ExtensionMethods
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static class EntityDefinitionExtensions
    {
        private static Dictionary<int, EntityProperty> _groupsCache = new Dictionary<int, EntityProperty>();
        
        public static bool TryToGroup(this EntityType type, out EntityProperty entityProperty)
        {
            entityProperty = default;
            var typeHash = type.GetHashCode();
            if (_groupsCache.ContainsKey(typeHash))
            {
                entityProperty = _groupsCache[typeHash];
                return true;
            }

            if (!Enum.TryParse(type.ToString(), out EntityProperty groupType))
            {
                return false;
            }

            _groupsCache[typeHash] = groupType;
            entityProperty = groupType;
            return true;
        }
    }
}
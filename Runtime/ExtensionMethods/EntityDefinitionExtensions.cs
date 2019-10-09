using System;
using System.Collections.Generic;
using Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.ExtensionMethods
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static class EntityDefinitionExtensions
    {
        private static Dictionary<int, EntityGroup> _groupsCache = new Dictionary<int, EntityGroup>();
        /// <summary>
        /// Returns the Groups of the entity as Flag
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static EntityGroups Groups(this EntityType type)
        {
            var typeDefinition = GddLoader.GddLoader.GetEntityTypeDefinition(type);
            return typeDefinition.EntityGroups;
        }
        
        public static EntityGroup ToGroup(this EntityType type)
        {
            var typeHash = type.GetHashCode();
            if (_groupsCache.ContainsKey(typeHash))
            {
                return _groupsCache[typeHash];
            }

            if (!Enum.TryParse(type.ToString(), out EntityGroup groupType))
            {
                groupType = 0;
            }

            _groupsCache[typeHash] = groupType;
            return groupType;
        }
    }
}
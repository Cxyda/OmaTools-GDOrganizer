using System;
using System.Collections;
using System.Collections.Generic;
using Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    [Serializable]
    public class EntityProperties
    {
        public List<EntityProperty> Properties = new List<EntityProperty>();

        private Dictionary<int, EntityProperty> _cache = new Dictionary<int, EntityProperty>();

        public bool HasProperty(EntityProperty property)
        {
            return _cache.ContainsKey((int) property);
        }

        public List<EntityProperty> GetProperties()
        {
            return new List<EntityProperty>(Properties);
        }

        public void SetGroup(EntityProperty property)
        {
            if (_cache.ContainsKey((int) property))
            {
                return;
            }
            _cache.Add((int)property, property);
            Properties.Add(property);
        }
        
        //public static implicit operator EntityProperty (EntityProperties properties)
        //{
        //    return (EntityProperty) properties.Value;
        //}
    }
}
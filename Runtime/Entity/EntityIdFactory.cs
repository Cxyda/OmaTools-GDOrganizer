using System;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// The EntityIdFactory is responsible to generate unique EntityIds for dynamic game data
    /// </summary>
    public static class EntityIdFactory
    {
        private static uint _entityInstances = 0;

        /// <summary>
        /// This method is responsible for creating unique Entities on runtime
        /// </summary>
        /// <returns></returns>
        public static EntityId Create()
        {
            if (!Application.isPlaying)
            {
                throw new Exception("This method is only supposed to be called on runtime!");
            }
            return new EntityId(++_entityInstances);
        }
#if UNITY_EDITOR
        public static EntityId Pregenerate()
        {
            if (Application.isPlaying)
            {
                throw new Exception("This method is only supposed to be called on edit time!");
            }
            // TODO: generate an unique entityID based on the timestamp which is not colliding with the runtime generation
            return default;
        }
#endif
    }
}
using System.Collections.Generic;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.ExtensionMethods;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// TODO:
    /// </summary>
    [CreateAssetMenu(menuName = "O.M.A.Tools/GD-Organizer/EntityTypeConfig", fileName = "EntityTypeConfig", order = 1)]
    public class EntityTypeConfig : ScriptableObject
    {
#if UNITY_EDITOR
        public List<string> EntityNames = new List<string>();

        public void ValidateNames()
        {
            var copy = new List<string>(EntityNames);
            for (var i = 0; i < copy.Count; i++)
            {
                EntityNames[i] = EntityNames[i].FirstCharToUpper();
            }
        }
#endif
        [ReadOnly, SerializeField]
        public List<EntityTypeDefinition> EntityTypeDefinitions;
    }
}
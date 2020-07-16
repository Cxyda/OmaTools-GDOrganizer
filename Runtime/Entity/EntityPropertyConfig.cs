using System.Collections.Generic;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.ExtensionMethods;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// TODO:
    /// </summary>
    [CreateAssetMenu(menuName = "O.M.A.Tools/GD-Organizer/EntityPropertyConfig", fileName = "EntityPropertyConfig", order = 2)]
    public class EntityPropertyConfig : ScriptableObject
    {
#if UNITY_EDITOR
        public List<string> PropertyNames = new List<string>();

        public void ValidateNames()
        {
            var copy = new List<string>(PropertyNames);
            for (var i = 0; i < copy.Count; i++)
            {
                PropertyNames[i] = PropertyNames[i].FirstCharToUpper();
            }
        }
#endif
        [ReadOnly, SerializeField]
        public List<EntityPropertyDefinition> EntityGroupDefinitions;

    }
}
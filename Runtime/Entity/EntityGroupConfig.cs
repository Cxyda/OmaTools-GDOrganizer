using System.Collections.Generic;
using System.Linq;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.ExtensionMethods;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;
using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// TODO:
    /// </summary>
    [CreateAssetMenu(menuName = "O.M.A.Tools/GD-Organizer/EntityGroupConfig", fileName = "EntityGroupConfig", order = 2)]
    public class EntityGroupConfig : ScriptableObject
    {
#if UNITY_EDITOR
        public List<string> GroupNames = new List<string>();

        public void ValidateNames()
        {
            var copy = new List<string>(GroupNames);
            for (var i = 0; i < copy.Count; i++)
            {
                GroupNames[i] = GroupNames[i].FirstCharToUpper();
            }
        }
#endif
        [ReadOnly, SerializeField]
        public List<EntityGroupDefinition> EntityGroupDefinitions;

    }
}
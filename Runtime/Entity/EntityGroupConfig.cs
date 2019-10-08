using System.Collections.Generic;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// TODO:
    /// </summary>
    [CreateAssetMenu(menuName = "O.M.A.Tools/GD-Organizer/EntityGroupConfig", fileName = "EntityGroupConfig", order = 2)]
    public class EntityGroupConfig : ScriptableObject
    {
        public List<EntityGroupDefinition> GroupNames;

    }
}
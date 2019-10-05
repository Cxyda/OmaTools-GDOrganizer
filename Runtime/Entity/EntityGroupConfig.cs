using System.Collections.Generic;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor
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
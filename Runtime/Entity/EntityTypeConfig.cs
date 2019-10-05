using System.Collections.Generic;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// TODO:
    /// </summary>
    [CreateAssetMenu(menuName = "O.M.A.Tools/GD-Organizer/EntityTypeConfig", fileName = "EntityTypeConfig", order = 1)]
    public class EntityTypeConfig : ScriptableObject
    {
        public List<EntityTypeDefinition> EntityTypes;
    }
}
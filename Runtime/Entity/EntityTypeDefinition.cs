using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// An EntityTypeDefinition links EntityTypes to certain EntityGroups
    /// </summary>
    [CreateAssetMenu (fileName = "NewEntityDefinition", menuName = "O.M.A.Tools/GD-Organizer/Add EntityType", order = 100)]
    public class EntityTypeDefinition : EntityDefinition
    {
        // This is for documentation and clarification only.
        [SerializeField] [TextArea(4, 10)] private string _description;

        [Header("Entity Groups:")]
        public LongEntityGroup EntityGroups;
    }
}
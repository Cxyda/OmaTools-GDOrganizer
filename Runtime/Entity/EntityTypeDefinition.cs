using Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// An EntityTypeDefinition links EntityTypes to certain EntityProperties
    /// </summary>
    [CreateAssetMenu (fileName = "NewEntityDefinition", menuName = "O.M.A.Tools/GD-Organizer/Add EntityType", order = 100)]
    public class EntityTypeDefinition : EntityDefinition
    {
        // This is for documentation and clarification only.
        [SerializeField] private string Description;

        [Header("Entity Groups:")]
        public EntityProperties EntityProperties;

#if UNITY_EDITOR
        public void SetGroup(EntityProperty type)
        {
            EntityProperties.SetGroup(type);
        }
#endif
    }
}
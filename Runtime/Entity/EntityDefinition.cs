using Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// TODO:
    /// </summary>
    public abstract class EntityDefinition : ScriptableObject, IEntityDefinition
    {
        public EntityType EntityType => _entityType;

        [SerializeField, ReadOnly]
        protected EntityType _entityType;

#if UNITY_EDITOR
        public void SetEntityType(EntityType type)
        {
            _entityType = type;
        }
        private void OnValidate()
        {
        }

        [ContextMenu("Validate Asset")]
        public virtual void Validate()
        {

        }
#endif
    }
}
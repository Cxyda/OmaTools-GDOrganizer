using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// An EntityPropertyDefinition links EntityTypes to certain EntityProperties
    /// </summary>
    //[CreateAssetMenu (fileName = "NewEntityPropertyDefinition", menuName = "O.M.A.Tools/GD-Organizer/Add EntityProperty", order = 101)]
    public class EntityPropertyDefinition : EntityDefinition
    {
        
#if UNITY_EDITOR

        private void OnValidate()
        {

        }

        [ContextMenu("Validate Asset")]
        public override void Validate()
        {
            base.Validate();
        }
#endif
    }
}
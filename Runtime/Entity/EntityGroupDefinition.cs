using System;
using Logic;
using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// An EntityTypeDefinition links EntityTypes to certain EntityGroups
    /// </summary>
    [CreateAssetMenu (fileName = "NewEntityGroupDefinition", menuName = "O.M.A.Tools/GD-Organizer/Add EntityGroup", order = 101)]
    public class EntityGroupDefinition : EntityDefinition
    {
        
#if UNITY_EDITOR

        private void OnValidate()
        {
            Validate();
            SaveAsset();
        }

        [ContextMenu("Validate Asset")]
        public override void Validate()
        {
            base.Validate();
        }
#endif
    }
}
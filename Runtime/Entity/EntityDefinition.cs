using System;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Generated;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;
using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// TODO:
    /// </summary>
    public abstract class EntityDefinition : ScriptableObject, IEntityDefinition
    {
        public EntityType EntityType
        {
            get { return _entityType; }
        }
        [SerializeField, ReadOnly]
        protected EntityType _entityType;

#if UNITY_EDITOR
        protected EntityType _lastEntityTypeInternal;

        private void OnValidate()
        {
            Validate();
            SaveAsset();
        }

        protected void SaveAsset()
        {
            string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, name);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
        [ContextMenu("Validate Asset")]
        public virtual void Validate()
        {
            if (_lastEntityTypeInternal != EntityType || !name.Equals(EntityType.ToString()))
            {
                if (!Enum.IsDefined(typeof(EntityType), name))
                {
                    EntityTypeGenerator.AddEnum(name);
                }

                Enum.TryParse(name, out EntityType parsedType);
                _lastEntityTypeInternal = _entityType = parsedType;

                string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
                AssetDatabase.RenameAsset(assetPath, name);
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
            }
        }
#endif
    }
}
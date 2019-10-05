using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor.Configs
{
    /// <summary>
    /// TODO:
    /// </summary>
    [CustomEditor(typeof(EntityTypeConfig))]
    public class EntityTypeConfigEditor : UnityEditor.Editor
    {
        private SerializedProperty _enableAreaBounds;
        private SerializedProperty _cameraLimitationData;

        public override void OnInspectorGUI()
        {
            var myTarget = (EntityTypeConfig)target;
            
            DrawDefaultInspector();
 
            
            if(GUILayout.Button("Regenerate"))
            {
                myTarget.EntityTypes = EntityTypeGenerator.CollectEntityTypeDefinitions();
                EntityTypeGenerator.RegenerateTypeEnums();
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
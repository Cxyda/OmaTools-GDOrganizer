using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor
{
    /// <summary>
    /// TODO:
    /// </summary>
    [CustomEditor(typeof(EntityGroupConfig))]
    public class EntityGroupConfigEditor : UnityEditor.Editor
    {
        private SerializedProperty _enableAreaBounds;
        private SerializedProperty _cameraLimitationData;

        public override void OnInspectorGUI()
        {
            var myTarget = (EntityGroupConfig)target;
            
            DrawDefaultInspector();
 
            
            if(GUILayout.Button("Regenerate"))
            {
                myTarget.GroupNames = EntityGroupGenerator.CollectEntityGroupDefinitions();
                EntityGroupGenerator.RegenerateGroupEnums();
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
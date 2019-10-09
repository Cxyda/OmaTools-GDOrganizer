using Plugins.O.M.A.Games.GDOrganizer.Editor.Window;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
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
            
            if(GUILayout.Button("Regenerate"))
            {
                EditorPrefs.SetBool(GdOrganizerWindow.RegenerationTriggeredKey, true);
                
                GdOrganizerWindow.Regenerate();
            }
            
            DrawDefaultInspector();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
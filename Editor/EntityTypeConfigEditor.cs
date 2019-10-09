using Plugins.O.M.A.Games.GDOrganizer.Editor.Window;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor
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
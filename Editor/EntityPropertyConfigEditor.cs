using Plugins.O.M.A.Games.GDOrganizer.Editor.Window;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor
{
    /// <summary>
    /// TODO:
    /// </summary>
    [CustomEditor(typeof(EntityPropertyConfig))]
    public class EntityPropertyConfigEditor : UnityEditor.Editor
    {
        
        private SerializedProperty _enableAreaBounds;
        private SerializedProperty _cameraLimitationData;

        private ReorderableList _allPropertiesList;
        private SerializedProperty _propertyNames;
        private void OnEnable()
        {
            _propertyNames = serializedObject.FindProperty(nameof(EntityPropertyConfig.PropertyNames));
            _allPropertiesList = new ReorderableList(serializedObject, _propertyNames, true, true, true, true);
            _allPropertiesList.drawElementCallback = DrawListItems;
            _allPropertiesList.drawHeaderCallback = DrawHeader;
        }

        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Entity Properties:", EditorStyles.boldLabel);
        }

        private void DrawListItems(Rect rect, int index, bool isactive, bool isfocused)
        {
            SerializedProperty element = _allPropertiesList.serializedProperty.GetArrayElementAtIndex(index); 
            _allPropertiesList.serializedProperty.GetArrayElementAtIndex(index).stringValue = EditorGUI.TextField(
                new Rect(rect.x, rect.y, rect.width - 50, EditorGUIUtility.singleLineHeight), element.stringValue); 
        }

        public override void OnInspectorGUI()
        {
            if(GUILayout.Button("Regenerate Enums"))
            {
                EditorPrefs.SetBool(GdOrganizerWatcher.RegenerationTriggeredKey, true);
                
                GdOrganizerWatcher.Regenerate();
            }
            EditorGUILayout.Space(10);

            serializedObject.Update();
            _allPropertiesList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
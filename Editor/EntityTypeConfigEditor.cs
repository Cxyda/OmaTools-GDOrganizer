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
    [CustomEditor(typeof(EntityTypeConfig))]
    public class EntityTypeConfigEditor : UnityEditor.Editor
    {
        private SerializedProperty _enableAreaBounds;
        private SerializedProperty _cameraLimitationData;

        private ReorderableList _allEntityTypesList;
        private SerializedProperty _entityTypeNames;

        private void OnEnable()
        {
            _entityTypeNames = serializedObject.FindProperty(nameof(EntityTypeConfig.EntityNames));
            _allEntityTypesList = new ReorderableList(serializedObject, _entityTypeNames, true, true, true, true);
            _allEntityTypesList.drawElementCallback = DrawListItems;
            _allEntityTypesList.drawHeaderCallback = DrawHeader;
        }

        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Entity Types:", EditorStyles.boldLabel);
        }

        private void DrawListItems(Rect rect, int index, bool isactive, bool isfocused)
        {
            SerializedProperty element = _allEntityTypesList.serializedProperty.GetArrayElementAtIndex(index); 
            _allEntityTypesList.serializedProperty.GetArrayElementAtIndex(index).stringValue = EditorGUI.TextField(
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
            _allEntityTypesList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
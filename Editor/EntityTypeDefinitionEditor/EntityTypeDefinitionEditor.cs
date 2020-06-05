using System.IO;
using System.Reflection;
using Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.WSA;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor
{
    /// <summary>
    /// TODO:
    /// </summary>
    [CustomEditor(typeof(EntityTypeDefinition)), CanEditMultipleObjects]
    public class EntityTypeDefinitionEditor : UnityEditor.Editor
    {
        private static string _descriptionPlaceholder = "Enter a description here...";

        private SerializedProperty _description;
        
        private EntityTypeDefinition _myTarget;
        private VisualElement _rootElement;
        private VisualTreeAsset _visualTree;

        private void OnEnable()
        {
            _myTarget= (EntityTypeDefinition)target;
            
            _rootElement = new VisualElement();
            StyleSheet styleSheet = null;
            string[] templateGuids = AssetDatabase.FindAssets("EntityTypeDefinitionTemplate", null);
            string[] styleGuids = AssetDatabase.FindAssets("EntityTypeDefinitionStyle", null);

            if (templateGuids.Length > 0)
            {
                var templatePath = AssetDatabase.GUIDToAssetPath(templateGuids[0]);
                _visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(templatePath);
            }
            else
            {
                Debug.LogError("Unable find template EntityTypeDefinitionTemplate");
            }

            if (styleGuids.Length > 0)
            {
                var stylePath = AssetDatabase.GUIDToAssetPath(styleGuids[0]);
                styleSheet =
                    AssetDatabase.LoadAssetAtPath<StyleSheet>(stylePath);
            }
            else
            {
                Debug.LogError("Unable find styleSheet EntityTypeDefinitionStyle");
            }

            _rootElement.styleSheets.Add(styleSheet);

        }

        public override void OnInspectorGUI()
        {
            
            _rootElement.Clear();
            _visualTree.CloneTree(_rootElement);
            
            serializedObject.Update();


            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(serializedObject.targetObject);
        }

        public override VisualElement CreateInspectorGUI()
        {
            _rootElement.Clear();
            _visualTree.CloneTree(_rootElement);
            
            var enumTypeField = _rootElement.Q<EnumField>( "current-type-field");
            var descriptionField = _rootElement.Q<TextField>( "description-field");
            var propertyField = _rootElement.Q<PropertyField>( "properties-foldout");
            
            enumTypeField.BindProperty(serializedObject.FindProperty("_entityType"));
            enumTypeField.SetEnabled(false);
            
            descriptionField.BindProperty(serializedObject.FindProperty("Description"));
            propertyField.BindProperty(serializedObject.FindProperty("EntityProperties"));
            
            return _rootElement;
        }

    }
}
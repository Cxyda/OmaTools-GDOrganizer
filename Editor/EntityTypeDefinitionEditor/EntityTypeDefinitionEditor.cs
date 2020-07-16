using Plugins.O.M.A.Games.GDOrganizer.Editor.Utils;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

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

            _visualTree = AssetDatabaseHelper.LoadFirstAssetByFilter<VisualTreeAsset>("EntityTypeDefinitionTemplate", null);
            var styleSheet = AssetDatabaseHelper.LoadFirstAssetByFilter<StyleSheet>("EntityTypeDefinitionStyle", null);

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
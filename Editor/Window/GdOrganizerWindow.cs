using Plugins.O.M.A.Games.GDOrganizer.Editor.Generators;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Utils;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.GdOrganizer;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor.Window
{
    public class GdOrganizerWindow : EditorWindow
    {
        public const string RegenerationTriggeredKey = "OMAGames.GDOrganizer.RegenerationTriggered";

        [MenuItem("O.M.A.Tools/GD-Organizer/Settings", priority = 100)]
        static void SelectSettingsFile()
        {
            var settingsSO = ScriptableObjectEditorUtils.FindFirstOfType<GdOrganizerSettings>();
            Selection.activeObject = settingsSO;
            EditorGUIUtility.PingObject(Selection.activeObject);
        }
        
        // Add menu named "My Window" to the Window menu
        [MenuItem("O.M.A.Tools/GD-Organizer/Gd-Organizer Ui", priority = 1)]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            GdOrganizerWindow window = (GdOrganizerWindow)EditorWindow.GetWindow(typeof(GdOrganizerWindow), false, "GD-Organizer", true);
            window.Show();
        }

        void OnGUI()
        {
            if (GUILayout.Button("Generate Group Definitions"))
            {
                GdOrganizerEditorUtils.GenerateAllGroupDefinitions();
                GdOrganizerEditorUtils.GenerateGroupDefinitionsForAllTypes();
            }
            /*
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            myString = EditorGUILayout.TextField("Text Field", myString);

            groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
            myBool = EditorGUILayout.Toggle("Toggle", myBool);
            myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
            EditorGUILayout.EndToggleGroup();
            */
        }
        
        [DidReloadScripts]
        public static void OnCompileScripts()
        {
            if (!EditorPrefs.GetBool(RegenerationTriggeredKey))
            {
                return;
            }
            var entityGroupConfig = ScriptableObjectEditorUtils.FindFirstOfType<EntityGroupConfig>();
            var entityTypeConfig = ScriptableObjectEditorUtils.FindFirstOfType<EntityTypeConfig>();
            
            EntityTypeGenerator.CleanupUnusedDefinitions();
            EntityGroupGenerator.CleanupUnusedDefinitions();
                
            EntityTypeGenerator.GenerateDefinitions();
            EntityGroupGenerator.GenerateDefinitions();
            
            entityTypeConfig.EntityTypeDefinitions = ScriptableObjectEditorUtils.FindAllOfType<EntityTypeDefinition>();
            entityGroupConfig.EntityGroupDefinitions = ScriptableObjectEditorUtils.FindAllOfType<EntityGroupDefinition>();
            
            EditorPrefs.SetBool(RegenerationTriggeredKey, false);
        }
        
        public static void Regenerate()
        {
            var entityGroupConfig = ScriptableObjectEditorUtils.FindFirstOfType<EntityGroupConfig>();
            var entityTypeConfig = ScriptableObjectEditorUtils.FindFirstOfType<EntityTypeConfig>();
            
            entityGroupConfig.ValidateNames();
            entityTypeConfig.ValidateNames();

            EntityGroupGenerator.GenerateEnums(entityGroupConfig.GroupNames);
            EntityTypeGenerator.GenerateEnums(entityTypeConfig.EntityNames);
        }
    }
}


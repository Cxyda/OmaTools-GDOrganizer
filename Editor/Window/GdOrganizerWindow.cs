using Plugins.O.M.A.Games.GDOrganizer.Editor.Utils;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.GdOrganizer;
using UnityEngine;
using UnityEditor;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor.Window
{
    public class GdOrganizerWindow : EditorWindow
    {

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
            GdOrganizerEditorUtils.LoadSettingsFile();
            GdOrganizerEditorUtils.CreateConfigFiles();
            
            // Get existing open window or if none, make a new one:
            GdOrganizerWindow window = (GdOrganizerWindow)EditorWindow.GetWindow(typeof(GdOrganizerWindow), false, "GD-Organizer", true);
            window.Show();
        }

        void OnGUI()
        {
            if (GUILayout.Button(new GUIContent("Generate Enums", "This generates the Enum files from the values stored in the Config files.")))
            {
                EditorPrefs.SetBool(GdOrganizerWatcher.RegenerationTriggeredKey, true);
                GdOrganizerWatcher.Regenerate();
            }
            EditorGUILayout.Space(5);

            if (GUILayout.Button(new GUIContent("Generate Property Definitions", "This regenerates all property definition files.")))
            {
                GdOrganizerEditorUtils.GenerateAllGroupDefinitions();
                GdOrganizerEditorUtils.GeneratePropertyDefinitionsForAllTypes();
            }
            EditorGUILayout.Space(5);

            if (GUILayout.Button(new GUIContent("Generate All", "This runs the full pipeline")))
            {
                EditorPrefs.SetBool(GdOrganizerWatcher.RegenerationTriggeredKey, true);
                GdOrganizerWatcher.Regenerate();
                GdOrganizerEditorUtils.GenerateAllGroupDefinitions();
                GdOrganizerEditorUtils.GeneratePropertyDefinitionsForAllTypes();
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
        

    }
}


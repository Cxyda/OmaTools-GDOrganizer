using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.GdOrganizer
{
    /// <summary>
    /// TODO:
    /// </summary>
    [CreateAssetMenu(menuName = "O.M.A.Tools/GD-Organizer/Settings", fileName = "GD_Organizer_Settings", order = 100)]
    public class GdOrganizerSettings : ScriptableObject
    {
        [Header("Generated code locations:")]
        public string DefinitionTemplatePath = "Assets/Code/DefinitionTemplates";

        [Header("Generated data locations:")]
        public string DefinitionsRootPath = "Assets/GameDesign/EntityDefinitions";
        public string GeneratedScriptsRootPath = "Assets/GameDesign/Generated";
        public string ConfigRootPath = "Assets/GameDesign/Configs";
        
        [Header("Settings location:")]
        public string SettingsPath = "Assets/Plugins/O.M.A.Games/GDOrganizer/Settings";
    }
}
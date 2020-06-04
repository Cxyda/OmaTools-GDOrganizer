using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.GdOrganizer
{
    /// <summary>
    /// TODO:
    /// </summary>
    [CreateAssetMenu(menuName = "O.M.A.Tools/GD-Organizer/Settings", fileName = "GD_Organizer_Settings", order = 100)]
    public class GdOrganizerSettings : ScriptableObject
    {
        public string DefinitionTemplatePath = "Assets/GameDesign/DefinitionTemplates";

        public string DefinitionsRootPath = "Assets/GameDesign/EntityDefinitions";
        public string GeneratedScriptsRootPath = "Assets/Plugins/O.M.A.Games/GDOrganizer/Generated";
        public string ConfigRootPath = "Assets/GameDesign/Configs";
        
        public string SettingsPath = "Assets/Plugins/O.M.A.Games/GDOrganizer/Settings";
    }
}
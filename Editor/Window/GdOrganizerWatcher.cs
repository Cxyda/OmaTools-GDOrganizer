using System.Collections.Generic;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Generators;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Utils;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor.Window
{
    public static class GdOrganizerWatcher 
    {
        public const string RegenerationTriggeredKey = "OMAGames.GDOrganizer.RegenerationTriggered";

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
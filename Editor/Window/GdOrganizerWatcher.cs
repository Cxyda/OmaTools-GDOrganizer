using System;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Generators;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Utils;
using Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor.Window
{
    /// <summary>
    /// This script triggers the code generation when Unity finished script compiling
    /// </summary>
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
            var entityGroupConfig = ScriptableObjectEditorUtils.FindFirstOfType<EntityPropertyConfig>();
            var entityTypeConfig = ScriptableObjectEditorUtils.FindFirstOfType<EntityTypeConfig>();
            
            EntityTypeGenerator.CleanupUnusedDefinitions();
            EntityPropertyGenerator.CleanupUnusedDefinitions();
                
            EntityTypeGenerator.GenerateDefinitions();
            EntityPropertyGenerator.GenerateDefinitions();
            
            entityTypeConfig.EntityTypeDefinitions = ScriptableObjectEditorUtils.FindAllOfType<EntityTypeDefinition>();
            entityGroupConfig.EntityGroupDefinitions = ScriptableObjectEditorUtils.FindAllOfType<EntityPropertyDefinition>();
            
            EditorPrefs.SetBool(RegenerationTriggeredKey, false);
        }
        
        public static void Regenerate()
        {
            var entityPropertyConfig = ScriptableObjectEditorUtils.FindFirstOfType<EntityPropertyConfig>();
            var entityTypeConfig = ScriptableObjectEditorUtils.FindFirstOfType<EntityTypeConfig>();
            
            entityPropertyConfig.ValidateNames();
            entityTypeConfig.ValidateNames();

            EntityPropertyGenerator.GenerateEnums(entityPropertyConfig.PropertyNames);
            EntityTypeGenerator.GenerateEnums(entityTypeConfig.EntityNames);
        }
    }
}
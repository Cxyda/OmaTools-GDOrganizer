using System.Collections.Generic;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Generators;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Utils;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor.Window
{
    [InitializeOnLoad]
    public class Startup : ISerializationCallbackReceiver
    {
        static Startup()
        {
            Debug.Log("Startup");

            var groupEnum = GeneratorUtils.TryGetTypeFromName($"{EntityDefinitionGenerator.DefinitionNameSpace}.EntityGroup");
            if (groupEnum == null)
            {
                EntityGroupGenerator.GenerateEnums(new List<string>());
            }
            
            
            var typeEnum = GeneratorUtils.TryGetTypeFromName($"{EntityDefinitionGenerator.DefinitionNameSpace}.EntityType");
            if (typeEnum == null)
            {
                EntityTypeGenerator.GenerateEnums(new List<string>());
            }
        }

        public void OnBeforeSerialize()
        {
            Debug.Log("OnBeforeSerialize");
        }

        public void OnAfterDeserialize()
        {
            Debug.Log("OnAfterDeserialize");

        }
    }
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
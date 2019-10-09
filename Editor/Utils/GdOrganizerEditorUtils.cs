using System;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Utils;
using Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.ExtensionMethods;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.GdOrganizer;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;
using UnityEngine;
#if UNITY_EDITOR
using System.IO;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Generators;
using UnityEditor;
using UnityEngine.UI;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor.Window
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static class GdOrganizerEditorUtils
    {
        private static GdOrganizerSettings _settings;

        public static void GenerateAllGroupDefinitions()
        {
            LoadSettingsFile();
            
            var allGroups = Enum.GetValues(typeof(EntityGroup));
            foreach (var group in allGroups)
            {
                EntityDefinitionGenerator.GenerateGroupDefinitionFile((EntityGroup) group);
            }
        }

        public static void GenerateGroupDefinitionsForAllTypes()
        {
            var entityTypeDefinitions = ScriptableObjectEditorUtils.FindAllOfType<EntityTypeDefinition>();

            foreach (var typeDefinition in entityTypeDefinitions)
            {
                GenerateGroupDefinitionsForType(typeDefinition);
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void GenerateGroupDefinitionsForType(EntityTypeDefinition entityTypeDefinition)
        {
            LoadSettingsFile();

            if (entityTypeDefinition == null)
            {
                return;
            }
            if (_settings == null)
            {
                return;
            }
            var flags = ((EntityGroup) entityTypeDefinition.EntityGroups).GetFlags();

            foreach (var flag in flags)
            {
                var flagName = flag.ToString();
                var entityTypeName = entityTypeDefinition.EntityType.ToString();
                if (flagName.Equals(entityTypeName))
                {
                    break;
                }

                var path = Path.Combine(_settings.DefinitionsRootPath, $"{flagName}{EntityDefinitionGenerator.NameSuffix}s");

                var typeName = $"{EntityDefinitionGenerator.DefinitionNameSpace}.{flagName}{EntityDefinitionGenerator.NameSuffix}";
                var type = GeneratorUtils.GetTypeFromName(typeName);

                var entityGroupDefinition = ScriptableObject.CreateInstance(type);
                
                var definition = entityGroupDefinition as IEntityDefinition;
                definition?.SetEntityType(entityTypeDefinition.EntityType);
                
                if (entityGroupDefinition == null)
                {
                    return;
                }
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                AssetDatabase.CreateAsset(entityGroupDefinition,  Path.Combine(path, $"{entityTypeName}.asset"));
            }
        }

        private static void LoadSettingsFile()
        {
            if (_settings == null)
            {
                _settings = ScriptableObjectEditorUtils.FindFirstOfType<GdOrganizerSettings>();
            }
        }
    }
}

#endif
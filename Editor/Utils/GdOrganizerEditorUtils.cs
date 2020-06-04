

using Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.ExtensionMethods;
#if UNITY_EDITOR
using System;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Utils;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.GdOrganizer;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;
using UnityEngine;
using System.IO;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Generators;
using UnityEditor;

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
            
            var allGroups = Enum.GetValues(typeof(EntityProperty));
            foreach (var group in allGroups)
            {
                EntityDefinitionGenerator.GeneratePropertyDefinitionFile((EntityProperty) group);
            }
        }

        public static void GeneratePropertyDefinitionsForAllTypes()
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

            var properties = entityTypeDefinition.EntityProperties.GetProperties();

            foreach (EntityProperty property in properties)
            {
                var propertyName = property.ToString();
                var entityTypeName = entityTypeDefinition.EntityType.ToString();
                if (propertyName.Equals(entityTypeName))
                {
                    break;
                }

                var path = Path.Combine(_settings.DefinitionsRootPath, $"{propertyName}{EntityDefinitionGenerator.NameSuffix}s");

                var typeName = $"{EntityDefinitionGenerator.DefinitionNameSpace}.{propertyName}{EntityDefinitionGenerator.NameSuffix}";
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
        public static void LoadSettingsFile()
        {
            GetSettingsFile();
        }
        public static GdOrganizerSettings GetSettingsFile()
        {
            if (_settings == null)
            {
                _settings = ScriptableObjectEditorUtils.FindFirstOfType<GdOrganizerSettings>();
                if (_settings == null)
                {
                    _settings = CreateSettingsFile();
                }
            }

            return _settings;
        }

        public static GdOrganizerSettings CreateSettingsFile()
        {
            _settings = ScriptableObject.CreateInstance<GdOrganizerSettings>();
            if (!Directory.Exists(_settings.SettingsPath))
            {
                Directory.CreateDirectory(_settings.SettingsPath);
            }
            AssetDatabase.CreateAsset(_settings, $"{Path.Combine(_settings.SettingsPath, "GdOrganizerSettings")}.asset");
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
            return _settings;
        }
        
        public static void CreateConfigFiles()
        {
            var typeConfig = ScriptableObjectEditorUtils.FindFirstOfType<EntityTypeConfig>();
            var groupConfig = ScriptableObjectEditorUtils.FindFirstOfType<EntityPropertyConfig>();

            if (typeConfig == null || groupConfig == null)
            {
                if (!Directory.Exists(_settings.ConfigRootPath))
                {
                    Directory.CreateDirectory(_settings.ConfigRootPath);
                }

                if (typeConfig == null)
                {
                    typeConfig = ScriptableObject.CreateInstance<EntityTypeConfig>();
                    AssetDatabase.CreateAsset(typeConfig, $"{Path.Combine(_settings.ConfigRootPath, "EntityTypeConfig")}.asset");

                }
                if (groupConfig == null)
                {
                    groupConfig = ScriptableObject.CreateInstance<EntityPropertyConfig>();
                    AssetDatabase.CreateAsset(groupConfig, $"{Path.Combine(_settings.ConfigRootPath, "EntityGroupConfig")}.asset");
                }
            }

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }
    }
}

#endif
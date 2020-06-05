using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Utils;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Window;
using Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.ExtensionMethods;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.GdOrganizer;
using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor.Generators
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static class EntityPropertyGenerator
    {
        private const string GeneratedFileName = "EntityProperty.cs";
        private static string _generatedFileContent = "";

        private static DateTime _lastTimeGenerated;
        private static List<string> _enumNames = new List<string>();

        private static DateTime _now;

        public static void GenerateEnums(List<string> definitionNames, bool refresh = true)
        {
            var addedDefinitions = new List<string>();
            var entityTypeConfig = ScriptableObjectEditorUtils.FindFirstOfType<EntityTypeConfig>();
            
            _enumNames = new List<string>();
            foreach (var definitionName in definitionNames)
            {
                if (addedDefinitions.Contains(definitionName))
                {
                    continue;
                }
                _enumNames.Add(definitionName);
                if (!entityTypeConfig.EntityNames.Contains(definitionName))
                {
                    entityTypeConfig.EntityNames.Add(definitionName);
                    EntityTypeGenerator.AddEnum(definitionName, false);
                }
                addedDefinitions.Add(definitionName);
            }
            GenerateFile();
            if (refresh)
            {
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            }
        }

        public static void GenerateDefinitions()
        {
            var addedDefinitions = new List<string>();
            var existingGroupDefinitions = ScriptableObjectEditorUtils.FindAllOfType<EntityPropertyDefinition>();

            var settings = ScriptableObjectEditorUtils.FindFirstOfType<GdOrganizerSettings>();
            
            var propertyDefinitionPath = Path.Combine(settings.DefinitionsRootPath, "EntityPropertyDefinitions");
            
            var definitionNames = Enum.GetNames(typeof(EntityProperty));
            
            foreach (var definitionName in definitionNames)
            {
                if (addedDefinitions.Contains(definitionName))
                {
                    continue;
                }
 
                if (!existingGroupDefinitions.Exists(x => x.EntityType.ToString().Equals(definitionName)))
                {
                    // create SO
                    var definitionInstance = ScriptableObject.CreateInstance<EntityPropertyDefinition>();
                    var groupType = Enum.Parse(typeof(EntityType), definitionName);
                    
                    definitionInstance.SetEntityType((EntityType) groupType);
                    if (!Directory.Exists(propertyDefinitionPath))
                    {
                        Directory.CreateDirectory(propertyDefinitionPath);
                    }

                    string fullPath = $"{Path.Combine(propertyDefinitionPath, definitionName)}.asset";
                    if (File.Exists(fullPath))
                    {
                        return;
                    }
                    AssetDatabase.CreateAsset(definitionInstance, fullPath);
                }
                addedDefinitions.Add(definitionName);
            }

            SetupGroupDefinitions();
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void CleanupUnusedDefinitions()
        {
            var definitionNames = Enum.GetNames(typeof(EntityProperty));
            var existingGroupDefinitions = ScriptableObjectEditorUtils.FindAllOfType<EntityPropertyDefinition>();

            if (definitionNames.Length != existingGroupDefinitions.Count)
            {
                foreach (var definition in existingGroupDefinitions)
                {
                    if (!definitionNames.Contains(definition.EntityType.ToString()))
                    {
                        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(definition));
                    }
                }
            }
            AssetDatabase.Refresh();
        }

        public static void SetupGroupDefinitions()
        {
            var existingGroupDefinitions = ScriptableObjectEditorUtils.FindAllOfType<EntityTypeDefinition>();

            foreach (var groupDefinition in existingGroupDefinitions)
            {
                if (groupDefinition.EntityType.TryToGroup(out EntityProperty entityProperty))
                {
                    groupDefinition.SetGroup(entityProperty);
                }
            }
        }
        public static void GenerateFile()
        {
            _generatedFileContent = "";
            _now = DateTime.Now;

            var path = "";
            try
            {
                path = GdOrganizerEditorUtils.GetSettingsFile().GeneratedScriptsRootPath;
            }
            catch
            {
                // ignored
            }

            Writeheader();
            WriteDisclaimer();
            WriteBody();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fullPath = Path.Combine(path, GeneratedFileName);

            File.WriteAllText(fullPath, _generatedFileContent);
            _lastTimeGenerated = _now;
            
            AssetDatabase.SaveAssets();
        }

        public static void RemoveEnum(string groupName, bool refresh = true)
        {
            _enumNames = Enum.GetNames(typeof(EntityProperty)).ToList();
            _enumNames.Remove(groupName);
            EntityTypeGenerator.RemoveEnum(groupName, refresh);
            GenerateFile();
            if (refresh)
            {
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            }
        }
        private static string GetCurrentPath()
        {
            var scriptGuids = AssetDatabase.FindAssets($"{typeof(EntityPropertyGenerator).Name} t:monoscript");
            if (scriptGuids.Length == 0)
            {
                throw new Exception("Can't find script.");
            }

            if (scriptGuids.Length > 1)
            {
                throw new Exception("Too many scripts found.");
            }

            return AssetDatabase.GUIDToAssetPath(scriptGuids[0]);
        }

        private static EntityPropertyConfig GetEntityGroupConfig()
        {
            var assetGuids = AssetDatabase.FindAssets($"t: {typeof(EntityPropertyConfig).Name}");
            if (assetGuids.Length == 0)
            {
                throw new Exception("Can't find EntityGroupConfig. Please create one... > Create > GD-Organizer> EntityGroupConfig");
            }

            return AssetDatabase.LoadAssetAtPath<EntityPropertyConfig>(AssetDatabase.GUIDToAssetPath(assetGuids[0]));

        }
        private static void WriteBody()
        {
            int indentation = 1;
            
            AppendContent(GetLine("public enum EntityProperty", indentation));
            AppendContent(GetLine("{", indentation));
            indentation++;
            int groupCount = 0;

            //AppendContent(GetLine($"None = 0L,", indentation));
            foreach (var group in _enumNames)
            {
                if (group == null)
                {
                    continue;
                }

                Enum.TryParse(group, out EntityType typedGroup);
                AppendContent(GetLine($"{@group} = {(long)typedGroup},", indentation));
                groupCount++;
            }
            indentation--;
            AppendContent(GetLine("}", indentation));
            indentation--;
            AppendContent(GetLine("}", indentation));
        }

        private static void AppendContent(string content)
        {
            _generatedFileContent += content;
        }
        private static string GetLine(string content, int indentationLevel = 0)
        {
            var indentation = "";
            for (var i = 0; i < indentationLevel; i++)
            {
                indentation += "\t";
            }
            return indentation + content.Trim() + "\n";
        }
        private static void Writeheader()
        {
            AppendContent(GetLine(""));
            AppendContent(GetLine("namespace Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition"));
            AppendContent(GetLine("{"));
        }
        private static void WriteDisclaimer()
        {
            AppendContent(GetLine("/// <summary>", 1));
            AppendContent(GetLine("/// ////////////////////////////////////////////////////////////////////////////////////", 1));
            AppendContent(GetLine("/// ///////////                                                              ///////////", 1));
            AppendContent(GetLine("/// ///////////       THIS IS AN AUTOGENERATED CLASS, DO NOT MODIFY!         ///////////", 1));
            AppendContent(GetLine("/// ///////////                                                              ///////////", 1));
            AppendContent(GetLine("/// ////////////////////////////////////////////////////////////////////////////////////", 1));
            AppendContent(GetLine($"/// Last time generated: {_now}", 1));
            AppendContent(GetLine($"/// This script has been generated by '{typeof(EntityPropertyGenerator).Name}'", 1));
            AppendContent(GetLine("/// </summary>", 1));
        }
    }
}
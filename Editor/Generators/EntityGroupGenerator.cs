using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Utils;
using Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.ExtensionMethods;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.GdOrganizer;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;
using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor.Generators
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static class EntityGroupGenerator
    {
        private const string GeneratedFileName = "../Runtime/Generated/EntityGroup.cs";

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
            var existingGroupDefinitions = ScriptableObjectEditorUtils.FindAllOfType<EntityGroupDefinition>();

            var settings = ScriptableObjectEditorUtils.FindFirstOfType<GdOrganizerSettings>();
            
            var groupDefinitionPath = Path.Combine(settings.DefinitionsRootPath, "EntityGroupDefinitions");
            
            var definitionNames = Enum.GetNames(typeof(EntityGroup));
            
            foreach (var definitionName in definitionNames)
            {
                if (addedDefinitions.Contains(definitionName))
                {
                    continue;
                }
 
                if (!existingGroupDefinitions.Exists(x => x.EntityType.ToString().Equals(definitionName)))
                {
                    // create SO
                    var definitionInstance = ScriptableObject.CreateInstance<EntityGroupDefinition>();
                    var groupType = Enum.Parse(typeof(EntityType), definitionName);
                    
                    definitionInstance.SetEntityType((EntityType) groupType);
                    if (!Directory.Exists(groupDefinitionPath))
                    {
                        Directory.CreateDirectory(groupDefinitionPath);
                    }
                    AssetDatabase.CreateAsset(definitionInstance, $"{Path.Combine(groupDefinitionPath, definitionName)}.asset");
                }
                addedDefinitions.Add(definitionName);
            }

            SetupGroupDefinitions();
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void CleanupUnusedDefinitions()
        {
            var definitionNames = Enum.GetNames(typeof(EntityGroup));
            var existingGroupDefinitions = ScriptableObjectEditorUtils.FindAllOfType<EntityGroupDefinition>();

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
                var groupType = groupDefinition.EntityType.ToGroup();
                groupDefinition.SetGroup(groupType);
            }
        }
        public static void GenerateFile()
        {
            _generatedFileContent = "";
            _now = DateTime.Now;

            var path = "";
            try
            {
                var modulePath = Directory.GetParent(GetCurrentPath()).Parent.FullName;
                path = Path.Combine(modulePath, GeneratedFileName);

            }
            catch
            {
                // ignored
            }

            Writeheader();
            WriteDisclaimer();
            WriteBody();
            File.WriteAllText(path, _generatedFileContent);
            _lastTimeGenerated = _now;
            
            AssetDatabase.SaveAssets();
        }

        public static void RemoveEnum(string groupName, bool refresh = true)
        {
            _enumNames = Enum.GetNames(typeof(EntityGroup)).ToList();
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
            var scriptGuids = AssetDatabase.FindAssets($"{typeof(EntityGroupGenerator).Name} t:monoscript");
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

        private static EntityGroupConfig GetEntityGroupConfig()
        {
            var assetGuids = AssetDatabase.FindAssets($"t: {typeof(EntityGroupConfig).Name}");
            if (assetGuids.Length == 0)
            {
                throw new Exception("Can't find EntityGroupConfig. Please create one... > Create > GD-Organizer> EntityGroupConfig");
            }

            return AssetDatabase.LoadAssetAtPath<EntityGroupConfig>(AssetDatabase.GUIDToAssetPath(assetGuids[0]));

        }
        private static void WriteBody()
        {
            int indentation = 1;
            
            AppendContent(GetLine("[System.Flags]", indentation));

            AppendContent(GetLine("public enum EntityGroup : long", indentation));
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

                AppendContent(GetLine($"{@group} = 1L << {groupCount},", indentation));
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
            AppendContent(GetLine($"///This script has been generated by '{typeof(EntityGroupGenerator).Name}'", 1));
            AppendContent(GetLine("/// </summary>", 1));
        }
    }
}
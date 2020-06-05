using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Utils;
using Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.GdOrganizer;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using System.IO;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Window;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor.Generators
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static class EntityTypeGenerator
    {
        private const string GeneratedFileName = "EntityType.cs";

        private static string _generatedFileContent = "";

        private static DateTime _now;
        private static List<string> _enumNames = new List<string>();
        
        public static void GenerateDefinitions()
        {
            var addedDefinitions = new List<string>();
            var existingTypeDefinitions = ScriptableObjectEditorUtils.FindAllOfType<EntityTypeDefinition>();

            var settings = ScriptableObjectEditorUtils.FindFirstOfType<GdOrganizerSettings>();
            
            var typeDefinitionPath = Path.Combine(settings.DefinitionsRootPath, "EntityTypeDefinitions");
            
            var definitionNames = Enum.GetNames(typeof(EntityType));
            
            foreach (var definitionName in definitionNames)
            {
                if (addedDefinitions.Contains(definitionName))
                {
                    continue;
                }
                
                if (!existingTypeDefinitions.Exists(x => x.EntityType.ToString().Equals(definitionName)))
                {
                    // create SO
                    var definitionInstance = ScriptableObject.CreateInstance<EntityTypeDefinition>();
                    var groupType = Enum.Parse(typeof(EntityType), definitionName);
                    
                    definitionInstance.SetEntityType((EntityType) groupType);
                    if (!Directory.Exists(typeDefinitionPath))
                    {
                        Directory.CreateDirectory(typeDefinitionPath);
                    }
                    string fullPath = $"{Path.Combine(typeDefinitionPath, definitionName)}.asset";
                    if (File.Exists(fullPath))
                    {
                        return;
                    }
                    
                    AssetDatabase.CreateAsset(definitionInstance, fullPath);
                }

                addedDefinitions.Add(definitionName);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        public static void CleanupUnusedDefinitions()
        {
            var definitionNames = Enum.GetNames(typeof(EntityType));
            var existingTypeDefinitions = ScriptableObjectEditorUtils.FindAllOfType<EntityTypeDefinition>();

            if (definitionNames.Length != existingTypeDefinitions.Count)
            {
                foreach (var definition in existingTypeDefinitions)
                {
                    if (!definitionNames.Contains(definition.EntityType.ToString()))
                    {
                        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(definition));
                    }
                }
            }
            AssetDatabase.Refresh();
        }
        
        public static void GenerateEnums(List<string> definitionNames, bool refresh = true)
        {
            var addedDefinitions = new List<string>();
            _enumNames = new List<string>();
            
            foreach (var definitionName in definitionNames)
            {
                if (addedDefinitions.Contains(definitionName))
                {
                    continue;
                }
                _enumNames.Add(definitionName);
                addedDefinitions.Add(definitionName);
            }

            GenerateFile();
            if (refresh)
            {
                AssetDatabase.Refresh();
            }
        }
        public static void AddEnum(string enumName, bool refresh = true)
        {
            if (string.IsNullOrEmpty(enumName))
            {
                return;
            }
            var enumValues = Enum.GetNames(typeof(EntityType)).ToList();
            if (enumValues.Contains(enumName) || _enumNames.Contains(enumName)) return;
            
            _enumNames.Add(enumName);
                
            GenerateFile();
            if (refresh)
            {
                AssetDatabase.Refresh();
            }
        }
        public static void RemoveEnum(string groupName, bool refresh = true)
        {
            _enumNames = Enum.GetNames(typeof(EntityType)).ToList();
            _enumNames.Remove(groupName);

            GenerateFile();
            if (refresh)
            {
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            }
        }
        private static void GenerateFile()
        {
            _generatedFileContent = "";
            _now = DateTime.Now;
            var path = "";
            try
            {
                path = Path.Combine(GdOrganizerEditorUtils.GetSettingsFile().GeneratedScriptsRootPath, GeneratedFileName);
            }
            catch
            {
                // ignored
            }

            Writeheader();
            WriteDisclaimer();
            WriteBody();
            if (!Directory.Exists(GdOrganizerEditorUtils.GetSettingsFile().GeneratedScriptsRootPath))
            {
                Directory.CreateDirectory(GdOrganizerEditorUtils.GetSettingsFile().GeneratedScriptsRootPath);
            }
            File.WriteAllText(path, _generatedFileContent);

            AssetDatabase.SaveAssets();
        }

        private static string GetCurrentPath()
        {
            var scriptGuids = AssetDatabase.FindAssets($"{typeof(EntityTypeGenerator).Name} t:monoscript");
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

        private static EntityTypeConfig GetEntityTypesConfig()
        {
            var assetGuids = AssetDatabase.FindAssets($"t: {typeof(EntityTypeConfig).Name}");
            if (assetGuids.Length == 0)
            {
                throw new Exception("Can't find EntityTypeConfig. Please create one... > Create > GD-Organizer> EntityTypeConfig");
            }

            return AssetDatabase.LoadAssetAtPath<EntityTypeConfig>(AssetDatabase.GUIDToAssetPath(assetGuids[0]));

        }

        private static void WriteBody()
        {
            int indentation = 1;
            var noneHash = "None".GetHashCode();

            AppendContent(GetLine("public enum EntityType", indentation));
            AppendContent(GetLine("{", indentation));
            indentation++;
            
            AppendContent(GetLine($"Invalid = 0,", indentation));
            AppendContent(GetLine($"None = {noneHash},", indentation));
            
            foreach (var type in _enumNames)
            {
                if (type.Equals("None") || type.Equals("Invalid"))
                {
                    continue;
                }

                AppendContent(GetLine($"{type} = {type.GetHashCode()},", indentation));

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
            AppendContent(GetLine($"/// This script has been generated by '{typeof(EntityTypeGenerator).Name}'", 1));
            AppendContent(GetLine("/// </summary>", 1));
        }
#endif
    }
}
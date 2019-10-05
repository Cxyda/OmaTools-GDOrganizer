using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Logic;
using Plugins.O.M.A.Games.GDOrganizer.Editor;
using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static class EntityTypeGenerator
    {
#if UNITY_EDITOR
        private const string GeneratedFileName = "Generated/EntityType.cs";

        private static string _generatedFileContent = "";

        private static DateTime _lastTimeGenerated;

        private static DateTime _now;

        public static List<EntityTypeDefinition> CollectEntityTypeDefinitions()
        {
            var groupDefinitionGuids = AssetDatabase.FindAssets($"t: {typeof(EntityTypeDefinition)}");
            var groupDefinitions = new List<EntityTypeDefinition>();
            foreach (var guid in groupDefinitionGuids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var groupDefinition = AssetDatabase.LoadAssetAtPath<EntityTypeDefinition>(path);
                if (groupDefinition == null)
                {
                    continue;
                }

                groupDefinitions.Add(groupDefinition);
            }

            return groupDefinitions;
        }

        public static void AddEnum(string enumName)
        {
            var enumValues = Enum.GetNames(typeof(EntityType)).ToList();
            if (!enumValues.Contains(enumName))
            {
                enumValues.Add(enumName);
                
                _generatedFileContent = "";
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
                WriteBody(enumValues);
                File.WriteAllText(path, _generatedFileContent);
                _lastTimeGenerated = _now;
            
                AssetDatabase.Refresh();
            }
        }
        public static void RegenerateTypeEnums()
        {
            _generatedFileContent = "";
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
            WriteBody(GetEntityTypesConfig().EntityTypes);
            File.WriteAllText(path, _generatedFileContent);
            _lastTimeGenerated = _now;
            
            AssetDatabase.SaveAssets();
        }
        //[UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            _now= DateTime.Now;
            if (_lastTimeGenerated != DateTime.MinValue && (_now - _lastTimeGenerated).Minutes <= 5)
            {
                return;
            }
            RegenerateTypeEnums();
            Debug.Log($":: -- {GeneratedFileName} has been generated");
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
        private static void WriteBody(List<EntityTypeDefinition> allEntitytypes)
        {
            int indentation = 1;
            
            AppendContent(GetLine("[System.Flags]", indentation));

            AppendContent(GetLine("public enum EntityType", indentation));
            AppendContent(GetLine("{", indentation));
            indentation++;
            
            AppendContent(GetLine($"Invalid = 0,", indentation));
            AppendContent(GetLine($"None = 1,", indentation));
            
            foreach (var type in allEntitytypes)
            {
                if (type == null)
                {
                    continue;
                }
                if (type.EntityType.ToString().Equals("None") || type.EntityType.ToString().Equals("Invalid"))
                {
                    continue;
                }
                AppendContent(GetLine($"{type.EntityType.ToString()} = {type.EntityType.ToString().GetHashCode()},", indentation));

            }
            indentation--;
            AppendContent(GetLine("}", indentation));
            indentation--;
            AppendContent(GetLine("}", indentation));

        }
        private static void WriteBody(List<string> allEntitytypes)
        {
            int indentation = 1;
            
            AppendContent(GetLine("[System.Flags]", indentation));

            AppendContent(GetLine("public enum EntityType", indentation));
            AppendContent(GetLine("{", indentation));
            indentation++;
            
            AppendContent(GetLine($"Invalid = 0,", indentation));
            AppendContent(GetLine($"None = 1,", indentation));
            
            foreach (var type in allEntitytypes)
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
            AppendContent(GetLine("namespace Logic"));
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
            AppendContent(GetLine("/// Last time generated: {DateTime.Now}", 1));
            AppendContent(GetLine($"///This script has been generated from '{typeof(EntityGroupGenerator).Name}'", 1));
            AppendContent(GetLine("/// </summary>", 1));
        }
#endif
    }
}
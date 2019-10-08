using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Generated;
using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static class EntityGroupGenerator
    {
        private const string GeneratedFileName = "Generated/EntityGroup.cs";

        private static string _generatedFileContent = "";

        private static DateTime _lastTimeGenerated;

        private static DateTime _now;

        public static List<EntityGroupDefinition> CollectEntityGroupDefinitions()
        {
            var groupDefinitionGuids = AssetDatabase.FindAssets($"t: {typeof(EntityGroupDefinition)}");
            var groupDefinitions = new List<EntityGroupDefinition>();
            foreach (var guid in groupDefinitionGuids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var groupDefinition = AssetDatabase.LoadAssetAtPath<EntityGroupDefinition>(path);
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
            var enumValues = Enum.GetNames(typeof(EntityGroup)).ToList();
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
        public static void RegenerateGroupEnums()
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
            WriteBody(GetEntityGroupConfig().GroupNames);
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
            RegenerateGroupEnums();
            Debug.Log($":: -- {GeneratedFileName} has been generated");
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
        private static void WriteBody(List<string> allEntityGroups)
        {
            int indentation = 1;
            
            AppendContent(GetLine("[System.Flags]", indentation));

            AppendContent(GetLine("public enum EntityGroup : long", indentation));
            AppendContent(GetLine("{", indentation));
            indentation++;
            int groupCount = 0;

            //AppendContent(GetLine($"None = 0L,", indentation));
            foreach (var group in allEntityGroups)
            {
                if (group == null)
                {
                    continue;
                }
                groupCount++;

                AppendContent(GetLine($"{@group} = 1L << {groupCount},", indentation));

            }
            indentation--;
            AppendContent(GetLine("}", indentation));
            indentation--;
            AppendContent(GetLine("}", indentation));
        }
        private static void WriteBody(List<EntityGroupDefinition> allEntityGroups)
        {
            int indentation = 1;
            
            AppendContent(GetLine("[System.Flags]", indentation));

            AppendContent(GetLine("public enum EntityGroup : long", indentation));
            AppendContent(GetLine("{", indentation));
            indentation++;
            int groupCount = 0;

            //AppendContent(GetLine($"None = 0L,", indentation));
            foreach (var group in allEntityGroups)
            {
                if (group == null)
                {
                    continue;
                }
                groupCount++;

                AppendContent(GetLine($"{group.EntityType.ToString()} = 1L << {groupCount},", indentation));

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
    }
}
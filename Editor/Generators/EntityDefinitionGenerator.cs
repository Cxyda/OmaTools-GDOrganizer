using System;
using System.Collections.Generic;
using System.IO;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Window;
using Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition;
using UnityEditor;

#if UNITY_EDITOR


namespace Plugins.O.M.A.Games.GDOrganizer.Editor.Generators
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static class EntityDefinitionGenerator
    {
        private const string GeneratedFilePath = "EntityDefinitions/";
        public const string NameSuffix = "Definition";
        public const string DefinitionNameSpace = "Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition";
        private static string _generatedFileContent = "";

        private static readonly List<string> EnumNames = new List<string>();

        private static DateTime _now;
        private static EntityGroup _entityGroup;

        public static void GenerateGroupDefinitionFile(EntityGroup entityGroup)
        {
            _entityGroup = entityGroup;
            _generatedFileContent = "";
            _now = DateTime.Now;

            var path = "";
            var fullPath = "";
            try
            {
                path = Path.Combine(GdOrganizerEditorUtils.GetSettingsFile().GeneratedScriptsRootPath,
                    GeneratedFilePath);
                fullPath = Path.Combine(path, $"{_entityGroup}{NameSuffix}.cs");
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
            File.WriteAllText(fullPath, _generatedFileContent);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void WriteBody()
        {
            var indentation = 1;
            
            AppendContent(GetLine($"public partial class {_entityGroup}Definition : ScriptableObject, IEntityDefinition", indentation));
            AppendContent(GetLine("{", indentation));
            indentation++;
            var groupCount = 0;

            AppendContent(GetLine($"private const EntityGroup Group = EntityGroup.{_entityGroup};", indentation));
            AppendContent(GetLine("[SerializeField, ReadOnly]", indentation));
            AppendContent(GetLine("private EntityType _entityType;", indentation));

            AppendContent(GetLine("public EntityType EntityType => _entityType;", indentation));
            AppendContent(GetLine("#if UNITY_EDITOR", 0));
            AppendContent(GetLine("public void SetEntityType(EntityType type)", indentation));
            AppendContent(GetLine("{", indentation));
            indentation++;
            AppendContent(GetLine("_entityType = type;", indentation));
            indentation--;
            AppendContent(GetLine("}", indentation));
            AppendContent(GetLine("#endif", 0));

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
            AppendContent(GetLine("using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;"));
            AppendContent(GetLine("using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;"));
            AppendContent(GetLine("using UnityEngine;"));
            AppendContent(GetLine(""));

            AppendContent(GetLine($"namespace {DefinitionNameSpace}"));
            
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
            AppendContent(GetLine("", 1));
            AppendContent(GetLine("/// NOTE: Remember to inherit from this class OR extend it with a partial class to customize attributes.", 1));
            AppendContent(GetLine("/// ----------------------------------------------------------------------------------------------------", 1));
            AppendContent(GetLine("", 1));
            AppendContent(GetLine($"/// Last time generated: {_now}", 1));
            AppendContent(GetLine($"///This script has been generated by '{typeof(EntityGroupGenerator).Name}'", 1));
            AppendContent(GetLine("/// </summary>", 1));
        }
    }
}

#endif
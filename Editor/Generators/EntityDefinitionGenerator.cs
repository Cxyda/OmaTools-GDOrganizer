using System.Collections.Generic;
using System.IO;
using O.M.A.Games.GDOrganizer.Generated;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Window;
using UnityEditor;

#if UNITY_EDITOR


namespace Plugins.O.M.A.Games.GDOrganizer.Editor.Generators
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static class EntityDefinitionGenerator
    {
        public const string NameSuffix = "Definition";
        private static string _generatedFileContent = "";

        private static readonly List<string> EnumNames = new List<string>();

        private static EntityProperty _entityProperty;

        public static void GeneratePropertyDefinitionFile(EntityProperty entityProperty)
        {
            _entityProperty = entityProperty;
            _generatedFileContent = "";

            var path = "";
            var fullPath = "";
            try
            {
                path = Path.Combine(GdOrganizerEditorUtils.GetSettingsFile().DefinitionTemplatePath);
                fullPath = Path.Combine(path, $"{_entityProperty}{NameSuffix}.cs");
                
                if (File.Exists(fullPath))
                {
                    return;
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
            catch
            {
                // ignored
            }
        }

        private static void WriteBody()
        {
            var indentation = 1;
            
            AppendContent(GetLine($"public class {_entityProperty}Definition : ScriptableObject, IEntityDefinition", indentation));
            AppendContent(GetLine("{", indentation));
            indentation++;

            AppendContent(GetLine($"private const EntityProperty Component = EntityProperty.{_entityProperty};", indentation));
            AppendContent(GetLine("[SerializeField, ReadOnly]", indentation));
            AppendContent(GetLine("private EntityType _entityType;", indentation));

            AppendContent(GetLine("public EntityType EntityType => _entityType;", indentation));
            AppendContent(GetLine("", indentation));
            AppendContent(GetLine("// Put in your custom code here ...", indentation));
            AppendContent(GetLine("", indentation));
            AppendContent(GetLine("", indentation));
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
            AppendContent(GetLine("using O.M.A.Games.GDOrganizer.Generated;"));
            AppendContent(GetLine("using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;"));
            AppendContent(GetLine("using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;"));
            
            
            AppendContent(GetLine("using UnityEngine;"));
            AppendContent(GetLine(""));

            AppendContent(GetLine($"namespace {GeneratorConstants.DefinitionNamespace}"));
            
            AppendContent(GetLine("{"));
        }
        private static void WriteDisclaimer()
        {
            AppendContent(GetLine("/// <summary>", 1));
            AppendContent(GetLine("/// This class template has been generated.", 1));
            AppendContent(GetLine("/// You can add your custom code within the class body.", 1));
            AppendContent(GetLine("/// As long as the file is present it won't be overwritten", 1));
            AppendContent(GetLine("", 1));
            AppendContent(GetLine("/// NOTE: If you want to regenerate this file you have to delete it and press regenerate.", 1));
            AppendContent(GetLine("/// ----------------------------------------------------------------------------------------------------", 1));
            AppendContent(GetLine("", 1));
            AppendContent(GetLine($"///This class template has been generated by '{typeof(EntityDefinitionGenerator).Name}'", 1));
            AppendContent(GetLine("/// </summary>", 1));
        }
    }
}

#endif
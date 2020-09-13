using System.Collections.Generic;
using System.IO;
using Modules.O.M.A.Games.GDOrganizer.Editor.Utils;
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
            
            AppendContent(CodeGenerationHelper.GetLine($"public class {_entityProperty}Definition : ScriptableObject, IEntityDefinition", indentation));
            AppendContent(CodeGenerationHelper.GetLine("{", indentation));
            indentation++;

            AppendContent(CodeGenerationHelper.GetLine($"private const EntityProperty Component = EntityProperty.{_entityProperty};", indentation));
            AppendContent(CodeGenerationHelper.GetLine("[SerializeField, ReadOnly]", indentation));
            AppendContent(CodeGenerationHelper.GetLine("private EntityType _entityType;", indentation));

            AppendContent(CodeGenerationHelper.GetLine("public EntityType EntityType => _entityType;", indentation));
            AppendContent(CodeGenerationHelper.GetLine("", indentation));
            AppendContent(CodeGenerationHelper.GetLine("// Put in your custom code here ...", indentation));
            AppendContent(CodeGenerationHelper.GetLine("", indentation));
            AppendContent(CodeGenerationHelper.GetLine("", indentation));
            AppendContent(CodeGenerationHelper.GetLine("#if UNITY_EDITOR", 0));
            AppendContent(CodeGenerationHelper.GetLine("public void SetEntityType(EntityType type)", indentation));
            AppendContent(CodeGenerationHelper.GetLine("{", indentation));
            indentation++;
            AppendContent(CodeGenerationHelper.GetLine("_entityType = type;", indentation));
            indentation--;
            AppendContent(CodeGenerationHelper.GetLine("}", indentation));
            AppendContent(CodeGenerationHelper.GetLine("#endif", 0));

            indentation--;
            AppendContent(CodeGenerationHelper.GetLine("}", indentation));
            indentation--;
            AppendContent(CodeGenerationHelper.GetLine("}", indentation));
        }

        private static void AppendContent(string content)
        {
            _generatedFileContent += content;
        }

        private static void Writeheader()
        {
            AppendContent(CodeGenerationHelper.GetLine("using O.M.A.Games.GDOrganizer.Generated;"));
            AppendContent(CodeGenerationHelper.GetLine("using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;"));
            AppendContent(CodeGenerationHelper.GetLine("using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;"));
            
            
            AppendContent(CodeGenerationHelper.GetLine("using UnityEngine;"));
            AppendContent(CodeGenerationHelper.GetLine(""));

            AppendContent(CodeGenerationHelper.GetLine($"namespace {GeneratorConstants.DefinitionNamespace}"));
            
            AppendContent(CodeGenerationHelper.GetLine("{"));
        }
        private static void WriteDisclaimer()
        {
            AppendContent(CodeGenerationHelper.GetLine("/// <summary>", 1));
            AppendContent(CodeGenerationHelper.GetLine("/// This class template has been generated.", 1));
            AppendContent(CodeGenerationHelper.GetLine("/// You can add your custom code within the class body.", 1));
            AppendContent(CodeGenerationHelper.GetLine("/// As long as the file is present it won't be overwritten", 1));
            AppendContent(CodeGenerationHelper.GetLine("", 1));
            AppendContent(CodeGenerationHelper.GetLine("/// NOTE: If you want to regenerate this file you have to delete it and press regenerate.", 1));
            AppendContent(CodeGenerationHelper.GetLine("/// ----------------------------------------------------------------------------------------------------", 1));
            AppendContent(CodeGenerationHelper.GetLine("", 1));
            AppendContent(CodeGenerationHelper.GetLine($"///This class template has been generated by '{typeof(EntityDefinitionGenerator).Name}'", 1));
            AppendContent(CodeGenerationHelper.GetLine("/// </summary>", 1));
        }
    }
}

#endif
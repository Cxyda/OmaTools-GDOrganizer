using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Modules.O.M.A.Games.GDOrganizer.Editor.Utils;
using O.M.A.Games.GDOrganizer.Generated;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Utils;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.GdOrganizer;
using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor.Generators
{
	/// <summary>
	/// TODO:
	/// </summary>
	public class EntityLookupGenerator
	{
		private static readonly string GeneratedFileName = $"{GeneratorConstants.EntityLookupName}.cs";

		[MenuItem("Generate/Run")]
		public static void GenerateLookup()
		{
			string equalityCompararContent = "";
			string typePropLookupContent = "";
			string propTypeLookupContent = "";

			Dictionary<EntityProperty, List<EntityType>>  PropertyTypeMap = new Dictionary<EntityProperty, List<EntityType>>();
			Dictionary<EntityType, List<EntityProperty>>  TypePropertyMap = new Dictionary<EntityType, List<EntityProperty>>();

			string fileContent = "";
			string typePropertyLookupName = "TypePropertiesLookup";
			string propertyTypeLookupName = "PropertyTypeLookup";
			var path = "Assets/Modules/O.M.A.Games/GDOrganizer/Generated";

			var existingTypeDefinitions = ScriptableObjectEditorUtils.FindAllOfType<EntityTypeDefinition>();
			var existingPropertyDefinitions = ScriptableObjectEditorUtils.FindAllOfType<EntityPropertyDefinition>();

			foreach (var propertyDefinition in existingPropertyDefinitions)
			{
				if (Enum.TryParse(propertyDefinition.EntityType.ToString(), out EntityProperty entityProperty))
				{
					PropertyTypeMap.Add(entityProperty, new List<EntityType>());
				}
				else
				{
					Debug.LogError("Cannot cast " + propertyDefinition);
				}
			}
			foreach (var typeDefinition in existingTypeDefinitions)
			{
				var properties = new List<EntityProperty>();
				foreach (var property in typeDefinition.EntityProperties.Properties)
				{
					properties.Add(property);
					if (PropertyTypeMap.ContainsKey(property))
					{
						PropertyTypeMap[property].Add(typeDefinition.EntityType);
					}
					else
					{
						Debug.LogError("Cannot find property " + property);
					}
				}
				TypePropertyMap.Add(typeDefinition.EntityType, properties);
			}
			

			var settings = ScriptableObjectEditorUtils.FindFirstOfType<GdOrganizerSettings>();
			var fullPath = Path.Combine(path, GeneratedFileName);

			equalityCompararContent += GenerateEqualityComparer<EntityType>();
			equalityCompararContent += GenerateEqualityComparer<EntityProperty>();

			typePropLookupContent += GenerateLookupMap(typePropertyLookupName, TypePropertyMap);
			propTypeLookupContent += GenerateLookupMap(propertyTypeLookupName, PropertyTypeMap);
			
			AppendContent(CodeGenerationHelper.GetLine("using System;"), ref fileContent);
			AppendContent(CodeGenerationHelper.GetLine("using System.Collections.Generic;"), ref fileContent);
			AppendContent(CodeGenerationHelper.GetLine("using System.Linq;"), ref fileContent);
			AppendContent(CodeGenerationHelper.GetLine(""), ref fileContent);
			AppendContent(CodeGenerationHelper.GetLine("namespace O.M.A.Games.GDOrganizer.Generated"), ref fileContent);
			AppendContent(CodeGenerationHelper.GetLine("{"), ref fileContent);
			AppendContent(CodeGenerationHelper.GetLine("public static class EntityLookup", 1), ref fileContent);
			AppendContent(CodeGenerationHelper.GetLine("{", 1), ref fileContent);
			AppendContent(CodeGenerationHelper.GetLine(equalityCompararContent), ref fileContent);

			AppendContent(CodeGenerationHelper.GetLine(GenerateHasLookupMethod<EntityType, EntityProperty>("HasProperty", typePropertyLookupName), 2), ref fileContent);
			AppendContent(CodeGenerationHelper.GetLine(GenerateHasLookupMethod<EntityProperty, EntityType>("HasType", propertyTypeLookupName), 2), ref fileContent);
			AppendContent(CodeGenerationHelper.GetLine(GenerateGetLookupMethod<EntityType, EntityProperty>("GetProperties", typePropertyLookupName), 2), ref fileContent);
			AppendContent(CodeGenerationHelper.GetLine(GenerateGetLookupMethod<EntityProperty, EntityType>("GetTypes", propertyTypeLookupName), 2), ref fileContent);

			AppendContent(CodeGenerationHelper.GetLine(typePropLookupContent, 2), ref fileContent);
			AppendContent(CodeGenerationHelper.GetLine(propTypeLookupContent, 2), ref fileContent);
			AppendContent(CodeGenerationHelper.GetLine("}", 1), ref fileContent);
			AppendContent(CodeGenerationHelper.GetLine("}"), ref fileContent);

			if (!Directory.Exists(fullPath))
			{
				Directory.CreateDirectory(path);
			}
			File.WriteAllText(fullPath, fileContent);
            
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
		private static void AppendContent(string content, ref string target)
		{
			target += content;
		}

		private static string GenerateLookupMap<TKey, TValue>(string lookupMapName, Dictionary<TKey, List<TValue>> lookupMap)
		{
			var content = "";
			AppendContent(CodeGenerationHelper.GetLine($"private static readonly Dictionary<{typeof(TKey).Name}, IList<{typeof(TValue).Name}>> {lookupMapName} =", 2),ref content);
			AppendContent(CodeGenerationHelper.GetLine($"new Dictionary<{typeof(TKey).Name}, IList<{typeof(TValue).Name}>>(new {GetEqualityComparerName<TKey>()}())", 3),ref content);
			AppendContent(CodeGenerationHelper.GetLine("{", 3),ref content);
			foreach (var keyValuePar in lookupMap)
			{
				AppendContent(GenerateLookupEntry(keyValuePar.Key, keyValuePar.Value), ref content);
			}
			AppendContent(CodeGenerationHelper.GetLine("};", 3),ref content);

			return content;
		}
		private static string GenerateLookupEntry<TKey, TValue>(TKey key, IList<TValue> values)
		{
			var content = "";
			AppendContent(CodeGenerationHelper.GetLine("{", 4),ref content);
			AppendContent(CodeGenerationHelper.GetLine($"{typeof(TKey).Name}.{key},", 5),ref content);
			AppendContent(CodeGenerationHelper.GetLine($"new List<{typeof(TValue).Name}>", 5),ref content);
			AppendContent(CodeGenerationHelper.GetLine("{", 5),ref content);
			foreach (var value in values)
			{
				AppendContent(CodeGenerationHelper.GetLine($"{typeof(TValue).Name}.{value},", 6),ref content);
			}
			AppendContent(CodeGenerationHelper.GetLine("}", 5),ref content);
			AppendContent(CodeGenerationHelper.GetLine("},", 4),ref content);
			return content;
		}
		private static string GetEqualityComparerName<T>()
		{
			return $"{typeof(T).Name}Comparer";
		}
		private static string GenerateEqualityComparer<T>()
		{
			var content = "";
			var typeName = typeof(T).Name;

			AppendContent(CodeGenerationHelper.GetLine(
				$"public struct {GetEqualityComparerName<T>()} : IEqualityComparer<{typeName}>", 2), ref content);
			AppendContent(CodeGenerationHelper.GetLine("{", 2),ref content);
			AppendContent(CodeGenerationHelper.GetLine($"public bool Equals({typeName} x, {typeName} y)", 3),ref content);
			AppendContent(CodeGenerationHelper.GetLine("{", 3),ref content);
			AppendContent(CodeGenerationHelper.GetLine("return x == y;", 4),ref content);
			AppendContent(CodeGenerationHelper.GetLine("}", 3),ref content);
			AppendContent(CodeGenerationHelper.GetLine(""),ref content);
			AppendContent(CodeGenerationHelper.GetLine($"public int GetHashCode({typeName} obj)", 3),ref content);
			AppendContent(CodeGenerationHelper.GetLine("{", 3),ref content);
			AppendContent(CodeGenerationHelper.GetLine("return (int) obj;", 4),ref content);
			AppendContent(CodeGenerationHelper.GetLine("}", 3),ref content);
			AppendContent(CodeGenerationHelper.GetLine("}", 2),ref content);
			return content;
		}

		private static string GenerateHasLookupMethod<Tkey, TType>(string methodName, string lookupTableName)
		{
			var content = "";
			AppendContent(CodeGenerationHelper.GetLine($"public static bool {methodName}(this {typeof(Tkey).Name} key, {typeof(TType)} value)", 2),ref content);
			AppendContent(CodeGenerationHelper.GetLine("{", 2),ref content);
			AppendContent(CodeGenerationHelper.GetLine($"if (!{lookupTableName}.TryGetValue(key, out var values)) throw new NotImplementedException($\"The given key '"+ "{key}" +"' could not be found\");", 3),ref content);
			AppendContent(CodeGenerationHelper.GetLine("return values.Any(p => p == value);", 3),ref content);
			AppendContent(CodeGenerationHelper.GetLine("}", 2),ref content);

			return content;
		}

		private static string GenerateGetLookupMethod<TIn, Tout>(string methodName, string lookupTableName)
		{
			var content = "";
			AppendContent(CodeGenerationHelper.GetLine($"public static IList<{typeof(Tout).Name}> {methodName}(this {typeof(TIn).Name} key)", 2),ref content);
			AppendContent(CodeGenerationHelper.GetLine("{", 2),ref content);
			AppendContent(CodeGenerationHelper.GetLine($"if (!{lookupTableName}.TryGetValue(key, out var values)) throw new NotImplementedException($\"The given key '"+ "{key}" +"' could not be found\");", 3),ref content);
			AppendContent(CodeGenerationHelper.GetLine("return values;", 3),ref content);
			AppendContent(CodeGenerationHelper.GetLine("}", 2),ref content);
			return content;

		}
		
	}
}
using System;
using O.M.A.Games.GDOrganizer.Generated;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.ExtensionMethods
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static partial class EntityLookup
    {


        public static bool IsInGroup(this EntityType type, EntityProperty property)
        {
            EntityTypeDefinition typeDefinition;

#if UNITY_EDITOR
            var guids = AssetDatabase.FindAssets("t:EntityTypeDefinition " + type);
            if (guids.Length == 0)
            {
                throw new Exception($"Cant find property definition {property} for entity type {type}");
            }

            string guid = guids[0];
            string path = AssetDatabase.GUIDToAssetPath(guid);
            typeDefinition = AssetDatabase.LoadAssetAtPath<EntityTypeDefinition>(path);
#else
            typeDefinition = GdDataProvider.GetEntityTypeDefinition(type);
#endif
            return typeDefinition.EntityProperties.HasProperty(property);
        }
        public static Sprite UiIcon(this EntityType type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the EntityTypeDefinition of the given EntityType
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static EntityTypeDefinition Definition(this EntityType entityType)
        {
            //return GdDataProvider.GetEntityTypeDefinition(entityType);
            return null;
        }
        /// <summary>
        /// Returns the Definition of the Entity of the given Group
        /// </summary>
        /// <param name="property"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public static IEntityDefinition GroupDefinition(this EntityType type, EntityProperty property)
        {
            switch (property)
            {
//                case EntityProperty.Currency:
//                    break;
//                case EntityProperty.Building:
//                    return GdDataProvider.GetBuildingDefinition(type);

                default:
                    throw new ArgumentOutOfRangeException();
            }
            throw new NotImplementedException();
        }
    }
}
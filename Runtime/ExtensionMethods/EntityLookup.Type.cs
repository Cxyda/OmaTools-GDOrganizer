using System;
using Data.EntityDefinition;
using Logic;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using Services.GddService;
using UnityEditor;
using UnityEngine;

namespace Services.EntityService.ExtensionMethods
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static partial class EntityLookup
    {
        /// <summary>
        /// Returns the Groups of the entity as Flag
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static EntityGroup Groups(this EntityType type)
        {
            EntityTypeDefinition typeDefinition = GddLoader.GetEntityTypeDefinition(type);
            return typeDefinition.EntityGroups;
        }

        public static bool IsInGroup(this EntityType type, EntityGroup group)
        {
            EntityTypeDefinition typeDefinition;

#if UNITY_EDITOR
            var guids = AssetDatabase.FindAssets("t:EntityTypeDefinition " + type);
            if (guids.Length == 0)
            {
                throw new Exception($"Cant find group definition {group} for entity type {type}");
            }

            string guid = guids[0];
            string path = AssetDatabase.GUIDToAssetPath(guid);
            typeDefinition = AssetDatabase.LoadAssetAtPath<EntityTypeDefinition>(path);
#else
            typeDefinition = GddLoader.GetEntityTypeDefinition(type);
#endif
            return typeDefinition.EntityGroups.HasFlag(group);
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
            //return GddLoader.GetEntityTypeDefinition(entityType);
            return null;
        }
        /// <summary>
        /// Returns the Definition of the Entity of the given Group
        /// </summary>
        /// <param name="group"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public static IEntityDefinition GroupDefinition(this EntityType type, EntityGroup group)
        {
            switch (group)
            {
//                case EntityGroup.Currency:
//                    break;
//                case EntityGroup.Building:
//                    return GddLoader.GetBuildingDefinition(type);

                default:
                    throw new ArgumentOutOfRangeException();
            }
            throw new NotImplementedException();
        }
    }
}
using System;
using System.IO;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Generated;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.GddLoader
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static partial class GddLoader
    {
        private static EntityTypeLoader<EntityTypeDefinition> _entityTypeLoader;


        public static void LoadAll(LoadManager runner)
        {
            _entityTypeLoader = new EntityTypeLoader<EntityTypeDefinition>();

            // WARNING ! The Order is Important here ! Load resources with the fewest dependencies first !
            _entityTypeLoader.LoadAll(runner);

            
        }

        public static EntityTypeDefinition GetEntityTypeDefinition(EntityType entityType)
        {
            EntityTypeDefinition definition;
            if (!_entityTypeLoader.DataMap.TryGetValue(entityType, out definition))
            {
                throw new Exception($"EntityTypeDefinition for EntityType '{entityType}' could not be found.");
            }

            return definition;
        }



        private static AssetBundle LoadAssetBundle(string relativeAssetBundlePath)
        {
            return AssetBundle.LoadFromFile(Path.Combine(Application.dataPath, relativeAssetBundlePath));
        }

    }
}
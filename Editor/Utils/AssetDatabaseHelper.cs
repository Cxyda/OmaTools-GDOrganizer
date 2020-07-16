using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor.Utils
{
#if UNITY_EDITOR
    public static class AssetDatabaseHelper
    {
        /// <summary>
        /// Loads the first asset found by the given filter
        /// </summary>
        /// <param name="assetFilter">The asset filter can be the name, or any search filter Unity supports. E.G. 't:ScriptableObject Settings'</param>
        /// <typeparam name="T">Type of the asset</typeparam>
        public static T LoadFirstAssetByFilter<T>(string assetFilter,  string[] searchInFolders = null) where T : UnityEngine.Object
        {
            var guids = AssetDatabase.FindAssets(assetFilter, searchInFolders);
            if (guids.Length > 0)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                return AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }

            Debug.LogError($"Unable to find asset '{assetFilter}'");

            return null;
        }
    }
#endif
}
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor.Utils
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static class ScriptableObjectEditorUtils
    {
        public static List<T> FindAllOfType<T>() where T : ScriptableObject
        {
            var guids = AssetDatabase.FindAssets($"t: {typeof(T)}");
            var scriptableObjects = new List<T>();
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var so = AssetDatabase.LoadAssetAtPath<T>(path);
                if (so == null)
                {
                    continue;
                }

                scriptableObjects.Add(so);
            }

            return scriptableObjects;
        }

        public static T FindFirstOfType<T>() where T : ScriptableObject
        {
            var allOfType = FindAllOfType<T>();
            if (allOfType.Count > 1)
            {
                Debug.LogWarning($"Multiple instances of type '{typeof(T)}'. Returning {AssetDatabase.GetAssetPath(allOfType[0])}");
            }
            return allOfType.Count > 0 ? allOfType[0] : null;
        }
    }
}
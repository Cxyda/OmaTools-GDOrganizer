using System;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static class GeneratorUtils
    {
        public static Type GetTypeFromName(string name)
        {
            var type = Type.GetType(name);
            if (type == null)
            {
                throw new Exception($"Unable to find type {name}");
            }

            return type;
        }
        public static Type TryGetTypeFromName(string name)
        {
            var type = Type.GetType(name);
            return type;
        }
    }
}
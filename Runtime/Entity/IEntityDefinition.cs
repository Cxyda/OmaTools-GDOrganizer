
using Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// TODO:
    /// </summary>
    public interface IEntityDefinition
    {
        EntityType EntityType { get; }
#if UNITY_EDITOR
        void SetEntityType(EntityType type);
#endif
    }
}
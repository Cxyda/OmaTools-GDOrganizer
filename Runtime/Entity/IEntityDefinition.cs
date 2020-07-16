
using O.M.A.Games.GDOrganizer.Generated;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// TODO:
    /// </summary>
    public interface IEntityDefinition
    {
        EntityType EntityType { get; }
        void SetEntityType(EntityType type);
    }
}
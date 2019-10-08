namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity
{
    /// <summary>
    /// The RoomId Factory is responsible to generate unique EntityIds for dynamic game data
    /// </summary>
    public static class EntityIdFactory
    {
        private static uint _entityInstances = 0;

        public static EntityId Create()
        {
            return new EntityId(++_entityInstances);
        }
    }
}
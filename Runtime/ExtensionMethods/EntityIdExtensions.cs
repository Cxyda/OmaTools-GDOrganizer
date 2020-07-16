using System;
using Code.Core;
using O.M.A.Games.GDOrganizer.Generated;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;

namespace Plugins.O.M.A.Games.GDOrganizer.Runtime.ExtensionMethods
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static class EntityIdExtensions
    {
        public static void Validate(this EntityId entityId)
        {
            if (entityId == EntityId.Invalid)
            {
                throw new Exception(
                    $"The RoomId {entityId.ReferenceId} is invalid. Please use the EntityIdFactory to create a valid Id.");
            }
        }
        public static void Validate(this EntityType entityType)
        {
            if (entityType == EntityType.Invalid)
            {
                throw new Exception(
                    $"The EntityType {EntityType.Invalid} is invalid. Please assign 'None' if you want to have it empty.");
            }
        }
    }
}
using O.M.A.Games.GDOrganizer.Generated;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using UnityEngine;

namespace O.M.A.Games.GDOrganizer.GameDesignDefinition
{
	/// <summary>
	/// This class template has been generated.
	/// You can add your custom code within the class body.
	/// As long as the file is present it won't be overwritten
	
	/// NOTE: If you want to regenerate this file you have to delete it and press regenerate.
	/// ----------------------------------------------------------------------------------------------------
	
	///This class template has been generated by 'EntityDefinitionGenerator'
	/// </summary>
	public class LivingRoomDefinition : ScriptableObject, IEntityDefinition
	{
		private const EntityProperty Component = EntityProperty.LivingRoom;
		[SerializeField, ReadOnly]
		private EntityType _entityType;
		public EntityType EntityType => _entityType;
		
		// Put in your custom code here ...
		
		
#if UNITY_EDITOR
		public void SetEntityType(EntityType type)
		{
			_entityType = type;
		}
#endif
	}
}

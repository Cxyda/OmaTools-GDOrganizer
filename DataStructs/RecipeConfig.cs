using System;
using System.Collections.Generic;
using O.M.A.Games.GDOrganizer.Generated;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;

namespace Modules.O.M.A.Games.GDOrganizer.DataStructs
{
	[Serializable]
	public struct RecipeConfig
	{
		[ReadOnly] public EntityType EntityType;
		[ReadOnly] public EntityProperty EntityProperty;

		public int RequiredWorkforce;

		public List<TypeIntTuple> Input;
		public TypeIntTuple Output;
		public List<DropChanceTuple> AdditionalOutput;
	}
	[Serializable]
	public struct TypeIntTuple
	{
		public EntityType EntityType;
		public int Amount;
	}
	[Serializable]
	public struct DropChanceTuple
	{
		public EntityType EntityType;
		public int Amount;
		public int Weight;
	}
}
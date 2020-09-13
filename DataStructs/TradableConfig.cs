using System;
using O.M.A.Games.GDOrganizer.Generated;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;

namespace Modules.O.M.A.Games.GDOrganizer.DataStructs
{
	[Serializable]
	public struct TradableConfig
	{
		[ReadOnly] public EntityType EntityType;
		[ReadOnly] public EntityProperty EntityProperty;

		public int BasePrice;
	}
}
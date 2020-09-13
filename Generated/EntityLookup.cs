using System;
using System.Collections.Generic;
using System.Linq;

namespace O.M.A.Games.GDOrganizer.Generated
{
	public static class EntityLookup
	{
public struct EntityTypeComparer : IEqualityComparer<EntityType>
		{
			public bool Equals(EntityType x, EntityType y)
			{
				return x == y;
			}

			public int GetHashCode(EntityType obj)
			{
				return (int) obj;
			}
		}
		public struct EntityPropertyComparer : IEqualityComparer<EntityProperty>
		{
			public bool Equals(EntityProperty x, EntityProperty y)
			{
				return x == y;
			}

			public int GetHashCode(EntityProperty obj)
			{
				return (int) obj;
			}
		}
		public static bool HasProperty(this EntityType key, O.M.A.Games.GDOrganizer.Generated.EntityProperty value)
		{
			if (!TypePropertiesLookup.TryGetValue(key, out var values)) throw new NotImplementedException($"The given key '{key}' could not be found");
			return values.Any(p => p == value);
		}
		public static bool HasType(this EntityProperty key, O.M.A.Games.GDOrganizer.Generated.EntityType value)
		{
			if (!PropertyTypeLookup.TryGetValue(key, out var values)) throw new NotImplementedException($"The given key '{key}' could not be found");
			return values.Any(p => p == value);
		}
		public static IList<EntityProperty> GetProperties(this EntityType key)
		{
			if (!TypePropertiesLookup.TryGetValue(key, out var values)) throw new NotImplementedException($"The given key '{key}' could not be found");
			return values;
		}
		public static IList<EntityType> GetTypes(this EntityProperty key)
		{
			if (!PropertyTypeLookup.TryGetValue(key, out var values)) throw new NotImplementedException($"The given key '{key}' could not be found");
			return values;
		}
		private static readonly Dictionary<EntityType, IList<EntityProperty>> TypePropertiesLookup =
			new Dictionary<EntityType, IList<EntityProperty>>(new EntityTypeComparer())
			{
				{
					EntityType.Apples,
					new List<EntityProperty>
					{
						EntityProperty.Tradable,
						EntityProperty.Resource,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.Attribute,
					new List<EntityProperty>
					{
						EntityProperty.Attribute,
					}
				},
				{
					EntityType.Audacity,
					new List<EntityProperty>
					{
						EntityProperty.Attribute,
					}
				},
				{
					EntityType.Beam,
					new List<EntityProperty>
					{
						EntityProperty.Tradable,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.Beer,
					new List<EntityProperty>
					{
						EntityProperty.Tradable,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.Boots,
					new List<EntityProperty>
					{
						EntityProperty.Tradable,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.Building,
					new List<EntityProperty>
					{
						EntityProperty.Building,
					}
				},
				{
					EntityType.Carpentry,
					new List<EntityProperty>
					{
						EntityProperty.Workshop,
						EntityProperty.Room,
					}
				},
				{
					EntityType.Character,
					new List<EntityProperty>
					{
						EntityProperty.Character,
					}
				},
				{
					EntityType.Charisma,
					new List<EntityProperty>
					{
						EntityProperty.Attribute,
					}
				},
				{
					EntityType.City,
					new List<EntityProperty>
					{
						EntityProperty.City,
					}
				},
				{
					EntityType.Dexterity,
					new List<EntityProperty>
					{
						EntityProperty.Attribute,
					}
				},
				{
					EntityType.GoldIngot,
					new List<EntityProperty>
					{
						EntityProperty.Tradable,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.Good,
					new List<EntityProperty>
					{
						EntityProperty.Good,
					}
				},
				{
					EntityType.Hammer,
					new List<EntityProperty>
					{
						EntityProperty.Tradable,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.Herbs,
					new List<EntityProperty>
					{
						EntityProperty.Tradable,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.Intellect,
					new List<EntityProperty>
					{
						EntityProperty.Attribute,
					}
				},
				{
					EntityType.Invalid,
					new List<EntityProperty>
					{
					}
				},
				{
					EntityType.IronBar,
					new List<EntityProperty>
					{
						EntityProperty.Good,
						EntityProperty.Tradable,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.IronOre,
					new List<EntityProperty>
					{
						EntityProperty.Resource,
						EntityProperty.Tradable,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.LivingRoom,
					new List<EntityProperty>
					{
						EntityProperty.Room,
						EntityProperty.LivingRoom,
					}
				},
				{
					EntityType.Log,
					new List<EntityProperty>
					{
						EntityProperty.Good,
						EntityProperty.Tradable,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.Luck,
					new List<EntityProperty>
					{
						EntityProperty.Attribute,
					}
				},
				{
					EntityType.Lübeck,
					new List<EntityProperty>
					{
						EntityProperty.City,
					}
				},
				{
					EntityType.Market,
					new List<EntityProperty>
					{
						EntityProperty.Storage,
						EntityProperty.Building,
					}
				},
				{
					EntityType.Meat,
					new List<EntityProperty>
					{
						EntityProperty.Tradable,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.Money,
					new List<EntityProperty>
					{
						EntityProperty.Tradable,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.Necklace,
					new List<EntityProperty>
					{
						EntityProperty.Tradable,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.None,
					new List<EntityProperty>
					{
					}
				},
				{
					EntityType.Oxcart,
					new List<EntityProperty>
					{
						EntityProperty.Vehicle,
						EntityProperty.Storage,
					}
				},
				{
					EntityType.Paper,
					new List<EntityProperty>
					{
						EntityProperty.Tradable,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.Resource,
					new List<EntityProperty>
					{
						EntityProperty.Resource,
					}
				},
				{
					EntityType.Ring,
					new List<EntityProperty>
					{
						EntityProperty.Tradable,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.Room,
					new List<EntityProperty>
					{
						EntityProperty.Room,
					}
				},
				{
					EntityType.Storable,
					new List<EntityProperty>
					{
						EntityProperty.Storable,
					}
				},
				{
					EntityType.Storage,
					new List<EntityProperty>
					{
						EntityProperty.Storage,
					}
				},
				{
					EntityType.Strength,
					new List<EntityProperty>
					{
						EntityProperty.Attribute,
					}
				},
				{
					EntityType.Sword,
					new List<EntityProperty>
					{
						EntityProperty.Good,
						EntityProperty.Tradable,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.Tradable,
					new List<EntityProperty>
					{
					}
				},
				{
					EntityType.Twigs,
					new List<EntityProperty>
					{
						EntityProperty.Resource,
						EntityProperty.Tradable,
						EntityProperty.Storable,
					}
				},
				{
					EntityType.Vehicle,
					new List<EntityProperty>
					{
						EntityProperty.Vehicle,
					}
				},
				{
					EntityType.Workshop,
					new List<EntityProperty>
					{
						EntityProperty.Room,
						EntityProperty.Workshop,
					}
				},
			};
		private static readonly Dictionary<EntityProperty, IList<EntityType>> PropertyTypeLookup =
			new Dictionary<EntityProperty, IList<EntityType>>(new EntityPropertyComparer())
			{
				{
					EntityProperty.Attribute,
					new List<EntityType>
					{
						EntityType.Attribute,
						EntityType.Audacity,
						EntityType.Charisma,
						EntityType.Dexterity,
						EntityType.Intellect,
						EntityType.Luck,
						EntityType.Strength,
					}
				},
				{
					EntityProperty.Building,
					new List<EntityType>
					{
						EntityType.Building,
						EntityType.Market,
					}
				},
				{
					EntityProperty.Character,
					new List<EntityType>
					{
						EntityType.Character,
					}
				},
				{
					EntityProperty.City,
					new List<EntityType>
					{
						EntityType.City,
						EntityType.Lübeck,
					}
				},
				{
					EntityProperty.Good,
					new List<EntityType>
					{
						EntityType.Good,
						EntityType.IronBar,
						EntityType.Log,
						EntityType.Sword,
					}
				},
				{
					EntityProperty.LivingRoom,
					new List<EntityType>
					{
						EntityType.LivingRoom,
					}
				},
				{
					EntityProperty.Resource,
					new List<EntityType>
					{
						EntityType.Apples,
						EntityType.IronOre,
						EntityType.Resource,
						EntityType.Twigs,
					}
				},
				{
					EntityProperty.Room,
					new List<EntityType>
					{
						EntityType.Carpentry,
						EntityType.LivingRoom,
						EntityType.Room,
						EntityType.Workshop,
					}
				},
				{
					EntityProperty.Storable,
					new List<EntityType>
					{
						EntityType.Apples,
						EntityType.Beam,
						EntityType.Beer,
						EntityType.Boots,
						EntityType.GoldIngot,
						EntityType.Hammer,
						EntityType.Herbs,
						EntityType.IronBar,
						EntityType.IronOre,
						EntityType.Log,
						EntityType.Meat,
						EntityType.Money,
						EntityType.Necklace,
						EntityType.Paper,
						EntityType.Ring,
						EntityType.Storable,
						EntityType.Sword,
						EntityType.Twigs,
					}
				},
				{
					EntityProperty.Storage,
					new List<EntityType>
					{
						EntityType.Market,
						EntityType.Oxcart,
						EntityType.Storage,
					}
				},
				{
					EntityProperty.Tradable,
					new List<EntityType>
					{
						EntityType.Apples,
						EntityType.Beam,
						EntityType.Beer,
						EntityType.Boots,
						EntityType.GoldIngot,
						EntityType.Hammer,
						EntityType.Herbs,
						EntityType.IronBar,
						EntityType.IronOre,
						EntityType.Log,
						EntityType.Meat,
						EntityType.Money,
						EntityType.Necklace,
						EntityType.Paper,
						EntityType.Ring,
						EntityType.Sword,
						EntityType.Twigs,
					}
				},
				{
					EntityProperty.Vehicle,
					new List<EntityType>
					{
						EntityType.Oxcart,
						EntityType.Vehicle,
					}
				},
				{
					EntityProperty.Workshop,
					new List<EntityType>
					{
						EntityType.Carpentry,
						EntityType.Workshop,
					}
				},
			};
	}
}

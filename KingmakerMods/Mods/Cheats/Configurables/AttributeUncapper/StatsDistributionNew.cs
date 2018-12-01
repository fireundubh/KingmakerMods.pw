using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Class.LevelUp;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Configurables.AttributeUncapper
{
	/// <summary>
	/// Allows configuring the maximum allocable permanent value. Cannot support reducing the minimum because the game
	/// will crash when below the default. For example, UpdateMaxLevelSpells() would produce an OverflowException.
	/// </summary>
	[ModifiesType]
	public class StatsDistributionNew : StatsDistribution
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("GetAddCost")]
		public int source_GetAddCost(StatType attribute)
		{
			throw new DeadEndException("source_GetAddCost");
		}

		[NewMember]
		[DuplicatesBody("GetRemoveCost")]
		public int source_GetRemoveCost(StatType attribute)
		{
			throw new DeadEndException("source_GetAddCost");
		}
		#endregion

		[ModifiesMember("GetAddCost")]
		public int mod_GetAddCost(StatType attribute)
		{
			int attributeValue = this.StatValues[attribute];

			if (attributeValue <= 7)
			{
				return 2;
			}

			if (attributeValue >= 17)
			{
				return 4;
			}

			return this.source_GetAddCost(attribute);
		}

		[ModifiesMember("GetRemoveCost")]
		public int mod_GetRemoveCost(StatType attribute)
		{
			int attributeValue = this.StatValues[attribute];

			if (attributeValue <= 7)
			{
				return -2;
			}

			if (attributeValue >= 17)
			{
				return -4;
			}

			return this.source_GetRemoveCost(attribute);
		}

		[ModifiesMember("CanAdd")]
		public bool mod_CanAdd(StatType attribute)
		{
			if (!this.Available)
			{
				return false;
			}

			int attributeValue = this.StatValues[attribute];

			int attributeMax = KingmakerPatchSettings.AttributeUncapper.Enabled ? KingmakerPatchSettings.AttributeUncapper.AttributeMax : 18;

			return attributeValue < attributeMax && this.mod_GetAddCost(attribute) <= this.Points;
		}
	}
}

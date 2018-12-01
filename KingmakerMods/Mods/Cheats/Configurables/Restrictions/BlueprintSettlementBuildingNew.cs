using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Kingdom.Settlements;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Configurables.Restrictions
{
	[ModifiesType]
	public class BlueprintSettlementBuildingNew : BlueprintSettlementBuilding
	{
		[ModifiesMember("CheckRestrictions")]
		public bool mod_CheckRestrictions(SettlementState settlement)
		{
			if (KingmakerPatchSettings.Restrictions.IgnoreBuildingRestrictions)
			{
				return true;
			}

			return this.GetComponents<BuildingRestriction>().All(r => r.CanBuild(this, settlement));
		}

		[ModifiesMember("CheckRestrictions")]
		public bool mod_CheckRestrictions(SettlementState settlement, SettlementGridTopology.Slot slot)
		{
			if (KingmakerPatchSettings.Restrictions.IgnoreBuildingRestrictions)
			{
				return true;
			}

			return this.GetComponents<BuildingRestriction>().All(r => r.CanBuildHere(this, settlement, slot));
		}
	}
}

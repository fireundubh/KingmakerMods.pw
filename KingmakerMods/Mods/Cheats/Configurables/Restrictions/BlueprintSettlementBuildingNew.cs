using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Kingdom.Settlements;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Configurables.Restrictions
{
	[ModifiesType]
	public class BlueprintSettlementBuildingNew : BlueprintSettlementBuilding
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[ModifiesMember("CheckRestrictions")]
		public bool mod_CheckRestrictions(SettlementState settlement)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats.Restrictions", "bIgnoreBuildingRestrictions");
			}

			if (_useMod)
			{
				return true;
			}

			return this.GetComponents<BuildingRestriction>().All(r => r.CanBuild(this, settlement));
		}

		[ModifiesMember("CheckRestrictions")]
		public bool mod_CheckRestrictions(SettlementState settlement, SettlementGridTopology.Slot slot)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats.Restrictions", "bIgnoreBuildingRestrictions");
			}

			if (_useMod)
			{
				return true;
			}

			return this.GetComponents<BuildingRestriction>().All(r => r.CanBuildHere(this, settlement, slot));
		}
	}
}

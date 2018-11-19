using Kingmaker.Blueprints.Items.Components;
using Kingmaker.UnitLogic;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Configurables.Restrictions
{
	[ModifiesType]
	public class EquipmentRestrictionStatNew : EquipmentRestrictionStat
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[ModifiesMember("CanBeEquippedBy")]
		public bool mod_CanBeEquippedBy(UnitDescriptor unit)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats.Restrictions", "bIgnoreEquipmentStatRestrictions");
			}

			if (_useMod)
			{
				return true;
			}

			return unit.Stats.GetStat(this.Stat).PermanentValue >= this.MinValue;
		}
	}
}

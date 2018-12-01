using Kingmaker.Blueprints.Items.Components;
using Kingmaker.UnitLogic;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Configurables.Restrictions
{
	[ModifiesType]
	public class EquipmentRestrictionStatNew : EquipmentRestrictionStat
	{
		[ModifiesMember("CanBeEquippedBy")]
		public bool mod_CanBeEquippedBy(UnitDescriptor unit)
		{
			if (KingmakerPatchSettings.Restrictions.IgnoreEquipmentStatRestrictions)
			{
				return true;
			}

			return unit.Stats.GetStat(this.Stat).PermanentValue >= this.MinValue;
		}
	}
}

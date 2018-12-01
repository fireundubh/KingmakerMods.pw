using Kingmaker.Blueprints.Items.Components;
using Kingmaker.UnitLogic;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Configurables.Restrictions
{
	[ModifiesType]
	public class EquipmentRestrictionClassNew : EquipmentRestrictionClass
	{
		[ModifiesMember("CanBeEquippedBy")]
		public bool mod_CanBeEquippedBy(UnitDescriptor unit)
		{
			if (KingmakerPatchSettings.Restrictions.IgnoreEquipmentClassRestrictions)
			{
				return true;
			}

			return !this.Not && unit.Progression.GetClassLevel(this.Class) > 0 || this.Not && unit.Progression.GetClassLevel(this.Class) == 0;
		}
	}
}

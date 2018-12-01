using Kingmaker.Blueprints.Items.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Configurables.Restrictions
{
	[ModifiesType]
	public class EquipmentRestrictionAlignmentNew : EquipmentRestrictionAlignment
	{
		[ModifiesMember("CanBeEquippedBy")]
		public bool mod_CanBeEquippedBy(UnitDescriptor unit)
		{
			if (KingmakerPatchSettings.Restrictions.IgnoreEquipmentAlignmentRestrictions)
			{
				return true;
			}

			return !this.Not && unit.Alignment.Value.HasComponent(this.Alignment) || this.Not && !unit.Alignment.Value.HasComponent(this.Alignment);
		}
	}
}

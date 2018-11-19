using Kingmaker.Blueprints.Items.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Configurables.Restrictions
{
	[ModifiesType]
	public class EquipmentRestrictionAlignmentNew : EquipmentRestrictionAlignment
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
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats.Restrictions", "bIgnoreEquipmentAlignmentRestrictions");
			}

			if (_useMod)
			{
				return true;
			}

			return !this.Not && unit.Alignment.Value.HasComponent(this.Alignment) || this.Not && !unit.Alignment.Value.HasComponent(this.Alignment);
		}
	}
}

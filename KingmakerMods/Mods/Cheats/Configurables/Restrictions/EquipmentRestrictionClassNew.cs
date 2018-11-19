using Kingmaker.Blueprints.Items.Components;
using Kingmaker.UnitLogic;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Configurables.Restrictions
{
	[ModifiesType]
	public class EquipmentRestrictionClassNew : EquipmentRestrictionClass
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
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats.Restrictions", "bIgnoreEquipmentClassRestrictions");
			}

			if (_useMod)
			{
				return true;
			}

			return !this.Not && unit.Progression.GetClassLevel(this.Class) > 0 || this.Not && unit.Progression.GetClassLevel(this.Class) == 0;
		}
	}
}

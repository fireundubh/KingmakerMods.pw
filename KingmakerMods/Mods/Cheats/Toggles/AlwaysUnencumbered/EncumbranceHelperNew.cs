using Kingmaker.UnitLogic;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.AlwaysUnencumbered
{
	[ModifiesType("Kingmaker.UnitLogic.EncumbranceHelper")]
	public static class EncumbranceHelperNew
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("GetEncumbrance")]
		public static Encumbrance source_GetEncumbrance(UnitDescriptor unit)
		{
			throw new DeadEndException("source_GetEncumbrance");
		}

		[ModifiesMember("GetEncumbrance")]
		public static Encumbrance mod_GetEncumbrance(UnitDescriptor unit)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bAlwaysUnencumbered");
			}

			if (_useMod && unit.IsPlayerFaction)
			{
				return Encumbrance.Light;
			}

			return source_GetEncumbrance(unit);
		}
	}
}

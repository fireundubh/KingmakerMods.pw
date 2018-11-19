using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Parts;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.AlwaysUnencumbered
{
	[ModifiesType]
	public class UnitPartEncumbranceNew : UnitPartEncumbrance
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("GetSpeedPenalty")]
		public static int source_GetSpeedPenalty(UnitDescriptor owner, Encumbrance encumbrance)
		{
			throw new DeadEndException("source_GetSpeedPenalty");
		}

		[NewMember]
		[DuplicatesBody("GetArmorCheckPenalty")]
		public static int source_GetArmorCheckPenalty(UnitDescriptor owner, Encumbrance encumbrance)
		{
			throw new DeadEndException("source_GetArmorCheckPenalty");
		}

		[NewMember]
		[DuplicatesBody("GetMaxDexterityBonus")]
		public static int? source_GetMaxDexterityBonus(UnitDescriptor owner, Encumbrance encumbrance)
		{
			throw new DeadEndException("source_GetMaxDexterityBonus");
		}

		[ModifiesMember("GetSpeedPenalty")]
		public static int mod_GetSpeedPenalty(UnitDescriptor owner, Encumbrance encumbrance)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bAlwaysUnencumbered");
			}

			return _useMod ? 30 : source_GetSpeedPenalty(owner, encumbrance);
		}

		[ModifiesMember("GetArmorCheckPenalty")]
		public static int mod_GetArmorCheckPenalty(UnitDescriptor owner, Encumbrance encumbrance)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bAlwaysUnencumbered");
			}

			return _useMod ? 0 : source_GetArmorCheckPenalty(owner, encumbrance);
		}

		[ModifiesMember("GetMaxDexterityBonus")]
		public static int? mod_GetMaxDexterityBonus(UnitDescriptor owner, Encumbrance encumbrance)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bAlwaysUnencumbered");
			}

			return _useMod ? null : source_GetMaxDexterityBonus(owner, encumbrance);
		}
	}
}

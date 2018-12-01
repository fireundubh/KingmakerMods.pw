using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Parts;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.AlwaysUnencumbered
{
	[ModifiesType]
	public class UnitPartEncumbranceNew : UnitPartEncumbrance
	{
		#region DUPLICATES
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
		#endregion

		[ModifiesMember("GetSpeedPenalty")]
		public static int mod_GetSpeedPenalty(UnitDescriptor owner, Encumbrance encumbrance)
		{
			return KingmakerPatchSettings.Cheats.AlwaysUnencumbered ? 30 : source_GetSpeedPenalty(owner, encumbrance);
		}

		[ModifiesMember("GetArmorCheckPenalty")]
		public static int mod_GetArmorCheckPenalty(UnitDescriptor owner, Encumbrance encumbrance)
		{
			return KingmakerPatchSettings.Cheats.AlwaysUnencumbered ? 0 : source_GetArmorCheckPenalty(owner, encumbrance);
		}

		[ModifiesMember("GetMaxDexterityBonus")]
		public static int? mod_GetMaxDexterityBonus(UnitDescriptor owner, Encumbrance encumbrance)
		{
			return KingmakerPatchSettings.Cheats.AlwaysUnencumbered ? null : source_GetMaxDexterityBonus(owner, encumbrance);
		}
	}
}

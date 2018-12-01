using Kingmaker.UnitLogic;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.AlwaysUnencumbered
{
	[ModifiesType("Kingmaker.UnitLogic.EncumbranceHelper")]
	public static class EncumbranceHelperNew
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("GetEncumbrance")]
		public static Encumbrance source_GetEncumbrance(UnitDescriptor unit)
		{
			throw new DeadEndException("source_GetEncumbrance");
		}
		#endregion

		[ModifiesMember("GetEncumbrance")]
		public static Encumbrance mod_GetEncumbrance(UnitDescriptor unit)
		{
			if (!KingmakerPatchSettings.Cheats.AlwaysUnencumbered)
			{
				return source_GetEncumbrance(unit);
			}

			return unit.IsPlayerFaction ? Encumbrance.Light : source_GetEncumbrance(unit);
		}
	}
}

using Kingmaker.UnitLogic.Parts;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.AlwaysUnencumbered
{
	[ModifiesType]
	public class UnitPartWearinessNew : UnitPartWeariness
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("GetFatigueHoursModifier")]
		public static float source_GetFatigueHoursModifier()
		{
			throw new DeadEndException("GetFatigueHoursModifier");
		}
		#endregion

		[ModifiesMember("GetFatigueHoursModifier")]
		public static float mod_GetFatigueHoursModifier()
		{
			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (KingmakerPatchSettings.Cheats.AlwaysUnencumbered)
			{
				return 1f;
			}

			return source_GetFatigueHoursModifier();
		}
	}
}

using Kingmaker.UnitLogic.Parts;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.AlwaysUnencumbered
{
	[ModifiesType]
	public class UnitPartWearinessNew : UnitPartWeariness
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("GetFatigueHoursModifier")]
		public static float source_GetFatigueHoursModifier()
		{
			throw new DeadEndException("GetFatigueHoursModifier");
		}

		[ModifiesMember("GetFatigueHoursModifier")]
		public static float mod_GetFatigueHoursModifier()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bAlwaysUnencumbered");
			}

			return _useMod ? 1f : source_GetFatigueHoursModifier();
		}
	}
}

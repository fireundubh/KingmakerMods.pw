using Kingmaker.Controllers.GlobalMap;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.AlwaysUnencumbered
{
	[ModifiesType]
	public class MapMovementControllerNew : MapMovementController
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("GetEncumbranceModifier")]
		public static float source_GetEncumbranceModifier()
		{
			throw new DeadEndException("source_GetEncumbranceModifier");
		}

		[ModifiesMember("GetEncumbranceModifier")]
		public static float mod_GetEncumbranceModifier()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bAlwaysUnencumbered");
			}

			return _useMod ? 1f : source_GetEncumbranceModifier();
		}
	}
}

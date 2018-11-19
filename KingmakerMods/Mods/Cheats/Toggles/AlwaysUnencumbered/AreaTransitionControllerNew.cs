using Kingmaker.UI.MapObjectOvertip;
using Kingmaker.View.MapObjects;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.AlwaysUnencumbered
{
	[ModifiesType]
	public class AreaTransitionControllerNew : AreaTransitionController
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("CanNotMove")]
		public static bool source_CanNotMove(AreaTransition areaTransition, bool silent = false)
		{
			throw new DeadEndException("source_CanNotMove");
		}

		[ModifiesMember("CanNotMove")]
		public static bool mod_CanNotMove(AreaTransition areaTransition, bool silent = false)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bAlwaysUnencumbered");
			}

			if (_useMod)
			{
				return false;
			}

			return source_CanNotMove(areaTransition, silent);
		}
	}
}

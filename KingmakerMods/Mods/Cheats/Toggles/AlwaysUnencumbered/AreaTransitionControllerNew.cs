using Kingmaker.UI.MapObjectOvertip;
using Kingmaker.View.MapObjects;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.AlwaysUnencumbered
{
	[ModifiesType]
	public class AreaTransitionControllerNew : AreaTransitionController
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("CanNotMove")]
		public static bool source_CanNotMove(AreaTransition areaTransition, bool silent = false)
		{
			throw new DeadEndException("source_CanNotMove");
		}
		#endregion

		[ModifiesMember("CanNotMove")]
		public static bool mod_CanNotMove(AreaTransition areaTransition, bool silent = false)
		{
			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (KingmakerPatchSettings.Cheats.AlwaysUnencumbered)
			{
				return false;
			}

			return source_CanNotMove(areaTransition, silent);
		}
	}
}

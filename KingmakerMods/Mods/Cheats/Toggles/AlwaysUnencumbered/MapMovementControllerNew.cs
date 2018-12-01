using Kingmaker.Controllers.GlobalMap;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.AlwaysUnencumbered
{
	[ModifiesType]
	public class MapMovementControllerNew : MapMovementController
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("GetEncumbranceModifier")]
		public static float source_GetEncumbranceModifier()
		{
			throw new DeadEndException("source_GetEncumbranceModifier");
		}
		#endregion

		[ModifiesMember("GetEncumbranceModifier")]
		public static float mod_GetEncumbranceModifier()
		{
			return KingmakerPatchSettings.Cheats.AlwaysUnencumbered ? 1f : source_GetEncumbranceModifier();
		}
	}
}

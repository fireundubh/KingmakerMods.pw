using Kingmaker.View.MapObjects;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Toggles.CampsiteCleanup
{
	[ModifiesType]
	public class CampPlaceViewNew : CampPlaceView
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("ReplaceWithInactiveCamp")]
		public MapObjectView source_ReplaceWithInactiveCamp()
		{
			throw new DeadEndException("source_ReplaceWithInactiveCamp");
		}
		#endregion

		[ModifiesMember("ReplaceWithInactiveCamp")]
		public MapObjectView mod_ReplaceWithInactiveCamp()
		{
			if (!KingmakerPatchSettings.Game.CampsiteCleanup)
			{
				return this.source_ReplaceWithInactiveCamp();
			}

			base.Destroy();

			return null;
		}
	}
}

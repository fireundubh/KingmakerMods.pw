using Kingmaker.View.MapObjects;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Toggles.CampsiteCleanup
{
	[ModifiesType]
	public class CampPlaceViewNew : CampPlaceView
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("ReplaceWithInactiveCamp")]
		public MapObjectView source_ReplaceWithInactiveCamp()
		{
			throw new DeadEndException("source_ReplaceWithInactiveCamp");
		}

		[ModifiesMember("ReplaceWithInactiveCamp")]
		public MapObjectView mod_ReplaceWithInactiveCamp()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game", "bCampsiteCleanup");
			}

			if (!_useMod)
			{
				return this.source_ReplaceWithInactiveCamp();
			}

			base.Destroy();

			return null;
		}
	}
}

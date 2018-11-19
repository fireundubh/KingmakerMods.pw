using Kingmaker.Achievements;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Toggles.Achievements
{
	[ModifiesType]
	public class AchievementsManagerNew : AchievementsManager
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("Unlock")]
		public void source_Unlock(AchievementData achData)
		{
			throw new DeadEndException("source_Unlock");
		}

		[ModifiesMember("Unlock")]
		public void mod_Unlock(AchievementData achData)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game", "bDisableAchievements");
			}

			if (_useMod)
			{
				return;
			}

			this.source_Unlock(achData);
		}
	}
}

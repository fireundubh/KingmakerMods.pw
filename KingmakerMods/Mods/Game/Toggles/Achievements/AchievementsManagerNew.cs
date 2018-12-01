using Kingmaker.Achievements;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Toggles.Achievements
{
	[ModifiesType]
	public class AchievementsManagerNew : AchievementsManager
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("Unlock")]
		public void source_Unlock(AchievementData achData)
		{
			throw new DeadEndException("source_Unlock");
		}
		#endregion

		[ModifiesMember("Unlock")]
		public void mod_Unlock(AchievementData achData)
		{
			if (KingmakerPatchSettings.Game.DisableAchievements)
			{
				return;
			}

			this.source_Unlock(achData);
		}
	}
}

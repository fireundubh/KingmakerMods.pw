using Kingmaker.Achievements;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Toggles.Achievements
{
	[ModifiesType]
	public class ActionAchievementUnlockNew : ActionAchievementUnlock
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("RunAction")]
		public void source_RunAction()
		{
			throw new DeadEndException("source_RunAction");
		}
		#endregion

		[ModifiesMember("RunAction")]
		public void mod_RunAction()
		{
			if (KingmakerPatchSettings.Game.DisableAchievements)
			{
				return;
			}

			this.source_RunAction();
		}
	}
}

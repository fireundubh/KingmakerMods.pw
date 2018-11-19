using Kingmaker.Achievements;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Toggles.Achievements
{
	[ModifiesType]
	public class ActionAchievementUnlockNew : ActionAchievementUnlock
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("RunAction")]
		public void source_RunAction()
		{
			throw new DeadEndException("source_RunAction");
		}

		[ModifiesMember("RunAction")]
		public void mod_RunAction()
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

			this.source_RunAction();
		}
	}
}

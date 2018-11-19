using Kingmaker.Designers.EventConditionActionSystem.Actions;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Toggles.PremiumRewards
{
	[ModifiesType]
	public class AddPremiumRewardNew : AddPremiumReward
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("IsDlcEnabled")]
		public static bool source_IsDlcEnabled(uint steamDlcId, ulong gogDlcId)
		{
			throw new DeadEndException("source_IsDlcEnabled");
		}

		[ModifiesMember("IsDlcEnabled")]
		public static bool mod_IsDlcEnabled(uint steamDlcId, ulong gogDlcId)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game", "bUnlockPremiumRewards");
			}

			if (!_useMod)
			{
				return source_IsDlcEnabled(steamDlcId, gogDlcId);
			}

			return true;
		}
	}
}

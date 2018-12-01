using Kingmaker.Designers.EventConditionActionSystem.Actions;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Toggles.PremiumRewards
{
	[ModifiesType]
	public class AddPremiumRewardNew : AddPremiumReward
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("IsDlcEnabled")]
		public static bool source_IsDlcEnabled(uint steamDlcId, ulong gogDlcId)
		{
			throw new DeadEndException("source_IsDlcEnabled");
		}
		#endregion

		[ModifiesMember("IsDlcEnabled")]
		public static bool mod_IsDlcEnabled(uint steamDlcId, ulong gogDlcId)
		{
			if (KingmakerPatchSettings.Game.UnlockPremiumRewards)
			{
				return true;
			}

			return source_IsDlcEnabled(steamDlcId, gogDlcId);
		}
	}
}

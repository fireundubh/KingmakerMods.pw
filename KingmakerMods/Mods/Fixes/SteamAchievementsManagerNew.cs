using Kingmaker.Achievements;
using Patchwork;
using Steamworks;

namespace KingmakerMods.Mods.Fixes
{
	/// <summary>
	/// Fixes an issue where calling Dispose() on SAM fields could generate NullReferenceExceptions
	/// This mostly happpens when achievements are disabled. The GogAchievementsManager already has this fix.
	/// </summary>
	[ModifiesType]
	public class SteamAchievementsManagerNew : SteamAchievementsManager
	{
		#region ALIASES
		[ModifiesMember("m_UserStatsReceived", ModificationScope.Nothing)]
		private Callback<UserStatsReceived_t> alias_m_UserStatsReceived;

		[ModifiesMember("m_UserStatsStored", ModificationScope.Nothing)]
		private Callback<UserStatsStored_t> alias_m_UserStatsStored;

		[ModifiesMember("m_UserAchievementStored", ModificationScope.Nothing)]
		private Callback<UserAchievementStored_t> alias_m_UserAchievementStored;
		#endregion

		[ModifiesMember("OnDisable")]
		private void mod_OnDisable()
		{
			this.alias_m_UserStatsReceived?.Dispose();
			this.alias_m_UserStatsStored?.Dispose();
			this.alias_m_UserAchievementStored?.Dispose();
		}
	}
}

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
		[ModifiesMember("m_UserStatsReceived", ModificationScope.Nothing)]
		private Callback<UserStatsReceived_t> source_m_UserStatsReceived;

		[ModifiesMember("m_UserStatsStored", ModificationScope.Nothing)]
		private Callback<UserStatsStored_t> source_m_UserStatsStored;

		[ModifiesMember("m_UserAchievementStored", ModificationScope.Nothing)]
		private Callback<UserAchievementStored_t> source_m_UserAchievementStored;

		[ModifiesMember("OnDisable")]
		private void mod_OnDisable()
		{
			this.source_m_UserStatsReceived?.Dispose();
			this.source_m_UserStatsStored?.Dispose();
			this.source_m_UserAchievementStored?.Dispose();
		}
	}
}

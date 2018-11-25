using Kingmaker.Controllers.Combat;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.InstantCooldowns
{
	[NewType]
	public static class UnitCooldownsHelper
	{
		public static void Reset(UnitCombatState.Cooldowns cooldowns)
		{
			cooldowns.Initiative = 0f;
			cooldowns.StandardAction = 0f;
			cooldowns.MoveAction = 0f;
			cooldowns.SwiftAction = 0f;
			cooldowns.AttackOfOpportunity = 0f;
		}
	}
}

using Kingmaker.UnitLogic.ActivatableAbilities;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.InfiniteAbilityUse
{
	[ModifiesType]
	public class ActivatableAbilityResourceLogicNew : ActivatableAbilityResourceLogic
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("SpendResource")]
		private void source_SpendResource(bool manual = false)
		{
			throw new DeadEndException("source_SpendResource");
		}
		#endregion

		[ModifiesMember("SpendResource")]
		private void mod_SpendResource(bool manual = false)
		{
			if (KingmakerPatchSettings.Cheats.InfiniteAbilityUse)
			{
				return;
			}

			this.source_SpendResource(manual);
		}
	}
}

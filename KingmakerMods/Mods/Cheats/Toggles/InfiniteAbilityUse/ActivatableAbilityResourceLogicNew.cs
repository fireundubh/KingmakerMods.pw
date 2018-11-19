using Kingmaker.UnitLogic.ActivatableAbilities;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.InfiniteAbilityUse
{
	[ModifiesType]
	public class ActivatableAbilityResourceLogicNew : ActivatableAbilityResourceLogic
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("SpendResource")]
		private void source_SpendResource(bool manual = false)
		{
			throw new DeadEndException("source_SpendResource");
		}

		[ModifiesMember("SpendResource")]
		private void mod_SpendResource(bool manual = false)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bInfiniteAbilityUse");
			}

			if (!_useMod)
			{
				this.source_SpendResource(manual);
			}
		}
	}
}

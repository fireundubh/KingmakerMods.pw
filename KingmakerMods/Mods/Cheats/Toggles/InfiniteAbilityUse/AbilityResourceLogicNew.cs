using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.InfiniteAbilityUse
{
	[ModifiesType]
	public class AbilityResourceLogicNew : AbilityResourceLogic
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("Spend")]
		public void source_Spend(AbilityData ability)
		{
			throw new DeadEndException("source_Spend");
		}

		[ModifiesMember("Spend")]
		public void mod_Spend(AbilityData ability)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bInfiniteAbilityUse");
			}

			UnitEntityData unit = ability.Caster.Unit;

			if (_useMod && unit?.IsPlayerFaction == true)
			{
				return;
			}

			this.source_Spend(ability);
		}
	}
}

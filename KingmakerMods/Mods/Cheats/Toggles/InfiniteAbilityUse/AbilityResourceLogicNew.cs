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
		#region DUPLICATES

		[NewMember]
		[DuplicatesBody("Spend")]
		public virtual void source_Spend(AbilityData ability)
		{
			throw new DeadEndException("source_Spend");
		}

		#endregion

		[ModifiesMember("Spend")]
		public virtual void mod_Spend(AbilityData ability)
		{
			UnitEntityData unit = ability.Caster.Unit;

			if (KingmakerPatchSettings.Cheats.InfiniteAbilityUse && unit?.IsPlayerFaction == true)
			{
				return;
			}

			this.source_Spend(ability);
		}
	}
}

using Kingmaker.Controllers.Combat;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.InstantCooldowns
{
	[ModifiesType]
	public class UnitCombatCooldownsControllerNew : UnitCombatCooldownsController
	{
		#region DUPLICATES

		[NewMember]
		[DuplicatesBody("TickOnUnit")]
		protected void source_TickOnUnit(UnitEntityData unit)
		{
			throw new DeadEndException("source_TickOnUnit");
		}

		#endregion

		[ModifiesMember("TickOnUnit")]
		protected void mod_TickOnUnit(UnitEntityData unit)
		{
			if (!KingmakerPatchSettings.Cheats.InstantCooldowns)
			{
				this.source_TickOnUnit(unit);
				return;
			}

			if (unit.IsDirectlyControllable)
			{
				UnitCombatState combatState = unit.CombatState;
				UnitCooldownsHelper.Reset(combatState.Cooldown);
				combatState.OnNewRound();
				EventBus.RaiseEvent((IUnitNewCombatRoundHandler h) => h.HandleNewCombatRound(unit));
				return;
			}

			this.source_TickOnUnit(unit);
		}
	}
}

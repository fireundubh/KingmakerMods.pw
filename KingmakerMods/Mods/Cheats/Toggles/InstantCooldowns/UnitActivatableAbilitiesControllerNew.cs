using Kingmaker.Blueprints.Facts;
using Kingmaker.Controllers.Units;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.GameModes;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Parts;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.InstantCooldowns
{
	[ModifiesType]
	public class UnitActivatableAbilitiesControllerNew : UnitActivatableAbilitiesController
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

			if (unit.ActivatableAbilities.RawFacts.Count <= 0)
			{
				return;
			}

			foreach (Fact fact in unit.ActivatableAbilities.RawFacts)
			{
				var ability = (ActivatableAbility) fact;

				if (ability.IsWaitingForTarget)
				{
					continue;
				}

				bool flag = ability.Blueprint.DeactivateIfOwnerDisabled && !ability.Owner.State.CanAct || ability.Blueprint.DeactivateIfOwnerUnconscious && !ability.Owner.State.IsConscious || Kingmaker.Game.Instance.CurrentMode == GameModeType.Rest;

				if (ability.IsRunning)
				{
					if (flag)
					{
						ability.Stop();
					}
					else
					{
						ability.TimeToNextRound -= Kingmaker.Game.Instance.TimeController.GameDeltaTime;

						if (!(ability.TimeToNextRound <= 0f))
						{
							continue;
						}

						ability.OnNewRound();

						// the actual mod
						if (ability.Owner.Unit.IsDirectlyControllable)
						{
							UnitCooldownsHelper.Reset(ability.Owner.Unit.CombatState.Cooldown);
						}

						ability.TimeToNextRound = 6f;
					}
				}
				else if (ability.IsOn && (ability.Blueprint.ActivateImmediately || ability.ReadyToStart))
				{
					if (ability.Blueprint.ActivateOnCombatStarts && !unit.IsInCombat || flag)
					{
						continue;
					}

					ability.TryStart();
				}
			}

			var unitPartActivatableAbility = unit.Get<UnitPartActivatableAbility>();
			unitPartActivatableAbility?.Update();
		}
	}
}

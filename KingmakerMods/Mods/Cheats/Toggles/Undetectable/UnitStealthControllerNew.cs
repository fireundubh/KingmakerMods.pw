using System.Linq;
using System.Reflection;
using Kingmaker.Controllers.Units;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.Undetectable
{
	[ModifiesType]
	public class UnitStealthControllerNew : UnitStealthController
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[ModifiesMember("NearEnemyPenaltyPerSecond")]
		[ToggleFieldAttributes(FieldAttributes.InitOnly)]
		public static float mod_NearEnemyPenaltyPerSecond = 1f;

		[NewMember]
		[DuplicatesBody("SpotterBreaksStealth")]
		private static bool source_SpotterBreaksStealth(UnitEntityData spotted, UnitEntityData spotting)
		{
			throw new DeadEndException("source_SpotterBreaksStealth");
		}

		[NewMember]
		[DuplicatesBody("TickUnit")]
		public void source_TickUnit(UnitEntityData unit)
		{
			throw new DeadEndException("source_TickUnit");
		}

		[ModifiesMember("TickUnit")]
		public void mod_TickUnit(UnitEntityData unit)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bUndetectable");
			}

			if (!_useMod || !unit.IsPlayerFaction)
			{
				this.source_TickUnit(unit);
				return;
			}

			UnitState unitState = unit.Descriptor.State;

			bool isInStealth = unitState.IsInStealth;
			bool shouldBeInStealth = this.ShouldBeInStealth(unit);

			if (unitState.IsInStealth)
			{
				if (shouldBeInStealth)
				{
					for (int i = 0; i < Kingmaker.Game.Instance.State.AwakeUnits.Count; i++)
					{
						UnitEntityData spotterUnit = Kingmaker.Game.Instance.State.AwakeUnits[i];

						bool hasBeenSpotted = unit.Stealth.SpottedBy.Contains(spotterUnit);
						bool consciousWithLOS = spotterUnit.Descriptor.State.IsConscious && spotterUnit.HasLOS(unit);

						if (!consciousWithLOS || hasBeenSpotted || !spotterUnit.IsEnemy(unit))
						{
							if (!hasBeenSpotted && consciousWithLOS && !spotterUnit.IsEnemy(unit) && !unit.Stealth.InAmbush)
							{
								unit.Stealth.AddSpottedBy(spotterUnit);
							}
						}
						else
						{
							float distanceToSpotter = unit.DistanceTo(spotterUnit.Position) - unit.View.Corpulence - spotterUnit.View.Corpulence;

							if (distanceToSpotter > GameConsts.MinWeaponRange.Meters - 0.1f)
							{
								continue;
							}

							if (source_SpotterBreaksStealth(unit, spotterUnit))
							{
								shouldBeInStealth = false;
								break;
							}

							if (!unit.Stealth.AddSpottedBy(spotterUnit))
							{
								continue;
							}

							EventBus.RaiseEvent<IUnitSpottedHandler>(h => h.HandleUnitSpotted(unit, spotterUnit));
						}
					}
				}

				if (!shouldBeInStealth)
				{
					unitState.IsInStealth = false;
					unit.Stealth.Clear();

					if (unit.IsPlayerFaction)
					{
						unit.Stealth.WantEnterStealth = false;
					}
				}
			}
			else if (!unitState.IsInStealth && shouldBeInStealth)
			{
				Rulebook.Trigger(new RuleEnterStealth(unit));
			}

			if (isInStealth != unitState.IsInStealth)
			{
				EventBus.RaiseEvent<IUnitStealthHandler>(h => h.HandleUnitSwitchStealthCondition(unit, unitState.IsInStealth));
			}

			unit.Stealth.ForceEnterStealth = false;
			unit.Stealth.BecameInvisibleThisFrame = false;
		}
	}
}

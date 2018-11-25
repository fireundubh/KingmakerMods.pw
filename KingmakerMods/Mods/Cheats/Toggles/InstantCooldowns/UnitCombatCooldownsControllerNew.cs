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
		#region CONFIGURATION
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		private static bool IsModReady()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bInstantCooldowns");
			}

			return _useMod;
		}
		#endregion

		#region DUPLICATED METHODS
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
			_useMod = IsModReady();

			if (_useMod && unit.IsDirectlyControllable)
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

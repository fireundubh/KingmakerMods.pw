using System;
using Kingmaker.Controllers.Units;
using Kingmaker.UnitLogic.Commands.Base;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.InstantCooldowns
{
	[ModifiesType]
	public class UnitActionControllerNew : UnitActionController
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("UpdateCooldowns")]
		public void source_UpdateCooldowns(UnitCommand command)
		{
			throw new DeadEndException("source_UpdateCooldowns");
		}

		[ModifiesMember("UpdateCooldowns")]
		public void mod_UpdateCooldowns(UnitCommand command)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bInstantCooldowns");
			}

			if (!_useMod)
			{
				this.source_UpdateCooldowns(command);
				return;
			}

			if (!command.Executor.IsInCombat || command.IsIgnoreCooldown)
			{
				return;
			}

			bool isPlayerFaction = command.Executor.IsPlayerFaction;
			float timeSinceStart = command.TimeSinceStart;

			float moveActionCooldown = isPlayerFaction ? 0f : 3f - timeSinceStart;
			float standardActionCooldown = isPlayerFaction ? 0f : 6f - timeSinceStart;
			float swiftActionCooldown = isPlayerFaction ? 0f : 6f - timeSinceStart;

			switch (command.Type)
			{
				case UnitCommand.CommandType.Free:
				case UnitCommand.CommandType.Move:
					command.Executor.CombatState.Cooldown.MoveAction = moveActionCooldown;
					break;
				case UnitCommand.CommandType.Standard:
					command.Executor.CombatState.Cooldown.StandardAction = standardActionCooldown;
					break;
				case UnitCommand.CommandType.Swift:
					command.Executor.CombatState.Cooldown.SwiftAction = swiftActionCooldown;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}

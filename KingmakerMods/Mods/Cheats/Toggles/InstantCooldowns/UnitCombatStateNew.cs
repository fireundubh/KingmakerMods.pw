using System;
using Kingmaker.Controllers.Combat;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Commands.Base;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.InstantCooldowns
{
	[ModifiesType]
	public class UnitCombatStateNew : UnitCombatState
	{
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

		[NewMember]
		[DuplicatesBody("HasCooldownForCommand")]
		public bool source_HasCooldownForCommand(UnitCommand command)
		{
			throw new DeadEndException("source_HasCooldownForCommand(UnitCommand command)");
		}

		[ModifiesMember("HasCooldownForCommand")]
		public bool mod_HasCooldownForCommand(UnitCommand command)
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				return this.source_HasCooldownForCommand(command);
			}

			if (!this.Unit.IsDirectlyControllable)
			{
				return this.source_HasCooldownForCommand(command);
			}

			return false;
		}

		[NewMember]
		[DuplicatesBody("HasCooldownForCommand")]
		public bool source_HasCooldownForCommand(UnitCommand.CommandType commandType)
		{
			throw new DeadEndException("source_HasCooldownForCommand(UnitCommand.CommandType commandType)");
		}

		[ModifiesMember("HasCooldownForCommand")]
		public bool mod_HasCooldownForCommand(UnitCommand.CommandType commandType)
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				return this.source_HasCooldownForCommand(commandType);
			}

			if (!this.Unit.IsDirectlyControllable)
			{
				return this.source_HasCooldownForCommand(commandType);
			}

			return false;
		}

		[NewMember]
		[DuplicatesBody("OnNewRound")]
		public void source_OnNewRound()
		{
			throw new DeadEndException("source_OnNewRound");
		}

		[ModifiesMember("OnNewRound")]
		public void mod_OnNewRound()
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				this.source_OnNewRound();
				return;
			}

			if (this.Unit.IsDirectlyControllable)
			{
				this.Cooldown.Initiative = 0f;
				this.Cooldown.StandardAction = 0f;
				this.Cooldown.MoveAction = 0f;
				this.Cooldown.SwiftAction = 0f;
				this.Cooldown.AttackOfOpportunity = 0f;
			}

			this.source_OnNewRound();
		}



		[Obsolete]
		public UnitCombatStateNew(UnitEntityData unit) : base(unit)
		{
		}
	}
}

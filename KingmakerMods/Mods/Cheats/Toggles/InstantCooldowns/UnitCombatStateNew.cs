using System;
using Kingmaker.Controllers.Combat;
using Kingmaker.EntitySystem.Entities;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.InstantCooldowns
{
	[ModifiesType]
	public class UnitCombatStateNew : UnitCombatState
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
		[DuplicatesBody("OnNewRound")]
		public void source_OnNewRound()
		{
			throw new DeadEndException("source_OnNewRound");
		}
		#endregion

		[ModifiesMember("OnNewRound")]
		public void mod_OnNewRound()
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				this.source_OnNewRound();
				return;
			}

			if (_useMod && this.Unit.IsDirectlyControllable)
			{
				// this.Cooldown.Clear()
				UnitCooldownsHelper.Reset(this.Cooldown);
			}

			this.source_OnNewRound();
		}

		[Obsolete]
		public UnitCombatStateNew(UnitEntityData unit) : base(unit)
		{
		}
	}
}

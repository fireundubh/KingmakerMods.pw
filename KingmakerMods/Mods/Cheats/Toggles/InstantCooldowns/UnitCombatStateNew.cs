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
		#region DUPLICATES
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
			if (!KingmakerPatchSettings.Cheats.InstantCooldowns)
			{
				this.source_OnNewRound();
				return;
			}

			if (this.Unit.IsDirectlyControllable)
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

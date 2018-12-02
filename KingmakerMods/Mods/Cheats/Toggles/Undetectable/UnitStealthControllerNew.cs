using Kingmaker.Controllers.Units;
using Kingmaker.EntitySystem.Entities;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.Undetectable
{
	[ModifiesType]
	public class UnitStealthControllerNew : UnitStealthController
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("SpotterBreaksStealth")]
		private static bool source_SpotterBreaksStealth(UnitEntityData spotted, UnitEntityData spotting)
		{
			throw new DeadEndException("source_SpotterBreaksStealth");
		}

		[NewMember]
		[DuplicatesBody("HandleUnitMakeOffensiveAction")]
		public void source_HandleUnitMakeOffensiveAction(UnitEntityData unit, UnitEntityData target)
		{
			throw new DeadEndException("source_HandleUnitMakeOffensiveAction");
		}
		#endregion

		[ModifiesMember("SpotterBreaksStealth")]
		private static bool mod_SpotterBreaksStealth(UnitEntityData spotted, UnitEntityData spotting)
		{
			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (KingmakerPatchSettings.Cheats.Undetectable)
			{
				return false;
			}

			return source_SpotterBreaksStealth(spotted, spotting);
		}

		[ModifiesMember("HandleUnitMakeOffensiveAction")]
		public void mod_HandleUnitMakeOffensiveAction(UnitEntityData unit, UnitEntityData target)
		{
			if (KingmakerPatchSettings.Cheats.UndetectableStealthAttacks)
			{
				return;
			}

			this.source_HandleUnitMakeOffensiveAction(unit, target);
		}
	}
}

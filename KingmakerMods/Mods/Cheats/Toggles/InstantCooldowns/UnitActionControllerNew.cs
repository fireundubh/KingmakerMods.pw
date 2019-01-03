using Kingmaker.Controllers.Units;
using Kingmaker.UnitLogic.Commands.Base;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.InstantCooldowns
{
	[ModifiesType]
	public class UnitActionControllerNew : UnitActionController
	{
		#region DUPLICATES

		[NewMember]
		[DuplicatesBody("UpdateCooldowns")]
		public void source_UpdateCooldowns(UnitCommand command)
		{
			throw new DeadEndException("source_UpdateCooldowns");
		}

		#endregion

		[ModifiesMember("UpdateCooldowns")]
		public void mod_UpdateCooldowns(UnitCommand command)
		{
			if (!KingmakerPatchSettings.Cheats.InstantCooldowns)
			{
				this.source_UpdateCooldowns(command);
				return;
			}

			if (command.Executor.IsDirectlyControllable)
			{
				return;
			}

			this.source_UpdateCooldowns(command);
		}
	}
}

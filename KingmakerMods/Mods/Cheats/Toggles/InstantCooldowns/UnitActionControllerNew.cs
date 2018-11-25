using Kingmaker.Controllers.Units;
using Kingmaker.UnitLogic.Commands.Base;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.InstantCooldowns
{
	[ModifiesType]
	public class UnitActionControllerNew : UnitActionController
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
		[DuplicatesBody("UpdateCooldowns")]
		public void source_UpdateCooldowns(UnitCommand command)
		{
			throw new DeadEndException("source_UpdateCooldowns");
		}
		#endregion

		[ModifiesMember("UpdateCooldowns")]
		public void mod_UpdateCooldowns(UnitCommand command)
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				this.source_UpdateCooldowns(command);
				return;
			}

			if (_useMod && command.Executor.IsDirectlyControllable)
			{
				return;
			}

			this.source_UpdateCooldowns(command);
		}
	}
}

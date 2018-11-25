using System;
using JetBrains.Annotations;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.Utility;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.InstantCooldowns
{
	[ModifiesType]
	public abstract class UnitCommandNew : UnitCommand
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
		[DuplicatesBody("get_IsIgnoreCooldown")]
		public bool source_get_IsIgnoreCooldown()
		{
			throw new DeadEndException("source_get_IsIgnoreCooldown");
		}
		#endregion

		[ModifiesMember("IsIgnoreCooldown")]
		public bool mod_IsIgnoreCooldown
		{
			[ModifiesMember("get_IsIgnoreCooldown")]
			get
			{
				_useMod = IsModReady();

				if (_useMod && this.Executor.IsDirectlyControllable)
				{
					return true;
				}

				return this.source_get_IsIgnoreCooldown();
			}
		}

		[Obsolete]
		protected UnitCommandNew(CommandType type, [CanBeNull] TargetWrapper target = null) : base(type, target)
		{
		}
	}
}

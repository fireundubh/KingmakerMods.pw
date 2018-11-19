using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Toggles.PremiumRewards
{
	[ModifiesType]
	public class BlueprintPortraitNew : BlueprintPortrait
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("get_PlayerHasAccess")]
		public bool source_get_PlayerHasAccess()
		{
			throw new DeadEndException("source_get_PlayerHasAccess");
		}

		[ModifiesMember("PlayerHasAccess")]
		public bool PlayerHasAccessNew
		{
			[ModifiesMember("get_PlayerHasAccess")]
			get
			{
				if (!_cfgInit)
				{
					_cfgInit = true;
					_useMod = UserConfig.Parser.GetValueAsBool("Game", "bUnlockPremiumPortraits");
				}

				if (_useMod)
				{
					return true;
				}

				return this.source_get_PlayerHasAccess();
			}
		}
	}
}

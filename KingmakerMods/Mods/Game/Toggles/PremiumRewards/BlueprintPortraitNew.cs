using Kingmaker.Blueprints;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Toggles.PremiumRewards
{
	[ModifiesType]
	public class BlueprintPortraitNew : BlueprintPortrait
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("get_PlayerHasAccess")]
		public bool source_get_PlayerHasAccess()
		{
			throw new DeadEndException("source_get_PlayerHasAccess");
		}
		#endregion

		[ModifiesMember("PlayerHasAccess")]
		public bool mod_PlayerHasAccess
		{
			[ModifiesMember("get_PlayerHasAccess")]
			get
			{
				// ReSharper disable once ConvertIfStatementToReturnStatement
				if (KingmakerPatchSettings.Game.UnlockPremiumPortraits)
				{
					return true;
				}

				return this.source_get_PlayerHasAccess();
			}
		}
	}
}

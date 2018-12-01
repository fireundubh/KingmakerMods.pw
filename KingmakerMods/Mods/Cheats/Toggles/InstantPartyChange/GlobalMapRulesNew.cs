using Kingmaker.Globalmap;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.InstantPartyChange
{
	[ModifiesType]
	public class GlobalMapRulesNew : GlobalMapRules
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("ChangePartyOnMap")]
		public void source_ChangePartyOnMap()
		{
			throw new DeadEndException("source_ChangePartyOnMap");
		}
		#endregion

		[ModifiesMember("ChangePartyOnMap")]
		public void mod_ChangePartyOnMap()
		{
			if (KingmakerPatchSettings.Cheats.InstantPartyChange)
			{
				return;
			}

			this.source_ChangePartyOnMap();
		}
	}
}

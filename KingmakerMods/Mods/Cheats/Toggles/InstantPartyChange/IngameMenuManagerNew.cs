using Kingmaker.UI.IngameMenu;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.InstantPartyChange
{
	[ModifiesType]
	public class IngameMenuManagerNew : IngameMenuManager
	{
		#region ALIASES
		[ModifiesMember("StartChangedPartyOnGlobalMap", ModificationScope.Nothing)]
		private void alias_StartChangedPartyOnGlobalMap()
		{
			throw new DeadEndException("source_StartChangedPartyOnGlobalMap");
		}
		#endregion

		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("OpenGroupManager")]
		public void source_OpenGroupManager()
		{
			throw new DeadEndException("source_OpenGroupManager");
		}
		#endregion

		[ModifiesMember("OpenGroupManager")]
		public void mod_OpenGroupManager()
		{
			if (!KingmakerPatchSettings.Cheats.InstantPartyChange)
			{
				this.source_OpenGroupManager();
				return;
			}

			this.alias_StartChangedPartyOnGlobalMap();
		}
	}
}

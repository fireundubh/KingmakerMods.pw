using Kingmaker.Blueprints.Root.Strings.GameLog;
using Kingmaker.UI.Log;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.AlwaysUnencumbered
{
	[ModifiesType]
	public class BattleLogManagerNew : BattleLogManager
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("ChangePartyEncumbrance")]
		public void source_ChangePartyEncumbrance()
		{
			throw new DeadEndException("source_ChangePartyEncumbrance");
		}
		#endregion

		[ModifiesMember("ChangePartyEncumbrance")]
		public void mod_ChangePartyEncumbrance()
		{
			if (KingmakerPatchSettings.Cheats.AlwaysUnencumbered)
			{
				this.LogView.AddLogEntry(GameLogStrings.Instance.PartyEncumbranceLight.GetData());
				return;
			}

			this.source_ChangePartyEncumbrance();
		}
	}
}

using Kingmaker.Blueprints.Root.Strings.GameLog;
using Kingmaker.UI.Log;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.AlwaysUnencumbered
{
	[ModifiesType]
	public class BattleLogManagerNew : BattleLogManager
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[ModifiesMember("LogStrings", ModificationScope.Nothing)]
		private static GameLogStrings mod_LogStrings
		{
			[ModifiesMember("get_LogStrings", ModificationScope.Nothing)]
			get { return GameLogStrings.Instance; }
		}

		[NewMember]
		[DuplicatesBody("ChangePartyEncumbrance")]
		public void source_ChangePartyEncumbrance()
		{
			throw new DeadEndException("source_ChangePartyEncumbrance");
		}

		[ModifiesMember("ChangePartyEncumbrance")]
		public void mod_ChangePartyEncumbrance()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bAlwaysUnencumbered");
			}

			if (_useMod)
			{
				this.LogView.AddLogEntry(BattleLogManagerNew.mod_LogStrings.PartyEncumbranceLight.GetData());
				return;
			}

			this.source_ChangePartyEncumbrance();
		}
	}
}

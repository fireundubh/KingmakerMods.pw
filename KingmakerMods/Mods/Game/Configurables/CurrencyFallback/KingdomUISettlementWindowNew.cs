using Kingmaker.Kingdom.Blueprints;
using Kingmaker.UI.Kingdom;
using Kingmaker.Utility;
using KingmakerMods.Helpers;
using Patchwork;
using TMPro;
using UnityEngine.UI;

namespace KingmakerMods.Mods.Game.Configurables.CurrencyFallback
{
	[ModifiesType]
	public class KingdomUISettlementWindowNew : KingdomUISettlementWindow
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[ModifiesMember("m_InputNameField", ModificationScope.Nothing)]
		private TMP_InputField source_m_InputNameField;

		[ModifiesMember("m_Build", ModificationScope.Nothing)]
		private Button source_m_Build;

		[NewMember]
		[DuplicatesBody("UpdateBuildEnabled")]
		public void source_UpdateBuildEnabled()
		{
			throw new DeadEndException("source_UpdateBuildEnabled");
		}

		[ModifiesMember("UpdateBuildEnabled")]
		public void mod_UpdateBuildEnabled()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game.KingdomEvents", "bCurrencyFallback");
			}

			if (!_useMod)
			{
				this.source_UpdateBuildEnabled();
				return;
			}

			bool canAfford = KingdomCurrencyFallback.CanSpend(KingdomRoot.Instance.SettlementBPCost);

			if (canAfford)
			{
				canAfford = !this.source_m_InputNameField.text.Empty();
			}

			this.source_m_Build.interactable = canAfford;
		}
	}
}

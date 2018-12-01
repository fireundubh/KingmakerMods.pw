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
		#region ALIASES
		[ModifiesMember("m_InputNameField", ModificationScope.Nothing)]
		private TMP_InputField alias_m_InputNameField;

		[ModifiesMember("m_Build", ModificationScope.Nothing)]
		private Button alias_m_Build;
		#endregion

		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("UpdateBuildEnabled")]
		public void source_UpdateBuildEnabled()
		{
			throw new DeadEndException("source_UpdateBuildEnabled");
		}
		#endregion

		[ModifiesMember("UpdateBuildEnabled")]
		public void mod_UpdateBuildEnabled()
		{
			if (!KingmakerPatchSettings.CurrencyFallback.Enabled)
			{
				this.source_UpdateBuildEnabled();
				return;
			}

			bool canAfford = KingdomCurrencyFallback.CanSpend(KingdomRoot.Instance.SettlementBPCost);

			if (canAfford)
			{
				canAfford = !this.alias_m_InputNameField.text.Empty();
			}

			this.alias_m_Build.interactable = canAfford;
		}
	}
}

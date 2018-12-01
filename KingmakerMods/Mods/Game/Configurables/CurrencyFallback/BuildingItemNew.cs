using System;
using System.Diagnostics.CodeAnalysis;
using Kingmaker.Kingdom;
using Kingmaker.Kingdom.Settlements;
using Kingmaker.Localization;
using KingmakerMods.Helpers;
using KingmakerMods.Mods.Game.Configurables.Localization;
using Patchwork;
using TMPro;
using UnityEngine.UI;

namespace KingmakerMods.Mods.Game.Configurables.CurrencyFallback
{
	[ModifiesType]
	public class BuildingItemNew : Kingmaker.UI.Settlement.BuildingItem
	{
		[ModifiesType("Kingmaker.UI.Settlement.BuildingItem/RequiredStaff")]
		[SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
		private class RequiredStaffNew
		{
			[ModifiesMember("Slots", ModificationScope.Nothing)]
			public Image alias_Slots;

			[ModifiesMember("Cost", ModificationScope.Nothing)]
			public TextMeshProUGUI alias_Cost;

			[ModifiesMember("DiscountLayer", ModificationScope.Nothing)]
			public Image alias_DiscountLayer;

			[NewMember]
			[DuplicatesBody("Initialize")]
			public void source_Initialize(BlueprintSettlementBuilding building, SettlementBuilding settlementBuilding, SettlementState settlement)
			{
				throw new DeadEndException("source_Initialize");
			}

			[ModifiesMember("Initialize")]
			public void mod_Initialize(BlueprintSettlementBuilding building, SettlementBuilding settlementBuilding, SettlementState settlement)
			{
				if (!KingmakerPatchSettings.CurrencyFallback.Enabled)
				{
					this.source_Initialize(building, settlementBuilding, settlement);
					return;
				}

				string costFormat = LocalizationManagerNew.LoadString("9191fbc8-23ac-4bd4-8167-28de8d418122");
				string costSplitFormat = LocalizationManagerNew.LoadString("da676fed-170f-4099-8b09-ba516d632dd7");

				this.alias_Slots.sprite = KingdomUIRoot.Instance.Settlement.GetSlotSprite(building.SlotCount);

				if (settlementBuilding == null)
				{
					int actualCost = settlement.GetActualCost(building, out bool isDiscounted);
					this.alias_DiscountLayer.gameObject.SetActive(actualCost == 0 || isDiscounted);

					if (actualCost == 0)
					{
						this.alias_Cost.text = string.Format(costFormat, KingdomUIRoot.Instance.Texts.BuildFreeCost);
					}
					else
					{
						Tuple<int, int> costSplit = KingdomCurrencyFallback.SplitCost(actualCost);

						LocalizedString format = isDiscounted ? KingdomUIRoot.Instance.Texts.BuildPointsDiscountFormat : KingdomUIRoot.Instance.Texts.BuildPointsFormat;

						if (costSplit.Item2 == 0)
						{
							this.alias_Cost.text = string.Format(costFormat, string.Format(format, costSplit.Item1));
						}
						else
						{
							this.alias_Cost.text = string.Format(costSplitFormat, string.Format(format, costSplit.Item1), costSplit.Item2);
						}
					}
				}
				else
				{
					this.alias_DiscountLayer.gameObject.SetActive(false);
					this.alias_Cost.text = string.Format(costFormat, string.Format(KingdomUIRoot.Instance.Texts.BuildPointsFormat, settlementBuilding.Owner.GetSellPrice(building)));
				}

				this.alias_SetColor(building, settlementBuilding, settlement);
			}

			[ModifiesMember("SetColor", ModificationScope.Nothing)]
			private void alias_SetColor(BlueprintSettlementBuilding building, SettlementBuilding settlementBuilding, SettlementState settlement)
			{
				throw new DeadEndException("source_SetColor");
			}
		}
	}
}

using System;
using Kingmaker.Kingdom;
using Kingmaker.Kingdom.Settlements;
using Kingmaker.Localization;
using KingmakerMods.Helpers;
using KingmakerMods.Mods.Game.Configurables.Localization;
using KingmakerMods.UserConfig;
using Patchwork;
using TMPro;
using UnityEngine.UI;

namespace KingmakerMods.Mods.Game.Configurables.CurrencyFallback
{
	[ModifiesType]
	public class BuildingItemNew : Kingmaker.UI.Settlement.BuildingItem
	{
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
				_useMod = Parser.GetValueAsBool("Game.KingdomEvents", "bCurrencyFallback");
			}

			return _useMod;
		}

		[ModifiesType("Kingmaker.UI.Settlement.BuildingItem/RequiredStaff")]
		private class RequiredStaffNew
		{
			[ModifiesMember("Slots", ModificationScope.Nothing)]
			public Image source_Slots;

			[ModifiesMember("Cost", ModificationScope.Nothing)]
			public TextMeshProUGUI source_Cost;

			[ModifiesMember("DiscountLayer", ModificationScope.Nothing)]
			public Image source_DiscountLayer;

			[NewMember]
			[DuplicatesBody("Initialize")]
			public void source_Initialize(BlueprintSettlementBuilding building, SettlementBuilding settlementBuilding, SettlementState settlement)
			{
				throw new DeadEndException("source_Initialize");
			}

			[ModifiesMember("Initialize")]
			public void mod_Initialize(BlueprintSettlementBuilding building, SettlementBuilding settlementBuilding, SettlementState settlement)
			{
				_useMod = IsModReady();

				if (!_useMod)
				{
					this.source_Initialize(building, settlementBuilding, settlement);
					return;
				}

				string costFormat = LocalizationManagerNew.LoadString("9191fbc8-23ac-4bd4-8167-28de8d418122");
				string costSplitFormat = LocalizationManagerNew.LoadString("da676fed-170f-4099-8b09-ba516d632dd7");

				this.source_Slots.sprite = KingdomUIRoot.Instance.Settlement.GetSlotSprite(building.SlotCount);

				if (settlementBuilding == null)
				{
					int actualCost = settlement.GetActualCost(building, out bool isDiscounted);
					this.source_DiscountLayer.gameObject.SetActive(actualCost == 0 || isDiscounted);

					if (actualCost == 0)
					{
						this.source_Cost.text = string.Format(costFormat, KingdomUIRoot.Instance.Texts.BuildFreeCost);
					}
					else
					{
						Tuple<int, int> costSplit = KingdomCurrencyFallback.SplitCost(actualCost);

						LocalizedString format = isDiscounted ? KingdomUIRoot.Instance.Texts.BuildPointsDiscountFormat : KingdomUIRoot.Instance.Texts.BuildPointsFormat;

						if (costSplit.Item2 == 0)
						{
							this.source_Cost.text = string.Format(costFormat, string.Format(format, costSplit.Item1));
						}
						else
						{
							this.source_Cost.text = string.Format(costSplitFormat, string.Format(format, costSplit.Item1), costSplit.Item2);
						}
					}
				}
				else
				{
					this.source_DiscountLayer.gameObject.SetActive(false);
					this.source_Cost.text = string.Format(costFormat, string.Format(KingdomUIRoot.Instance.Texts.BuildPointsFormat, settlementBuilding.Owner.GetSellPrice(building)));
				}

				this.source_SetColor(building, settlementBuilding, settlement);
			}

			[ModifiesMember("SetColor", ModificationScope.Nothing)]
			private void source_SetColor(BlueprintSettlementBuilding building, SettlementBuilding settlementBuilding, SettlementState settlement)
			{
				throw new DeadEndException("source_SetColor");
			}
		}
	}
}

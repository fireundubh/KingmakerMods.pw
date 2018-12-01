using System.Collections.Generic;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Parts;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Cheats.Configurables.VendorLogic
{
	[ModifiesType]
	public class VendorLogicNew : Kingmaker.Items.VendorLogic
	{
		#region ALIASES
		[ModifiesMember("m_Vendor", ModificationScope.Nothing)]
		private UnitEntityData alias_m_Vendor;

		[ModifiesMember("m_VendorPrices", ModificationScope.Nothing)]
		private ChangeVendorPrices alias_m_VendorPrices;
		#endregion

		[NewMember]
		private static int mod_GetHighestPersuasionScore()
		{
			var result = 0;

			bool isCapital = Kingmaker.Game.Instance.CurrentlyLoadedArea.IsCapital;

			List<UnitEntityData> partyMembers = isCapital ? Kingmaker.Game.Instance.Player.AllCharacters : Kingmaker.Game.Instance.Player.Party;

			foreach (UnitEntityData partyMember in partyMembers)
			{
				result = Mathf.Max(partyMember.Stats.SkillPersuasion.ModifiedValue, result);
			}

			return result > KingmakerPatchSettings.BuyLowSellHigh.PersuasionCap ? KingmakerPatchSettings.BuyLowSellHigh.PersuasionCap : result;
		}

		[NewMember]
		private static double mod_CalculatePersuasionEffect(bool type)
		{
			int divisor = type ? KingmakerPatchSettings.BuyLowSellHigh.SellDivisor : KingmakerPatchSettings.BuyLowSellHigh.BuyDivisor;

			int highestPersuasionScore = mod_GetHighestPersuasionScore();

			return highestPersuasionScore / (double) divisor;
		}

		[ModifiesMember("GetItemSellPrice")]
		public long mod_GetItemSellPrice(ItemEntity item)
		{
			double modifiedPrice = item.Cost * Kingmaker.Game.Instance.BlueprintRoot.Vendors.SellModifier;

			if (this.IsTrading)
			{
				modifiedPrice *= this.alias_m_Vendor.Ensure<UnitPartVendor>().SellPriceModifier;
			}

			// ReSharper disable once InvertIf
			if (KingmakerPatchSettings.BuyLowSellHigh.Enabled)
			{
				double persuasionEffect = 1f + mod_CalculatePersuasionEffect(true);

				modifiedPrice *= persuasionEffect;
			}

			return Mathf.RoundToInt((long) modifiedPrice);
		}

		[ModifiesMember("GetItemBuyPrice")]
		public long mod_GetItemBuyPrice(ItemEntity item)
		{
			if (!this.IsTrading)
			{
				return item.Cost;
			}

			double modifiedCost;

			if (this.alias_m_VendorPrices)
			{
				modifiedCost = this.alias_m_VendorPrices.GetCost(item.Blueprint);
			}
			else
			{
				modifiedCost = item.Cost;
			}

			modifiedCost *= this.alias_m_Vendor.Ensure<UnitPartVendor>().PriceModifier;

			// ReSharper disable once InvertIf
			if (KingmakerPatchSettings.BuyLowSellHigh.Enabled)
			{
				double persuasionEffect = 1f - mod_CalculatePersuasionEffect(false);

				modifiedCost *= persuasionEffect;
			}

			return Mathf.RoundToInt((long) modifiedCost);
		}
	}
}

using System.Collections.Generic;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Parts;
using KingmakerMods.Helpers;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Cheats.Configurables.VendorLogic
{
	[ModifiesType]
	public class VendorLogicNew : Kingmaker.Items.VendorLogic
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		private static int _persuasionCap;

		[NewMember]
		private static int _sellDivisor;

		[NewMember]
		private static int _buyDivisor;

		[ModifiesMember("m_Vendor", ModificationScope.Nothing)]
		private UnitEntityData mod_m_Vendor;

		[ModifiesMember("m_VendorPrices", ModificationScope.Nothing)]
		private ChangeVendorPrices mod_m_VendorPrices;

		[NewMember]
		[DuplicatesBody("BeginTrading")]
		public void source_BeginTrading(UnitEntityData vendor)
		{
			throw new DeadEndException("source_BeginTrading");
		}

		[ModifiesMember("BeginTrading")]
		public void mod_BeginTrading(UnitEntityData vendor)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game.VendorLogic", "bBuyLowSellHigh");
				_persuasionCap = UserConfig.Parser.GetValueAsInt("Game.VendorLogic", "iPersuasionCap");
				_sellDivisor = UserConfig.Parser.GetValueAsInt("Game.VendorLogic", "iSellDivisor");
				_buyDivisor = UserConfig.Parser.GetValueAsInt("Game.VendorLogic", "iBuyDivisor");
			}

			this.source_BeginTrading(vendor);
		}

		[NewMember]
		private static int mod_GetHighestPersuasionScore()
		{
			int result = 0;

			bool isCapital = Kingmaker.Game.Instance.CurrentlyLoadedArea.IsCapital;

			List<UnitEntityData> partyMembers = isCapital ? Kingmaker.Game.Instance.Player.AllCharacters : Kingmaker.Game.Instance.Player.Party;

			foreach (UnitEntityData partyMember in partyMembers)
			{
				result = Mathf.Max(partyMember.Stats.SkillPersuasion.ModifiedValue, result);
			}

			return result > _persuasionCap ? _persuasionCap : result;
		}

		[NewMember]
		private static double mod_CalculatePersuasionEffect(bool type)
		{
			int divisor = type ? _sellDivisor : _buyDivisor;

			if (divisor == 0)
			{
				divisor = 40;
			}

			int highestPersuasionScore = mod_GetHighestPersuasionScore();

			return highestPersuasionScore / (double) divisor;
		}

		[ModifiesMember("GetItemSellPrice")]
		public long mod_GetItemSellPrice(ItemEntity item)
		{
			double modifiedPrice = item.Cost * Kingmaker.Game.Instance.BlueprintRoot.Vendors.SellModifier;

			if (this.IsTrading)
			{
				modifiedPrice *= this.mod_m_Vendor.Ensure<UnitPartVendor>().SellPriceModifier;
			}

			if (_useMod)
			{
				double persuasionEffect = 1f + mod_CalculatePersuasionEffect(true);

				modifiedPrice *= persuasionEffect;
			}

			return (long) Mathf.RoundToInt((long) modifiedPrice);
		}

		[ModifiesMember("GetItemBuyPrice")]
		public long mod_GetItemBuyPrice(ItemEntity item)
		{
			if (!this.IsTrading)
			{
				return item.Cost;
			}

			double modifiedCost;

			if (this.mod_m_VendorPrices)
			{
				modifiedCost = this.mod_m_VendorPrices.GetCost(item.Blueprint);
			}
			else
			{
				modifiedCost = item.Cost;
			}

			modifiedCost *= this.mod_m_Vendor.Ensure<UnitPartVendor>().PriceModifier;

			if (_useMod)
			{
				double persuasionEffect = 1f - mod_CalculatePersuasionEffect(false);

				modifiedCost *= persuasionEffect;
			}

			return (long) Mathf.RoundToInt((long) modifiedCost);
		}
	}
}

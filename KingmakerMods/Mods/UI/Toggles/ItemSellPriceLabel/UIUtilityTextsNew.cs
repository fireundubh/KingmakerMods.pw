using Kingmaker.Blueprints.Root;
using Kingmaker.Items;
using KingmakerMods.Helpers;
using KingmakerMods.Mods.Game.Configurables.Localization;
using Patchwork;

namespace KingmakerMods.Mods.UI.Toggles.ItemSellPriceLabel
{
	[ModifiesType("Kingmaker.UI.Common.UIUtilityTexts")]
	public static class UIUtilityTextsNew
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
				_useMod = UserConfig.Parser.GetValueAsBool("UI", "bAddLabelToSellCost");
			}

			return _useMod;
		}

		[NewMember]
		[DuplicatesBody("GetItemCost")]
		public static string source_GetItemCost(ItemEntity item)
		{
			throw new DeadEndException("source_GetItemCost");
		}

		[ModifiesMember("GetItemCost")]
		public static string mod_GetItemCost(ItemEntity item)
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				return source_GetItemCost(item);
			}

			if (item.IsIdentified)
			{
				string sellForLabel = LocalizationManagerNew.LoadString("2f09e1be-86de-460d-96be-8a8ca72d456f");
				return string.Format(sellForLabel, Kingmaker.Game.Instance.Vendor.GetItemSellPrice(item) * item.Count);
			}

			return LocalizedTexts.Instance.UserInterfacesText.Tooltips.Unidentified;
		}
	}
}

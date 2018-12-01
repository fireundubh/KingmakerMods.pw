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
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("GetItemCost")]
		public static string source_GetItemCost(ItemEntity item)
		{
			throw new DeadEndException("source_GetItemCost");
		}
		#endregion

		[ModifiesMember("GetItemCost")]
		public static string mod_GetItemCost(ItemEntity item)
		{
			if (!KingmakerPatchSettings.UI.AddLabelToSellCost)
			{
				return source_GetItemCost(item);
			}

			if (!item.IsIdentified)
			{
				return LocalizedTexts.Instance.UserInterfacesText.Tooltips.Unidentified;
			}

			string sellForLabel = LocalizationManagerNew.LoadString("2f09e1be-86de-460d-96be-8a8ca72d456f");

			return string.Format(sellForLabel, Kingmaker.Game.Instance.Vendor.GetItemSellPrice(item) * item.Count);
		}
	}
}

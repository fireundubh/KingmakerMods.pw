using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Components;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UI.Common;
using Kingmaker.UI.ServiceWindow;
using KingmakerMods.Helpers;
using KingmakerMods.Mods.Cheats.Toggles.AllowSpontaneousCastersToCopyScrolls;
using Patchwork;

namespace KingmakerMods.Mods.UI.Toggles.CopyScrollBookName
{
	[ModifiesType]
	public class ItemSlotNew : ItemSlot
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("get_ScrollContent")]
		public string source_get_ScrollContent()
		{
			throw new DeadEndException("source_get_ScrollContent");
		}
		#endregion

		[ModifiesMember("ScrollContent")]
		public string mod_ScrollContent
		{
			[ModifiesMember("get_ScrollContent")]
			get
			{
				if (!KingmakerPatchSettings.UI.AddSpellbookNameToCopyScrollAction)
				{
					return this.source_get_ScrollContent();
				}

				UnitEntityData currentCharacter = UIUtility.GetCurrentCharacter();

				var component = this.Item.Blueprint.GetComponent<CopyItem>();

				string actionName = component?.GetActionName(currentCharacter) ?? string.Empty;

				if (!string.IsNullOrWhiteSpace(actionName))
				{
					actionName = CopyScrollNew.mod_GetSpellbookActionName(actionName, this.Item, currentCharacter);
				}

				return actionName;
			}
		}
	}
}

﻿using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Components;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UI.Common;
using Kingmaker.UI.Tooltip;
using KingmakerMods.Helpers;
using KingmakerMods.Mods.Cheats.Toggles.AllowSpontaneousCastersToCopyScrolls;
using Patchwork;

namespace KingmakerMods.Mods.UI.Toggles.CopyScrollBookName
{
	[ModifiesType]
	public class DescriptionTemplatesItemNew : DescriptionTemplatesItem
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
				_useMod = UserConfig.Parser.GetValueAsBool("UI", "bAddSpellbookNameToCopyScrollAction");
			}

			return _useMod;
		}

		[NewMember]
		[DuplicatesBody("CopyButton")]
		public void source_CopyButton(DescriptionBricksBox box, TooltipData data, bool isTooltip)
		{
			throw new DeadEndException("source_CopyButton");
		}

		[ModifiesMember("CopyButton")]
		public void mod_CopyButton(DescriptionBricksBox box, TooltipData data, bool isTooltip)
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				this.source_CopyButton(box, data, isTooltip);
				return;
			}

			if (data.Item == null || data.Item.Blueprint.GetComponent<CopyItem>() == null)
			{
				return;
			}

			UnitEntityData unit = UIUtility.GetCurrentCharacter();
			var component = data.Item.Blueprint.GetComponent<CopyItem>();

			if (component == null || !component.CanCopy(data.Item, unit) || !Kingmaker.Game.Instance.Player.Inventory.Contains(data.Item.Blueprint) || isTooltip)
			{
				return;
			}

			DescriptionBrick descriptionBrick = box.Add(DescriptionTemplatesBase.Bricks.GreenButton);

			string actionName = component.GetActionName(unit);
			actionName = CopyScrollNew.mod_GetSpellbookActionName(actionName, data.Item, unit);

			descriptionBrick.SetText(actionName);
			descriptionBrick.SetButtonAction(() => Kingmaker.Game.Instance.UI.DescriptionController.CopyItem(data.Item, unit));
		}
	}
}

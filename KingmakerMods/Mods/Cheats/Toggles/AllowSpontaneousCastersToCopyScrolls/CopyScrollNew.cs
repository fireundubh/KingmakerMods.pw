using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Kingmaker.Blueprints.Items.Components;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using KingmakerMods.Helpers;
using KingmakerMods.Mods.Game.Configurables.Localization;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.AllowSpontaneousCastersToCopyScrolls
{
	[ModifiesType]
	public class CopyScrollNew : CopyScroll
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
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bAllowSpontaneousCastersToCopyScrolls");
			}

			return _useMod;
		}

		[NewMember]
		public static string mod_GetSpellbookActionName(string actionName, ItemEntity item, UnitEntityData unit)
		{
			if (actionName != LocalizedTexts.Instance.Items.CopyScroll)
			{
				return actionName;
			}

			BlueprintAbility spell = CopyScrollNew.mod_ExtractSpell(item);

			if (spell == null)
			{
				return actionName;
			}

			List<Spellbook> spellbooks = unit.Descriptor.Spellbooks.Where(x => x.Blueprint.SpellList.Contains(spell)).ToList();

			int count = spellbooks.Count;

			if (count <= 0)
			{
				return actionName;
			}

			string actionFormat = LocalizationManagerNew.LoadString("76e0a02a-e6f4-4422-b14d-d958028a7cbd");

			return string.Format(actionFormat, actionName, count == 1 ? spellbooks.First().Blueprint.Name : "Multiple");
		}

		[NewMember]
		public static BlueprintAbility mod_ExtractSpell([NotNull] ItemEntity item)
		{
			ItemEntityUsable itemEntityUsable = item as ItemEntityUsable;

			if (itemEntityUsable?.Blueprint.Type != UsableItemType.Scroll)
			{
				return null;
			}

			return itemEntityUsable.Blueprint.Ability.Parent ? itemEntityUsable.Blueprint.Ability.Parent : itemEntityUsable.Blueprint.Ability;
		}

		[NewMember]
		[DuplicatesBody("CanCopySpell")]
		private static bool source_CanCopySpell([NotNull] BlueprintAbility spell, [NotNull] Spellbook spellbook)
		{
			throw new DeadEndException("source_CanCopySpell");
		}

		[ModifiesMember("CanCopySpell")]
		private static bool mod_CanCopySpell([NotNull] BlueprintAbility spell, [NotNull] Spellbook spellbook)
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				return source_CanCopySpell(spell, spellbook);
			}

			if (spellbook.IsKnown(spell))
			{
				return false;
			}

			bool spellListContainsSpell = spellbook.Blueprint.SpellList.Contains(spell);

			if (spellbook.Blueprint.Spontaneous && spellListContainsSpell)
			{
				return true;
			}

			return spellbook.Blueprint.CanCopyScrolls && spellListContainsSpell;
		}
	}
}

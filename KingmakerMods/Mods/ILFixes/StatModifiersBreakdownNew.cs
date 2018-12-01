using JetBrains.Annotations;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UI;
using Kingmaker.UI.Common;
using Patchwork;

namespace KingmakerMods.Mods.ILFixes
{
	[ModifiesType("Kingmaker.UI.Common.StatModifiersBreakdown")]
	public static class StatModifiersBreakdownNew
	{
		// [IL]: Error: [Assembly-CSharp : Kingmaker.UI.Common.StatModifiersBreakdown::AddStatModifiers][offset 0x00000054][found ref 'System.Object'][expected ref 'Kingmaker.UI.IUIDataProvider'] Unexpected type on the stack.(Error: 0x80131854)

		[ModifiesMember("AddStatModifiers")]
		public static void mod_AddStatModifiers([NotNull] ModifiableValue stat)
		{
			foreach (ModifiableValue.Modifier displayModifier in stat.GetDisplayModifiers())
			{
				if (displayModifier.ModValue == 0)
				{
					continue;
				}

				ModifierDescriptor descriptor = displayModifier.ModDescriptor == ModifierDescriptor.None ? ModifierDescriptor.Other : displayModifier.ModDescriptor;

				var bonusSource = (IUIDataProvider) (displayModifier.Source ?? (object) displayModifier.ItemSource);

				StatModifiersBreakdown.AddBonus(displayModifier.ModValue, bonusSource, descriptor);
			}
		}

		// [IL]: Error: [Assembly-CSharp : Kingmaker.UI.Common.StatModifiersBreakdown::AddArmorClassModifiers][offset 0x00000098][found ref 'System.Object'][expected ref 'Kingmaker.UI.IUIDataProvider'] Unexpected type on the stack.(Error: 0x80131854)

		[ModifiesMember("AddArmorClassModifiers")]
		public static void mod_AddArmorClassModifiers([NotNull] ModifiableValueArmorClass acStat, bool flatfooted, bool touch, bool ignoreDexterityBonus = false)
		{
			foreach (ModifiableValue.Modifier displayModifier in acStat.GetDisplayModifiers())
			{
				if (displayModifier.ModValue == 0 || flatfooted && !ModifiableValueArmorClass.AllowedForFlatFooted(displayModifier) || touch && !ModifiableValueArmorClass.AllowedForTouch(displayModifier) || ignoreDexterityBonus && displayModifier.ModDescriptor == ModifierDescriptor.DexterityBonus)
				{
					continue;
				}

				ModifierDescriptor descriptor = displayModifier.ModDescriptor == ModifierDescriptor.None ? ModifierDescriptor.Other : displayModifier.ModDescriptor;

				var bonusSource = (IUIDataProvider) (displayModifier.Source ?? (object) displayModifier.ItemSource);

				StatModifiersBreakdown.AddBonus(displayModifier.ModValue, bonusSource, descriptor);
			}
		}
	}
}

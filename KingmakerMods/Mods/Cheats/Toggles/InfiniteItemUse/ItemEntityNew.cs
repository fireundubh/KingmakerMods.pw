using System;
using JetBrains.Annotations;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Items;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.InfiniteItemUse
{
	[ModifiesType]
	public class ItemEntityNew : ItemEntity
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("SpendCharges")]
		public bool source_SpendCharges(UnitDescriptor user)
		{
			throw new DeadEndException("source_SpendCharges");
		}
		#endregion

		[ModifiesMember("SpendCharges")]
		public bool mod_SpendCharges(UnitDescriptor user)
		{
			if (!KingmakerPatchSettings.Cheats.InfiniteItemUse)
			{
				return this.source_SpendCharges(user);
			}

			var blueprintItemEquipment = this.Blueprint as BlueprintItemEquipment;

			if (!blueprintItemEquipment || !blueprintItemEquipment.GainAbility)
			{
				UberDebug.LogError(this.Blueprint, $"Item {this.Blueprint} doesn't gain ability");
				return false;
			}

			if (!this.IsSpendCharges)
			{
				return true;
			}

			var hasCharges = false;

			if (this.Charges > 0)
			{
				var itemEntityUsable = new ItemEntityUsable((BlueprintItemEquipmentUsable) this.Blueprint);

				if (user.State.Features.HandOfMagusDan && itemEntityUsable.Blueprint.Type == UsableItemType.Scroll)
				{
					var ruleRollDice = new RuleRollDice(user.Unit, new DiceFormula(1, DiceType.D100));

					Rulebook.Trigger(ruleRollDice);

					if (ruleRollDice.Result <= 25)
					{
						return true;
					}
				}

				if (user.IsPlayerFaction)
				{
					return true;
				}

				--this.Charges;
			}
			else
			{
				hasCharges = true;
				UberDebug.LogError("Has no charges");
			}

			if (this.Charges >= 1 || blueprintItemEquipment.RestoreChargesOnRest)
			{
				return !hasCharges;
			}

			if (this.Count > 1)
			{
				this.DecrementCount(1);
				this.Charges = 1;
			}
			else
			{
				ItemsCollection collection = this.Collection;
				collection?.Remove(this);
			}

			return !hasCharges;
		}

		[Obsolete]
		public ItemEntityNew([NotNull] BlueprintItem bpItem) : base(bpItem)
		{
		}
	}
}

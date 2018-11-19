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
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("SpendCharges")]
		public bool source_SpendCharges(UnitDescriptor user)
		{
			throw new DeadEndException("source_SpendCharges");
		}

		[ModifiesMember("SpendCharges")]
		public bool mod_SpendCharges(UnitDescriptor user)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bInfiniteItemUse");
			}

			if (!_useMod)
			{
				return this.source_SpendCharges(user);
			}

			BlueprintItemEquipment blueprintItemEquipment = this.Blueprint as BlueprintItemEquipment;

			if (!blueprintItemEquipment || !blueprintItemEquipment.GainAbility)
			{
				UberDebug.LogError(this.Blueprint, $"Item {this.Blueprint} doesn't gain ability");
				return false;
			}

			if (!this.IsSpendCharges)
			{
				return true;
			}

			bool hasCharges = false;

			if (this.Charges > 0)
			{
				ItemEntityUsable itemEntityUsable = new ItemEntityUsable((BlueprintItemEquipmentUsable) this.Blueprint);

				if (user.State.Features.HandOfMagusDan && itemEntityUsable.Blueprint.Type == UsableItemType.Scroll)
				{
					RuleRollDice ruleRollDice = new RuleRollDice(user.Unit, new DiceFormula(1, DiceType.D100));

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

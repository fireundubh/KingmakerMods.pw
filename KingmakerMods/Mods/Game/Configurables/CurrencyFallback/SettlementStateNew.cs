using System;
using System.Linq;
using Kingmaker.Kingdom;
using Kingmaker.Kingdom.Settlements;
using Kingmaker.PubSubSystem;
using Kingmaker.UI.Settlement;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Configurables.CurrencyFallback
{
	[ModifiesType]
	public class SettlementStateNew : SettlementState
	{
		#region ALIASES
		[ModifiesMember("m_SlotsLeft", ModificationScope.Nothing)]
		private int alias_m_SlotsLeft;

		[ModifiesMember("m_Buildings", ModificationScope.Nothing)]
		private readonly BuildingsCollection alias_m_Buildings;

		[ModifiesMember("SellDiscountedBuilding", ModificationScope.Nothing)]
		public BlueprintSettlementBuilding alias_SellDiscountedBuilding
		{
			[ModifiesMember("get_SellDiscountedBuilding", ModificationScope.Nothing)]
			get;
			[ModifiesMember("set_SellDiscountedBuilding", ModificationScope.Nothing)]
			private set;
		}

		[ModifiesMember("CanBuildByLevel", ModificationScope.Nothing)]
		private bool alias_CanBuildByLevel(BlueprintSettlementBuilding building)
		{
			throw new DeadEndException("source_CanBuildByLevel");
		}
		#endregion

		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("Build")]
		public SettlementBuilding source_Build(BlueprintSettlementBuilding building, SettlementGridTopology.Slot slot, bool force = false)
		{
			throw new DeadEndException("source_Build");
		}

		[NewMember]
		[DuplicatesBody("CanBuild")]
		public bool source_CanBuild(BlueprintSettlementBuilding building)
		{
			throw new DeadEndException("source_CanBuild");
		}

		[NewMember]
		[DuplicatesBody("CanBuildUprgade")]
		public bool source_CanBuildUprgade(BlueprintSettlementBuilding building)
		{
			throw new DeadEndException("source_CanBuildUprgade");
		}

		[NewMember]
		[DuplicatesBody("UpgradeBuilding")]
		public SettlementBuilding source_UpgradeBuilding(SettlementBuilding building)
		{
			throw new DeadEndException("source_UpgradeBuilding");
		}
		#endregion

		[ModifiesMember("Build")]
		public SettlementBuilding mod_Build(BlueprintSettlementBuilding building, SettlementGridTopology.Slot slot, bool force = false)
		{
			if (!KingmakerPatchSettings.CurrencyFallback.Enabled)
			{
				return this.source_Build(building, slot, force);
			}

			var removedBuilding = true;

			if (!force)
			{
				if (!this.CanBuild(building))
				{
					return null;
				}

				BuildingSlot slotObject = slot.GetSlotObject();

				if (slotObject?.CanBuildHere(building) != true)
				{
					return null;
				}

				KingdomCurrencyFallback.SpendPoints(this.GetActualCost(building));

				removedBuilding = this.FreeBuildings.Remove(building) || KingdomState.Instance.FreeBuildings.Remove(building);
			}

			SettlementBuilding settlementBuilding = this.alias_m_Buildings.Build(building);
			settlementBuilding.BuildOnSlot(slot);

			if (building.SpecialSlot == SpecialSlotType.None)
			{
				this.alias_m_SlotsLeft -= building.SlotCount;
			}

			if (!force && !removedBuilding || this.alias_SellDiscountedBuilding != building)
			{
				this.alias_SellDiscountedBuilding = null;
			}

			this.Update();

			EventBus.RaiseEvent((ISettlementBuildingHandler h) => h.OnBuildingStarted(this, settlementBuilding));

			return settlementBuilding;
		}

		[ModifiesMember("CanBuild")]
		public bool mod_CanBuild(BlueprintSettlementBuilding building)
		{
			if (!KingmakerPatchSettings.CurrencyFallback.Enabled)
			{
				return this.source_CanBuild(building);
			}

			if (!KingdomCurrencyFallback.CanSpend(this.GetActualCost(building)))
			{
				UberDebug.LogSystem("[fireundubh] mod_CanBuild: Cannot spend");
				return false;
			}

			SpecialSlotType specialSlot = building.SpecialSlot;

			if (specialSlot != SpecialSlotType.None)
			{
				if (this.IsSpecialSlotFilled(specialSlot))
				{
					return false;
				}
			}
			else if (this.alias_m_SlotsLeft < building.SlotCount)
			{
				return false;
			}

			return this.alias_CanBuildByLevel(building);
		}

		[ModifiesMember("CanBuildUprgade")]
		public bool mod_CanBuildUprgade(BlueprintSettlementBuilding building)
		{
			if (!KingmakerPatchSettings.CurrencyFallback.Enabled)
			{
				return this.source_CanBuildUprgade(building);
			}

			return KingdomCurrencyFallback.CanSpend(this.GetActualCost(building)) && building.MinLevel <= this.Level && this.Buildings.Any(b => b.Blueprint.UpgradesTo == building);
		}

		[ModifiesMember("UpgradeBuilding")]
		public SettlementBuilding mod_UpgradeBuilding(SettlementBuilding building)
		{
			if (!KingmakerPatchSettings.CurrencyFallback.Enabled)
			{
				return this.source_UpgradeBuilding(building);
			}

			if (!building.IsFinished || !this.alias_m_Buildings.HasFact(building) || !building.Blueprint.UpgradesTo)
			{
				return null;
			}

			if (!KingdomCurrencyFallback.CanSpend(this.GetActualCost(building.Blueprint.UpgradesTo)))
			{
				UberDebug.LogWarning("Cannot upgrade " + building.Blueprint + ": not enough BP");
				return null;
			}

			KingdomCurrencyFallback.SpendPoints(this.GetActualCost(building.Blueprint));

			SettlementBuilding result = this.alias_m_Buildings.Upgrade(building);

			this.Update();

			EventBus.RaiseEvent((ISettlementBuildUpdate h) => h.OnBuildUpdate(building));

			return result;
		}

		[Obsolete]
		public SettlementStateNew(LevelType level) : base(level)
		{
		}
	}
}

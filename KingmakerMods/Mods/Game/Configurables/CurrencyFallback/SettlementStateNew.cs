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
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[ModifiesMember("m_SlotsLeft", ModificationScope.Nothing)]
		private int source_m_SlotsLeft;

		[ModifiesMember("m_Buildings", ModificationScope.Nothing)]
		private readonly BuildingsCollection source_m_Buildings;

		[ModifiesMember("SellDiscountedBuilding", ModificationScope.Nothing)]
		public BlueprintSettlementBuilding source_SellDiscountedBuilding { get; private set; }

		[NewMember]
		private static bool IsModReady()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game.KingdomEvents", "bCurrencyFallback");
			}

			return _useMod;
		}

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

		[ModifiesMember("CanBuildByLevel", ModificationScope.Nothing)]
		private bool source_CanBuildByLevel(BlueprintSettlementBuilding building)
		{
			throw new DeadEndException("source_CanBuildByLevel");
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

		[ModifiesMember("Build")]
		public SettlementBuilding mod_Build(BlueprintSettlementBuilding building, SettlementGridTopology.Slot slot, bool force = false)
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				return this.source_Build(building, slot, force);
			}

			bool removedBuilding = true;

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

			SettlementBuilding settlementBuilding = this.source_m_Buildings.Build(building);
			settlementBuilding.BuildOnSlot(slot);

			if (building.SpecialSlot == SpecialSlotType.None)
			{
				this.source_m_SlotsLeft -= building.SlotCount;
			}

			if (!force && !removedBuilding || this.source_SellDiscountedBuilding != building)
			{
				this.source_SellDiscountedBuilding = null;
			}

			this.Update();

			EventBus.RaiseEvent((ISettlementBuildingHandler h) => h.OnBuildingStarted(this, settlementBuilding));

			return settlementBuilding;
		}

		[ModifiesMember("CanBuild")]
		public bool mod_CanBuild(BlueprintSettlementBuilding building)
		{
			_useMod = IsModReady();

			if (!_useMod)
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
			else if (this.source_m_SlotsLeft < building.SlotCount)
			{
				return false;
			}

			return this.source_CanBuildByLevel(building);
		}

		[ModifiesMember("CanBuildUprgade")]
		public bool mod_CanBuildUprgade(BlueprintSettlementBuilding building)
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				return this.source_CanBuildUprgade(building);
			}

			return KingdomCurrencyFallback.CanSpend(this.GetActualCost(building)) && building.MinLevel <= this.Level && this.Buildings.Any(b => b.Blueprint.UpgradesTo == building);
		}

		[ModifiesMember("UpgradeBuilding")]
		public SettlementBuilding mod_UpgradeBuilding(SettlementBuilding building)
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				return this.source_UpgradeBuilding(building);
			}

			if (!building.IsFinished || !this.source_m_Buildings.HasFact(building) || !building.Blueprint.UpgradesTo)
			{
				return null;
			}

			if (!KingdomCurrencyFallback.CanSpend(this.GetActualCost(building.Blueprint.UpgradesTo)))
			{
				UberDebug.LogWarning("Cannot upgrade " + building.Blueprint + ": not enough BP");
				return null;
			}

			KingdomCurrencyFallback.SpendPoints(this.GetActualCost(building.Blueprint));

			SettlementBuilding result = this.source_m_Buildings.Upgrade(building);

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

using System;
using System.Linq;
using Kingmaker.Kingdom;
using Kingmaker.Kingdom.Blueprints;
using Kingmaker.Kingdom.Settlements;
using Kingmaker.PubSubSystem;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Configurables.CurrencyFallback
{
	[ModifiesType]
	public class RegionStateNew : RegionState
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("FoundSettlement")]
		public void source_FoundSettlement(RegionSettlementLocation settlementLocation, string name = null)
		{
			throw new DeadEndException("source_FoundSettlement");
		}
		#endregion

		[ModifiesMember("FoundSettlement")]
		public void mod_FoundSettlement(RegionSettlementLocation settlementLocation, string name = null)
		{
			if (!KingmakerPatchSettings.CurrencyFallback.Enabled)
			{
				this.source_FoundSettlement(settlementLocation, name);
				return;
			}

			if (!this.Blueprint.SettlementBuildArea)
			{
				UberDebug.LogError("Cannot found a settlement in {0}: no building area set up", settlementLocation);
				return;
			}

			if (this.Settlement != null)
			{
				UberDebug.LogError("Cannot found a settlement in {0}: already built", settlementLocation);
				return;
			}

			if (settlementLocation != null && settlementLocation.AssociatedLocation == null)
			{
				UberDebug.LogError("Cannot found a settlement in {0}: no globalmap location associated", settlementLocation);
				return;
			}

			if (settlementLocation == null && this.Blueprint.SettlementGlobalmapLocations.Length == 0)
			{
				UberDebug.LogError("Cannot found a settlement in {0}: no location specified and no default found", this.Blueprint);
				return;
			}

			KingdomCurrencyFallback.SpendPoints(KingdomRoot.Instance.SettlementBPCost);

			var settlementState = new SettlementState(SettlementState.LevelType.Village)
			{
				Region = this
			};

			SettlementState settlementState2 = settlementState;

			settlementState2.HasWaterSlot = settlementLocation?.HasWaterSlot == true;

			settlementState.Name = name ?? this.Blueprint.DefaultSettlementName;

			settlementState.Location = settlementLocation?.AssociatedLocation ?? this.Blueprint.SettlementGlobalmapLocations.FirstOrDefault();

			settlementState.SettlementLocation = settlementLocation;

			this.Settlement = settlementState;

			this.SetSettlementUIMarkers();

			EventBus.RaiseEvent((ISettlementFoundingHandler h) => h.OnSettlementFounded(this.Settlement));
		}

		[Obsolete]
		public RegionStateNew(BlueprintRegion blueprint) : base(blueprint)
		{
		}
	}
}

using System;
using System.Linq;
using Kingmaker.Enums;
using Kingmaker.Kingdom;
using Kingmaker.Kingdom.Blueprints;
using Kingmaker.Kingdom.Tasks;
using Kingmaker.UnitLogic.Alignments;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Configurables.KingdomEvents
{
	[ModifiesType]
	public class KingdomEventNew : KingdomEvent
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useAlignmentOverride;

		[NewMember]
		private static bool _useMarginOverride;

		[NewMember]
		private static string _alignmentString;

		[NewMember]
		private static AlignmentMaskType new_GetAlignment(AlignmentMaskType alignment)
		{
			switch (_alignmentString)
			{
				case "lawfulgood":
					alignment = AlignmentMaskType.LawfulGood;
					break;
				case "neutralgood":
					alignment = AlignmentMaskType.NeutralGood;
					break;
				case "chaoticgood":
					alignment = AlignmentMaskType.ChaoticGood;
					break;
				case "lawfulneutral":
					alignment = AlignmentMaskType.LawfulNeutral;
					break;
				case "trueneutral":
					alignment = AlignmentMaskType.TrueNeutral;
					break;
				case "chaoticneutral":
					alignment = AlignmentMaskType.ChaoticNeutral;
					break;
				case "lawfulevil":
					alignment = AlignmentMaskType.LawfulEvil;
					break;
				case "neutralevil":
					alignment = AlignmentMaskType.NeutralEvil;
					break;
				case "chaoticevil":
					alignment = AlignmentMaskType.ChaoticEvil;
					break;
			}

			return alignment;
		}

		[NewMember]
		private EventResult.MarginType new_GetOverrideMargin()
		{
			KingdomTaskEvent associatedTask = this.AssociatedTask;

			LeaderType leaderType = LeaderType.None;

			if (associatedTask != null)
			{
				leaderType = associatedTask.AssignedLeader.Type;
			}

			EventResult.MarginType autoResolveType = this.EventBlueprint.AutoResolveResult;

			bool hasSolutions = this.EventBlueprint.Solutions.HasSolutions;

			if (!hasSolutions)
			{
				return autoResolveType;
			}

			bool canSolve = this.EventBlueprint.Solutions.CanSolve(leaderType);

			if (!canSolve)
			{
				return autoResolveType;
			}

			EventResult[] possibleResults = this.EventBlueprint.Solutions.GetResolutions(leaderType).
			                                     Where(r => !string.IsNullOrEmpty(r.LocalizedDescription)).
			                                     ToArray();

			if (possibleResults.Any(r => r.Margin == EventResult.MarginType.GreatSuccess))
			{
				return EventResult.MarginType.GreatSuccess;
			}

			if (possibleResults.Any(r => r.Margin == EventResult.MarginType.Success))
			{
				return EventResult.MarginType.Success;
			}

			return autoResolveType;
		}

		[NewMember]
		[DuplicatesBody("ForceFinalResolve")]
		public void source_ForceFinalResolve(EventResult.MarginType margin, AlignmentMaskType? overrideAlignment = null)
		{
			throw new DeadEndException("source_ForceFinalResolve");
		}

		[ModifiesMember("ForceFinalResolve")]
		public void mod_ForceFinalResolve(EventResult.MarginType margin, AlignmentMaskType? overrideAlignment = null)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useAlignmentOverride = UserConfig.Parser.GetValueAsBool("Cheats.KingdomAlignment", "bEnabled");
				_useMarginOverride = UserConfig.Parser.GetValueAsBool("Cheats.KingdomEvents", "bMaximumEffort");
			}

			if (!_useAlignmentOverride && !_useMarginOverride)
			{
				this.source_ForceFinalResolve(margin, overrideAlignment);
				return;
			}

			if (_useAlignmentOverride)
			{
				_alignmentString = UserConfig.Parser.GetValueAsString("Cheats.KingdomAlignment", "sAlignment").ToLowerInvariant();
				overrideAlignment = new_GetAlignment(overrideAlignment ?? KingdomState.Instance.Alignment.ToMask());
			}

			if (!_useMarginOverride)
			{
				this.source_ForceFinalResolve(margin, overrideAlignment);
				return;
			}

			EventResult.MarginType overrideMargin = this.new_GetOverrideMargin();

			if (overrideMargin == EventResult.MarginType.Success || overrideMargin == EventResult.MarginType.GreatSuccess)
			{
				this.source_ForceFinalResolve(overrideMargin, overrideAlignment);
				return;
			}

			this.source_ForceFinalResolve(margin, overrideAlignment);
		}

		[NewMember]
		[DuplicatesBody("Resolve")]
		private void source_Resolve(int checkMargin, AlignmentMaskType alignment, LeaderType type)
		{
			throw new DeadEndException("source_Resolve");
		}

		[ModifiesMember("Resolve")]
		private void mod_Resolve(int checkMargin, AlignmentMaskType alignment, LeaderType type)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useAlignmentOverride = UserConfig.Parser.GetValueAsBool("Cheats.KingdomAlignment", "bEnabled");
				_useMarginOverride = UserConfig.Parser.GetValueAsBool("Cheats.KingdomEvents", "bMaximumEffort");
			}

			if (!_useAlignmentOverride && !_useMarginOverride)
			{
				this.source_Resolve(checkMargin, alignment, type);
				return;
			}

			if (_useAlignmentOverride)
			{
				_alignmentString = UserConfig.Parser.GetValueAsString("Cheats.KingdomAlignment", "sAlignment").ToLowerInvariant();
				alignment = new_GetAlignment(alignment);
			}

			if (!_useMarginOverride)
			{
				this.source_Resolve(checkMargin, alignment, type);
				return;
			}

			EventResult.MarginType overrideMargin = this.new_GetOverrideMargin();

			if (overrideMargin == EventResult.MarginType.Success || overrideMargin == EventResult.MarginType.GreatSuccess)
			{
				checkMargin = EventResult.MarginToInt(overrideMargin);
			}

			this.source_Resolve(checkMargin, alignment, type);
		}

		[Obsolete]
		public KingdomEventNew(BlueprintKingdomEventBase blueprint, RegionState region) : base(blueprint, region)
		{
		}
	}
}

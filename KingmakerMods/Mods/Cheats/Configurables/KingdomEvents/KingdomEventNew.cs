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
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("ForceFinalResolve")]
		public void source_ForceFinalResolve(EventResult.MarginType margin, AlignmentMaskType? overrideAlignment = null)
		{
			throw new DeadEndException("source_ForceFinalResolve");
		}

		[NewMember]
		[DuplicatesBody("Resolve")]
		private void source_Resolve(int checkMargin, AlignmentMaskType alignment, LeaderType type)
		{
			throw new DeadEndException("source_Resolve");
		}
		#endregion

		[NewMember]
		private static AlignmentMaskType? new_GetAlignment(string alignmentString)
		{
			AlignmentMaskType alignment;

			switch (alignmentString)
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
				default:
					return null;
			}

			return alignment;
		}

		[NewMember]
		private EventResult.MarginType new_GetOverrideMargin()
		{
			KingdomTaskEvent associatedTask = this.AssociatedTask;

			var leaderType = LeaderType.None;

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

			EventResult[] possibleResults = this.EventBlueprint.Solutions.GetResolutions(leaderType).Where(r => !string.IsNullOrEmpty(r.LocalizedDescription)).ToArray();

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

		[ModifiesMember("ForceFinalResolve")]
		public void mod_ForceFinalResolve(EventResult.MarginType margin, AlignmentMaskType? overrideAlignment = null)
		{
			if (!KingmakerPatchSettings.KingdomAlignment.Enabled && !KingmakerPatchSettings.KingdomEvents.MaximumEffort)
			{
				this.source_ForceFinalResolve(margin, overrideAlignment);
				return;
			}

			if (KingmakerPatchSettings.KingdomAlignment.Enabled)
			{
				string alignmentString = KingmakerPatchSettings.KingdomAlignment.Alignment.ToLowerInvariant();
				overrideAlignment = new_GetAlignment(alignmentString) ?? KingdomState.Instance.Alignment.ToMask();
			}

			if (!KingmakerPatchSettings.KingdomEvents.MaximumEffort)
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

		[ModifiesMember("Resolve")]
		private void mod_Resolve(int checkMargin, AlignmentMaskType alignment, LeaderType type)
		{
			if (!KingmakerPatchSettings.KingdomAlignment.Enabled && !KingmakerPatchSettings.KingdomEvents.MaximumEffort)
			{
				this.source_Resolve(checkMargin, alignment, type);
				return;
			}

			if (KingmakerPatchSettings.KingdomAlignment.Enabled)
			{
				string alignmentString = KingmakerPatchSettings.KingdomAlignment.Alignment.ToLowerInvariant();
				alignment = new_GetAlignment(alignmentString) ?? alignment;
			}

			if (!KingmakerPatchSettings.KingdomEvents.MaximumEffort)
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

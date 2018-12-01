using Kingmaker.Blueprints.Root;
using KingmakerMods.Helpers;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Game.Configurables.CompanionCost
{
	[ModifiesType]
	public class PlayerNew : Kingmaker.Player
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("GetCustomCompanionCost")]
		public int source_GetCustomCompanionCost()
		{
			throw new DeadEndException("source_GetCustomCompanionCost");
		}
		#endregion

		[ModifiesMember("GetCustomCompanionCost")]
		public int mod_GetCustomCompanionCost()
		{
			if (!KingmakerPatchSettings.CompanionCost.UsePartyLevelAsCostMultiplier && !KingmakerPatchSettings.CompanionCost.UseMultiplier)
			{
				return this.source_GetCustomCompanionCost();
			}

			if (KingmakerPatchSettings.CompanionCost.UsePartyLevelAsCostMultiplier)
			{
				return BlueprintRoot.Instance.CustomCompanionBaseCost * this.PartyLevel;
			}

			if (KingmakerPatchSettings.CompanionCost.UseMultiplier)
			{
				return BlueprintRoot.Instance.CustomCompanionBaseCost * KingmakerPatchSettings.CompanionCost.CostMultiplier;
			}

			return (int) (BlueprintRoot.Instance.CustomCompanionBaseCost * Mathf.Pow(this.PartyLevel, 2));
		}
	}
}

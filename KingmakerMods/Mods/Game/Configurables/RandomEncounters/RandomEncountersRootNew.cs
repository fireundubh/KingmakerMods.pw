using Kingmaker.Blueprints.Root;
using Patchwork;

namespace KingmakerMods.Mods.Game.Configurables.RandomEncounters
{
	[ModifiesType]
	public class RandomEncountersRootNew : RandomEncountersRoot
	{
		[MemberAlias(".ctor", typeof(object))]
		private void object_ctor()
		{
		}

		[ModifiesMember(".ctor")]
		public void CtorNew()
		{
			this.object_ctor();

			#region DEFAULT SETTINGS
			this.ChanceOnCamp = 0.4f;
			this.ChanceOnCampSecondTime = 0.1f;
			this.ChanceOnGlobalMap = 0.3f;
			this.DefaultSafeZoneSize = 4f;
			this.EncounterMaxBonusCR = 1;
			this.EncounterMinBonusCR = -1;
			this.EncounterPawnDistanceFromLocation = 1.5f;
			this.EncounterPawnOffset = 1.5f;
			this.EncountersEnabled = true;
			this.HardEncounterBonusCR = 3;
			this.HardEncounterChance = 0.05f;
			this.HardEncounterChanceIncrease = 0.05f;
			this.HardEncounterMaxChance = 0.9f;
			this.RandomEncounterAvoidanceFailMargin = 5;
			this.RollMiles = 8f;
			this.SafeMilesAfterEncounter = 16f;
			this.StalkerAmbushChance = 0.25f;
			#endregion

			if (!KingmakerPatchSettings.RandomEncounters.Enabled)
			{
				return;
			}

			#region USER SETTINGS
			this.ChanceOnCamp = KingmakerPatchSettings.RandomEncounters.ChanceOnCamp;
			this.ChanceOnCampSecondTime = KingmakerPatchSettings.RandomEncounters.ChanceOnCampSecondTime;
			this.ChanceOnGlobalMap = KingmakerPatchSettings.RandomEncounters.ChanceOnGlobalMap;
			this.DefaultSafeZoneSize = KingmakerPatchSettings.RandomEncounters.DefaultSafeZoneSize;
			this.EncounterMaxBonusCR = KingmakerPatchSettings.RandomEncounters.EncounterMaxBonusCR;
			this.EncounterMinBonusCR = KingmakerPatchSettings.RandomEncounters.EncounterMinBonusCR;
			this.EncounterPawnDistanceFromLocation = KingmakerPatchSettings.RandomEncounters.EncounterPawnDistanceFromLocation;
			this.EncounterPawnOffset = KingmakerPatchSettings.RandomEncounters.EncounterPawnOffset;
			this.EncountersEnabled = KingmakerPatchSettings.RandomEncounters.EncountersEnabled;
			this.HardEncounterBonusCR = KingmakerPatchSettings.RandomEncounters.HardEncounterBonusCR;
			this.HardEncounterChance = KingmakerPatchSettings.RandomEncounters.HardEncounterChance;
			this.HardEncounterChanceIncrease = KingmakerPatchSettings.RandomEncounters.HardEncounterChanceIncrease;
			this.HardEncounterMaxChance = KingmakerPatchSettings.RandomEncounters.HardEncounterMaxChance;
			this.RandomEncounterAvoidanceFailMargin = KingmakerPatchSettings.RandomEncounters.RandomEncounterAvoidanceFailMargin;
			this.RollMiles = KingmakerPatchSettings.RandomEncounters.RollMiles;
			this.SafeMilesAfterEncounter = KingmakerPatchSettings.RandomEncounters.SafeMilesAfterEncounter;
			this.StalkerAmbushChance = KingmakerPatchSettings.RandomEncounters.StalkerAmbushChance;
			#endregion
		}
	}
}

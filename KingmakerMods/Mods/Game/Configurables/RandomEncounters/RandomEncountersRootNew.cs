using Kingmaker.Blueprints.Root;
using Patchwork;

namespace KingmakerMods.Mods.Game.Configurables.RandomEncounters
{
	[ModifiesType]
	public class RandomEncountersRootNew : RandomEncountersRoot
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		private static float? _chanceOnGlobalMap; // range constrained

		[NewMember]
		private static float? _chanceOnCamp; // range constrained

		[NewMember]
		private static float? _chanceOnCampSecondTime; // range constrained

		[NewMember]
		private static float? _hardEncounterChance; // range constrained

		[NewMember]
		private static float? _hardEncounterMaxChance; // range constrained

		[NewMember]
		private static float? _hardEncounterChanceIncrease; // range constrained

		[NewMember]
		public static float? _stalkerAmbushChance; // range constrained

		[NewMember]
		private static float mod_GetRangeConstrainedValue(float? newValue, float defaultValue)
		{
			if (newValue == null)
			{
				return defaultValue;
			}

			return (float) (newValue >= 0.0 && newValue <= 1.0 ? newValue : defaultValue);
		}

		[NewMember]
		private void mod_RandomEncounterSetup()
		{
			// retrieve range-constrained settings
			_chanceOnGlobalMap = UserConfig.Parser.GetValueAsFloat("Game.RandomEncounters", "fChanceOnGlobalMap");
			_chanceOnCamp = UserConfig.Parser.GetValueAsFloat("Game.RandomEncounters", "fChanceOnCamp");
			_chanceOnCampSecondTime = UserConfig.Parser.GetValueAsFloat("Game.RandomEncounters", "fChanceOnCampSecondTime");
			_hardEncounterChance = UserConfig.Parser.GetValueAsFloat("Game.RandomEncounters", "fHardEncounterChance");
			_hardEncounterMaxChance = UserConfig.Parser.GetValueAsFloat("Game.RandomEncounters", "fHardEncounterMaxChance");
			_hardEncounterChanceIncrease = UserConfig.Parser.GetValueAsFloat("Game.RandomEncounters", "fHardEncounterChanceIncrease");
			_stalkerAmbushChance = UserConfig.Parser.GetValueAsFloat("Game.RandomEncounters", "fStalkerAmbushChance");

			// commit settings
			this.EncountersEnabled = UserConfig.Parser.GetValueAsBool("Game.RandomEncounters", "bEncountersEnabled");

			this.ChanceOnGlobalMap = mod_GetRangeConstrainedValue(_chanceOnGlobalMap, 0.3f);
			this.ChanceOnCamp = mod_GetRangeConstrainedValue(_chanceOnCamp, 0.4f);
			this.ChanceOnCampSecondTime = mod_GetRangeConstrainedValue(_chanceOnCampSecondTime, 0.1f);
			this.HardEncounterChance = mod_GetRangeConstrainedValue(_hardEncounterChance, 0.05f);
			this.HardEncounterMaxChance = mod_GetRangeConstrainedValue(_hardEncounterMaxChance, 0.9f);
			this.HardEncounterChanceIncrease = mod_GetRangeConstrainedValue(_hardEncounterChanceIncrease, 0.05f);
			this.StalkerAmbushChance = mod_GetRangeConstrainedValue(_stalkerAmbushChance, 0.25f);

			this.RollMiles = UserConfig.Parser.GetValueAsFloat("Game.RandomEncounters", "fRollMiles");
			this.SafeMilesAfterEncounter = UserConfig.Parser.GetValueAsFloat("Game.RandomEncounters", "fSafeMilesAfterEncounter");
			this.EncounterMinBonusCR = UserConfig.Parser.GetValueAsInt("Game.RandomEncounters", "iEncounterMinBonusCR");
			this.EncounterMaxBonusCR = UserConfig.Parser.GetValueAsInt("Game.RandomEncounters", "iEncounterMaxBonusCR");
			this.HardEncounterBonusCR = UserConfig.Parser.GetValueAsInt("Game.RandomEncounters", "iHardEncounterBonusCR");
			this.DefaultSafeZoneSize = UserConfig.Parser.GetValueAsFloat("Game.RandomEncounters", "fDefaultSafeZoneSize");
			this.RandomEncounterAvoidanceFailMargin = UserConfig.Parser.GetValueAsInt("Game.RandomEncounters", "iRandomEncounterAvoidanceFailMargin");
			this.EncounterPawnOffset = UserConfig.Parser.GetValueAsFloat("Game.RandomEncounters", "fEncounterPawnOffset");
			this.EncounterPawnDistanceFromLocation = UserConfig.Parser.GetValueAsFloat("Game.RandomEncounters", "fEncounterPawnDistanceFromLocation");
		}

		[MemberAlias(".ctor", typeof(object))]
		private void object_ctor()
		{
		}

		[ModifiesMember(".ctor")]
		public void CtorNew()
		{
			this.object_ctor();

			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game.RandomEncounters", "bEnabled");
			}

			this.EncountersEnabled = true;

			this.ChanceOnGlobalMap = 0.3f;
			this.ChanceOnCamp = 0.4f;
			this.ChanceOnCampSecondTime = 0.1f;
			this.HardEncounterChance = 0.05f;
			this.HardEncounterMaxChance = 0.9f;
			this.HardEncounterChanceIncrease = 0.05f;
			this.RollMiles = 8f;
			this.SafeMilesAfterEncounter = 16f;
			this.EncounterMinBonusCR = -1;
			this.EncounterMaxBonusCR = 1;
			this.HardEncounterBonusCR = 3;
			this.DefaultSafeZoneSize = 4f;
			this.RandomEncounterAvoidanceFailMargin = 5;
			this.EncounterPawnOffset = 1.5f;
			this.EncounterPawnDistanceFromLocation = 1.5f;
			this.StalkerAmbushChance = 0.25f;

			if (_useMod)
			{
				this.mod_RandomEncounterSetup();
			}
		}
	}
}

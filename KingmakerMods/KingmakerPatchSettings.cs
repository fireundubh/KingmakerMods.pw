using Patchwork;

namespace KingmakerMods
{
	[NewType]
	public static class KingmakerPatchSettings
	{
		[NewType]
		public static class Affection
		{
			private const string SECTION = "Cheats.Affection";

			public static bool IncreaseMultEnabled { get; set; }

			public static float IncreaseMultAmount { get; set; }

			public static bool DecreaseMultEnabled { get; set; }

			public static float DecreaseMultAmount { get; set; }

			public static bool FixedAffectionIncreaseEnabled { get; set; }

			public static int FixedAffectionIncreaseAmount { get; set; }

			public static bool FixedAffectionDecreaseEnabled { get; set; }

			public static int FixedAffectionDecreaseAmount { get; set; }

			static Affection()
			{
				IncreaseMultEnabled = UserConfig.Parser.TryGetBool(SECTION, "bIncreaseMultEnabled");
				IncreaseMultAmount  = UserConfig.Parser.TryGetFloat(SECTION, "fIncreaseMult", 1f);

				DecreaseMultEnabled = UserConfig.Parser.TryGetBool(SECTION, "bDecreaseMultEnabled");
				DecreaseMultAmount  = UserConfig.Parser.TryGetFloat(SECTION, "fDecreaseMult", 1f);

				FixedAffectionIncreaseEnabled = UserConfig.Parser.TryGetBool(SECTION, "bFixedIncreaseEnabled");
				FixedAffectionIncreaseAmount  = UserConfig.Parser.TryGetInt(SECTION, "iFixedIncrease", 1);

				FixedAffectionDecreaseEnabled = UserConfig.Parser.TryGetBool(SECTION, "bFixedDecreaseEnabled");
				FixedAffectionDecreaseAmount  = UserConfig.Parser.TryGetInt(SECTION, "iFixedDecrease", -1);
			}
		}

		[NewType]
		public static class AttributeUncapper
		{
			private const string SECTION = "Cheats.AttributeUncapper";

			/// <summary>
			/// When true, at character creation, you can increase attributes up to AttributeMax.
			/// </summary>
			public static bool Enabled { get; set; }

			/// <summary>
			/// At character creation, you can increase attributes up to AttributeMax.
			/// </summary>
			public static int AttributeMax { get; set; }

			static AttributeUncapper()
			{
				Enabled      = UserConfig.Parser.TryGetBool(SECTION, "bEnabled");
				AttributeMax = UserConfig.Parser.TryGetInt(SECTION, "iAttributeMax", 18, true, 18, int.MaxValue);
			}
		}

		[NewType]
		public static class BuyLowSellHigh
		{
			private const string SECTION = "Game.VendorLogic";

			/// <summary>
			/// When true, your active companions' highest modified Persuasion score will affect the value of item transactions.
			/// </summary>
			public static bool Enabled { get; set; }

			/// <summary>
			/// At Lv. 20 Persuasion, the default value (40) grants a 50% discount on vendors' items and a 50% markup on your items.
			/// iBuyDivisor applies to items you buy from the vendor. Discount Formula: 1 - HighestPersuasionScore / iBuyDivisor
			/// </summary>
			public static int BuyDivisor { get; set; }

			/// <summary>
			/// At Lv. 20 Persuasion, the default value (40) grants a 50% discount on vendors' items and a 50% markup on your items.
			/// iSellDivisor applies to items you sell to the vendor. Markup Formula: 1 + HighestPersuasionScore / iSellDivisor
			/// </summary>
			public static int SellDivisor { get; set; }

			/// <summary>
			/// If the highest modified Persuasion score exceeds iPersuasionCap, HighestPersuasionScore will be capped to this value.
			/// Note:
			/// - If you raise the cap too high, and your highest modified Persuasion score is extreme, vendors will pay you to take their items.
			/// - Exposed only for completeness. Best left alone.
			/// </summary>
			public static int PersuasionCap { get; set; }

			static BuyLowSellHigh()
			{
				Enabled       = UserConfig.Parser.TryGetBool(SECTION, "bBuyLowSellHigh");
				BuyDivisor    = UserConfig.Parser.TryGetInt(SECTION, "iBuyDivisor", 40, true, 1, int.MaxValue);
				SellDivisor   = UserConfig.Parser.TryGetInt(SECTION, "iSellDivisor", 40, true, 1, int.MaxValue);
				PersuasionCap = UserConfig.Parser.TryGetInt(SECTION, "iPersuasionCap", 20);
			}
		}

		[NewType]
		public static class CameraRig
		{
			private const string SECTION = "Game.CameraRig";

			/// <summary>
			/// When true, you can use the following settings to customize the camera rig.
			/// </summary>
			public static bool Enabled { get; set; }

			public static float ScrollRubberBand { get; set; }
			public static float ScrollRubberBandCamp { get; set; }
			public static float ScrollScreenThreshold { get; set; }
			public static float ScrollSpeed { get; set; }
			public static float DragScrollSpeed { get; set; }

			static CameraRig()
			{
				Enabled               = UserConfig.Parser.TryGetBool(SECTION, "bEnabled");
				ScrollRubberBand      = UserConfig.Parser.TryGetFloat(SECTION, "fScrollRubberBand", 4f);
				ScrollRubberBandCamp  = UserConfig.Parser.TryGetFloat(SECTION, "fScrollRubberBandCamp", 3f);
				ScrollScreenThreshold = UserConfig.Parser.TryGetFloat(SECTION, "fScrollScreenThreshold", 4f);
				ScrollSpeed           = UserConfig.Parser.TryGetFloat(SECTION, "fScrollSpeed", 25f);
				DragScrollSpeed       = UserConfig.Parser.TryGetFloat(SECTION, "fDragScrollSpeed", 1f);
			}
		}

		[NewType]
		public static class CameraZoom
		{
			private const string SECTION = "Game.CameraZoom";

			/// <summary>
			/// When true, the field of view max will be set to fCameraZoomMax. Setting fCameraZoomMax to very high values is not advisable.
			/// </summary>
			public static bool Enabled { get; set; }

			public static float CameraZoomMax { get; set; }
			public static float CameraZoomMaxCutscene { get; set; }
			public static float CameraZoomMaxDialog { get; set; }
			public static float CameraZoomMaxGlobalMap { get; set; }
			public static float CameraZoomMaxSettlement { get; set; }

			static CameraZoom()
			{
				Enabled                 = UserConfig.Parser.TryGetBool(SECTION, "bEnabled");
				CameraZoomMax           = UserConfig.Parser.TryGetFloat(SECTION, "fCameraZoomMax", 22f);
				CameraZoomMaxCutscene   = UserConfig.Parser.TryGetFloat(SECTION, "fCameraZoomMaxCutscene", 22f);
				CameraZoomMaxDialog     = UserConfig.Parser.TryGetFloat(SECTION, "fCameraZoomMaxDialog", 22f);
				CameraZoomMaxGlobalMap  = UserConfig.Parser.TryGetFloat(SECTION, "fCameraZoomMaxGlobalMap", 28.3f);
				CameraZoomMaxSettlement = UserConfig.Parser.TryGetFloat(SECTION, "fCameraZoomMaxSettlement", 22f);
			}
		}

		[NewType]
		public static class Cheats
		{
			private const string SECTION = "Cheats";

			/// <summary>
			/// When true, while traveling on the world map, you will reveal all possible locations regardless
			/// of your party's highest Perception score. Hidden Locations remain hidden. Some locations are
			/// revealed only when specific conditions are met.
			/// </summary>
			public static bool Abracadabra { get; set; }

			/// <summary>
			/// When true, spontaneous casters may copy scrolls.
			/// </summary>
			public static bool AllowSpontaneousCastersToCopyScrolls { get; set; }

			/// <summary>
			/// When true, your party will always have light encumbrance in areas or on the world map.
			/// </summary>
			public static bool AlwaysUnencumbered { get; set; }

			public static bool CookingRequiresNoIngredients { get; set; }

			/// <summary>
			/// When true, fog of war will be disabled.
			/// </summary>
			public static bool DisableFogOfWar { get; set; }

			/// <summary>
			/// When true, directly controllable units will not spend resources to cast abilities.
			/// </summary>
			public static bool InfiniteAbilityUse { get; set; }

			/// <summary>
			/// When true, directly controllable units will not spend charges to use items.
			/// </summary>
			public static bool InfiniteItemUse { get; set; }

			/// <summary>
			/// When true, directly controllable units will not spend resources to cast spells.
			/// </summary>
			public static bool InfiniteSpellUse { get; set; }

			/// <summary>
			/// When true, you perform move, standard, and swift actions without cooldowns.
			/// </summary>
			public static bool InstantCooldowns { get; set; }

			/// <summary>
			/// When true, you can change your party on the global map without advancing time.
			/// </summary>
			public static bool InstantPartyChange { get; set; }

			/// <summary>
			/// When true, you and your party will never fail skill checks.
			/// </summary>
			public static bool NeverFailSkillChecks { get; set; }

			/// <summary>
			/// When true, your party members can no longer deal any type of damage to each other or themselves.
			/// </summary>
			public static bool NoFriendlyFire { get; set; }

			/// <summary>
			/// When true, your party members can no longer deal area-of-effect damage to each other or themselves.
			/// </summary>
			public static bool NoFriendlyFireAOE { get; set; }

			public static bool Undetectable { get; set; }

			public static bool UndetectableStealthAttacks { get; set; }

			public static bool UnlockCookingRecipes { get; set; }

			static Cheats()
			{
				AllowSpontaneousCastersToCopyScrolls = UserConfig.Parser.TryGetBool(SECTION, "bAllowSpontaneousCastersToCopyScrolls");
				AlwaysUnencumbered                   = UserConfig.Parser.TryGetBool(SECTION, "bAlwaysUnencumbered");
				CookingRequiresNoIngredients         = UserConfig.Parser.TryGetBool(SECTION, "bCookingRequiresNoIngredients");
				DisableFogOfWar                      = UserConfig.Parser.TryGetBool(SECTION, "bDisableFogOfWar");
				InfiniteAbilityUse                   = UserConfig.Parser.TryGetBool(SECTION, "bInfiniteAbilityUse");
				InfiniteItemUse                      = UserConfig.Parser.TryGetBool(SECTION, "bInfiniteItemUse");
				InfiniteSpellUse                     = UserConfig.Parser.TryGetBool(SECTION, "bInfiniteSpellUse");
				InstantCooldowns                     = UserConfig.Parser.TryGetBool(SECTION, "bInstantCooldowns");
				InstantPartyChange                   = UserConfig.Parser.TryGetBool(SECTION, "bInstantPartyChange");
				NeverFailSkillChecks                 = UserConfig.Parser.TryGetBool(SECTION, "bNeverFailSkillChecks");
				NoFriendlyFire                       = UserConfig.Parser.TryGetBool(SECTION, "bNoFriendlyFire");
				NoFriendlyFireAOE                    = UserConfig.Parser.TryGetBool(SECTION, "bNoFriendlyFireAOE");
				Undetectable                         = UserConfig.Parser.TryGetBool(SECTION, "bUndetectable");
				UndetectableStealthAttacks           = UserConfig.Parser.TryGetBool(SECTION, "bUndetectableStealthAttacks");
				UnlockCookingRecipes                 = UserConfig.Parser.TryGetBool(SECTION, "bUnlockCookingRecipes");
			}
		}

		[NewType]
		public static class CompanionCost
		{
			private const string SECTION = "Game.CompanionCost";

			/// <summary>
			/// When true, CustomCompanionBaseCost will be multiplied by Party Level. (CustomCompanionBaseCost * PartyLevel)
			/// </summary>
			public static bool UsePartyLevelAsCostMultiplier { get; set; }

			/// <summary>
			/// When true, CustomCompanionBaseCost will be multiplied by CostMultiplier. (CustomCompanionBaseCost * CostMultiplier)
			/// </summary>
			public static bool UseMultiplier { get; set; }

			/// <summary>
			/// CustomCompanionBaseCost will be multiplied by CostMultiplier. (CustomCompanionBaseCost * CostMultiplier)
			/// </summary>
			public static int CostMultiplier { get; set; }

			static CompanionCost()
			{
				UsePartyLevelAsCostMultiplier = UserConfig.Parser.TryGetBool(SECTION, "bUsePartyLevelAsCostMultiplier");
				UseMultiplier                 = UserConfig.Parser.TryGetBool(SECTION, "bUseMultiplier");
				CostMultiplier                = UserConfig.Parser.TryGetInt(SECTION, "iCostMultiplier", 0);
			}
		}

		[NewType]
		public static class CurrencyFallback
		{
			private const string SECTION = "Game.KingdomEvents";

			public static bool Enabled { get; set; }

			public static int CurrencyMultiplier { get; set; }

			static CurrencyFallback()
			{
				Enabled            = UserConfig.Parser.TryGetBool(SECTION, "bCurrencyFallback");
				CurrencyMultiplier = UserConfig.Parser.TryGetInt(SECTION, "iCurrencyMultiplier", 80);
			}
		}

		[NewType]
		public static class DeltaTime
		{
			private const string SECTION = "Game.DeltaTime";

			/// <summary>
			/// When true, you can speed up the game (DeltaTime) in combat, out of combat, and on the global map by the following multipliers.
			/// </summary>
			public static bool Enabled { get; set; }

			public static float CombatMultiplier { get; set; }
			public static float GlobalMapMultiplier { get; set; }
			public static float OutOfCombatMultiplier { get; set; }

			static DeltaTime()
			{
				Enabled               = UserConfig.Parser.TryGetBool(SECTION, "bEnabled");
				CombatMultiplier      = UserConfig.Parser.TryGetFloat(SECTION, "fCombatMultiplier", 1f);
				GlobalMapMultiplier   = UserConfig.Parser.TryGetFloat(SECTION, "fGlobalMapMultiplier", 1f);
				OutOfCombatMultiplier = UserConfig.Parser.TryGetFloat(SECTION, "fOutOfCombatMultiplier", 1f);
			}
		}

		[NewType]
		public static class Game
		{
			private const string SECTION = "Game";

			/// <summary>
			/// When true, after you camp successfully, your party won't leave behind an inactive campsite object.
			/// </summary>
			public static bool CampsiteCleanup { get; set; }

			/// <summary>
			/// When true, the game history log will be disabled, reducing the size of statistic.json. Disabling history
			/// potentially improves loading speed; however, this history is used by Owlcat Games for debugging.
			/// </summary>
			public static bool DisableHistory { get; set; }

			/// <summary>
			/// When true, the debug log will be written to "%USERPROFILE%\AppData\LocalLow\Owlcat Games\Pathfinder Kingmaker".
			/// </summary>
			public static bool Logging { get; set; }

			/// <summary>
			/// When true, the selected party members will move as fast as the fastest of the selected party members.
			/// </summary>
			public static bool NoManLeftBehind { get; set; }

			/// <summary>
			/// When true, your party can rest anywhere, except during combat and special locations.
			/// </summary>
			public static bool RestAnywhere { get; set; }

			/// <summary>
			/// When true, premium DLC portraits will be unlocked.
			/// </summary>
			public static bool UnlockPremiumPortraits { get; set; }

			static Game()
			{
				CampsiteCleanup        = UserConfig.Parser.TryGetBool(SECTION, "bCampsiteCleanup");
				DisableHistory         = UserConfig.Parser.TryGetBool(SECTION, "bDisableHistory");
				Logging                = UserConfig.Parser.TryGetBool(SECTION, "bLogging");
				NoManLeftBehind        = UserConfig.Parser.TryGetBool(SECTION, "bNoManLeftBehind");
				RestAnywhere           = UserConfig.Parser.TryGetBool(SECTION, "bRestAnywhere");
				UnlockPremiumPortraits = UserConfig.Parser.TryGetBool(SECTION, "bUnlockPremiumPortraits");
			}
		}

		[NewType]
		public static class HighlightObjects
		{
			private const string SECTION = "UI.HighlightObjectsToggle";

			/// <summary>
			/// When true, pressing the Tab key will toggle object highlighting on/off until the key is pressed again.
			/// Highlighting will automatically toggle off during combat and area transitions.
			/// </summary>
			public static bool Enabled { get; set; }

			/// <summary>
			/// If you experience lag while highlighting is enabled, increase this value. This rate controls how fast
			/// overtips (e.g., nameplates) are updated for newly highlighted units.
			/// </summary>
			public static int SecondsBetweenTicksGameTime { get; set; }

			static HighlightObjects()
			{
				Enabled                     = UserConfig.Parser.TryGetBool(SECTION, "bEnabled");
				SecondsBetweenTicksGameTime = UserConfig.Parser.TryGetInt(SECTION, "iSecondsBetweenTicksGameTime", 1);
			}
		}

		[NewType]
		public static class KingdomAlignment
		{
			private const string SECTION = "Cheats.KingdomAlignment";

			public static bool Enabled { get; set; }

			public static string Alignment { get; set; }

			static KingdomAlignment()
			{
				Enabled   = UserConfig.Parser.TryGetBool(SECTION, "bEnabled");
				Alignment = UserConfig.Parser.TryGetString(SECTION, "sAlignment", "trueneutral");
			}
		}

		[NewType]
		public static class KingdomEvents
		{
			private const string SECTION = "Cheats.KingdomEvents";

			/// <summary>
			/// When true, you immediately complete kingdom events after starting them. Affects only events requiring leaders.
			/// For events already in progress, open and close the respective event cards to complete those events.
			/// </summary>
			public static bool InstantComplete { get; set; }

			/// <summary>
			/// When true, the outcome of completed events will be Triumph. If the current leader type cannot achieve Triumph,
			/// the outcome will be Success. If the leader type cannot achieve Success, you will get the Auto Resolve outcome.
			/// </summary>
			public static bool MaximumEffort { get; set; }

			static KingdomEvents()
			{
				InstantComplete = UserConfig.Parser.TryGetBool(SECTION, "bInstantComplete");
				MaximumEffort   = UserConfig.Parser.TryGetBool(SECTION, "bMaximumEffort");
			}
		}

		[NewType]
		public static class PointBuy
		{
			private const string SECTION = "Game.PointBuy";

			/// <summary>
			/// When true, at character creation, the player's attribute points will be equal to PlayerAttributePoints
			/// and the custom companion's attribute points will be equal to CompanionAttributePoints.
			/// </summary>
			public static bool Enabled;

			public static int PlayerAttributePoints { get; set; }
			public static int CompanionAttributePoints { get; set; }

			static PointBuy()
			{
				Enabled                  = UserConfig.Parser.TryGetBool(SECTION, "bEnabled");
				PlayerAttributePoints    = UserConfig.Parser.TryGetInt(SECTION, "iPlayerAttributePoints", 25, true, 20, int.MaxValue);
				CompanionAttributePoints = UserConfig.Parser.TryGetInt(SECTION, "iCompanionAttributePoints", 20, true, 20, int.MaxValue);
			}
		}

		[NewType]
		public static class RandomEncounters
		{
			private const string SECTION = "Game.RandomEncounters";

			public static bool Enabled { get; set; }
			public static bool EncountersEnabled { get; set; }
			public static float ChanceOnGlobalMap { get; set; }
			public static float ChanceOnCamp { get; set; }
			public static float ChanceOnCampSecondTime { get; set; }
			public static float HardEncounterChance { get; set; }
			public static float HardEncounterMaxChance { get; set; }
			public static float HardEncounterChanceIncrease { get; set; }
			public static float RollMiles { get; set; }
			public static float SafeMilesAfterEncounter { get; set; }
			public static int EncounterMinBonusCR { get; set; }
			public static int EncounterMaxBonusCR { get; set; }
			public static int HardEncounterBonusCR { get; set; }
			public static float DefaultSafeZoneSize { get; set; }
			public static int RandomEncounterAvoidanceFailMargin { get; set; }
			public static float EncounterPawnOffset { get; set; }
			public static float EncounterPawnDistanceFromLocation { get; set; }
			public static float StalkerAmbushChance { get; set; }

			static RandomEncounters()
			{
				Enabled                            = UserConfig.Parser.TryGetBool(SECTION, "bEnabled");
				EncountersEnabled                  = UserConfig.Parser.TryGetBool(SECTION, "bEncountersEnabled", true);
				ChanceOnGlobalMap                  = UserConfig.Parser.TryGetFloat(SECTION, "fChanceOnGlobalMap", 0.3f, true);
				ChanceOnCamp                       = UserConfig.Parser.TryGetFloat(SECTION, "fChanceOnCamp", 0.4f, true);
				ChanceOnCampSecondTime             = UserConfig.Parser.TryGetFloat(SECTION, "fChanceOnCampSecondTime", 0.1f, true);
				HardEncounterChance                = UserConfig.Parser.TryGetFloat(SECTION, "fHardEncounterChance", 0.05f, true);
				HardEncounterMaxChance             = UserConfig.Parser.TryGetFloat(SECTION, "fHardEncounterMaxChance", 0.9f, true);
				HardEncounterChanceIncrease        = UserConfig.Parser.TryGetFloat(SECTION, "fHardEncounterChanceIncrease", 0.05f, true);
				StalkerAmbushChance                = UserConfig.Parser.TryGetFloat(SECTION, "fStalkerAmbushChance", 0.25f, true);
				RollMiles                          = UserConfig.Parser.TryGetFloat(SECTION, "fRollMiles", 8f);
				SafeMilesAfterEncounter            = UserConfig.Parser.TryGetFloat(SECTION, "fSafeMilesAfterEncounter", 16f);
				EncounterMinBonusCR                = UserConfig.Parser.TryGetInt(SECTION, "iEncounterMinBonusCR");
				EncounterMaxBonusCR                = UserConfig.Parser.TryGetInt(SECTION, "iEncounterMaxBonusCR", 1);
				HardEncounterBonusCR               = UserConfig.Parser.TryGetInt(SECTION, "iHardEncounterBonusCR", 3);
				DefaultSafeZoneSize                = UserConfig.Parser.TryGetFloat(SECTION, "fDefaultSafeZoneSize", 4f);
				RandomEncounterAvoidanceFailMargin = UserConfig.Parser.TryGetInt(SECTION, "iRandomEncounterAvoidanceFailMargin", 5);
				EncounterPawnOffset                = UserConfig.Parser.TryGetFloat(SECTION, "fEncounterPawnOffset", 1.5f);
				EncounterPawnDistanceFromLocation  = UserConfig.Parser.TryGetFloat(SECTION, "fEncounterPawnDistanceFromLocation", 1.5f);
			}
		}

		[NewType]
		public static class Restrictions
		{
			private const string SECTION = "Cheats.Restrictions";

			/// <summary>
			/// When true, alignment-restricted dialogue answers can be freely selected without alignment restrictions.
			/// </summary>
			public static bool IgnoreDialogueAlignmentRestrictions { get; set; }

			/// <summary>
			/// When true, alignment-restricted equipment can be freely equipped without alignment restrictions.
			/// </summary>
			public static bool IgnoreEquipmentAlignmentRestrictions { get; set; }

			/// <summary>
			/// When true, class-restricted equipment can be freely equipped without class restrictions.
			/// </summary>
			public static bool IgnoreEquipmentClassRestrictions { get; set; }

			/// <summary>
			/// When true, stat-restricted equipment can be freely equipped without stat restrictions.
			/// </summary>
			public static bool IgnoreEquipmentStatRestrictions { get; set; }

			/// <summary>
			/// When true, settlement building restrictions will be removed. These include: Adjacency,
			/// Alignment, Artisan, Capital Level, Lone Slot, Other Building, and Stat Rank restrictions.
			/// </summary>
			public static bool IgnoreBuildingRestrictions { get; set; }

			static Restrictions()
			{
				IgnoreBuildingRestrictions           = UserConfig.Parser.TryGetBool(SECTION, "bIgnoreBuildingRestrictions");
				IgnoreDialogueAlignmentRestrictions  = UserConfig.Parser.TryGetBool(SECTION, "bIgnoreDialogueAlignmentRestrictions");
				IgnoreEquipmentAlignmentRestrictions = UserConfig.Parser.TryGetBool(SECTION, "bIgnoreEquipmentAlignmentRestrictions");
				IgnoreEquipmentClassRestrictions     = UserConfig.Parser.TryGetBool(SECTION, "bIgnoreEquipmentClassRestrictions");
				IgnoreEquipmentStatRestrictions      = UserConfig.Parser.TryGetBool(SECTION, "bIgnoreEquipmentStatRestrictions");
			}
		}

		[NewType]
		public static class UI
		{
			private const string SECTION = "UI";

			/// <summary>
			/// When true, the name of the target spellbook will be appended to the "Copy to Spellbook" action.
			/// </summary>
			public static bool AddSpellbookNameToCopyScrollAction { get; set; }

			/// <summary>
			/// When true, the "Sell: " label will be prepended to the item sell price on the item tooltip.
			/// </summary>
			public static bool AddLabelToSellCost { get; set; }

			static UI()
			{
				AddSpellbookNameToCopyScrollAction = UserConfig.Parser.TryGetBool(SECTION, "bAddSpellbookNameToCopyScrollAction");
				AddLabelToSellCost                 = UserConfig.Parser.TryGetBool(SECTION, "bAddLabelToSellCost");
			}
		}

		[NewType]
		public static class XPGain
		{
			private const string SECTION = "Cheats.XPGain";

			public static bool Enabled { get; set; }

			public static float XPMultiplier { get; set; }

			static XPGain()
			{
				Enabled      = UserConfig.Parser.TryGetBool(SECTION, "bEnabled");
				XPMultiplier = UserConfig.Parser.TryGetFloat(SECTION, "fXPMultiplier", 1f);
			}
		}
	}
}

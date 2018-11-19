using System;
using System.Collections.Generic;
using System.Reflection;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Patchwork;

namespace KingmakerMods.Mods.Game.Configurables.PointBuy
{
	[ModifiesType]
	public class LevelUpStateNew : LevelUpState
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		private static int _pointBuyPlayer;

		[NewMember]
		private static int _pointBuyCompanion;

		[ToggleFieldAttributes(FieldAttributes.InitOnly)]
		[ModifiesAccessibility("m_Unit")]
		private UnitDescriptor mod_m_Unit;

		[ToggleFieldAttributes(FieldAttributes.InitOnly)]
		[ModifiesAccessibility("NextLevel")]
		public int mod_NextLevel;

		[ToggleFieldAttributes(FieldAttributes.InitOnly)]
		[ModifiesAccessibility("IsCharGen")]
		public bool mod_IsCharGen;

		[ToggleFieldAttributes(FieldAttributes.InitOnly)]
		[ModifiesAccessibility("Selections")]
		public List<FeatureSelectionState> mod_Selections;

		[ToggleFieldAttributes(FieldAttributes.InitOnly)]
		[ModifiesAccessibility("SpellSelections")]
		public List<SpellSelectionData> mod_SpellSelections;

		[MemberAlias(".ctor", typeof(object))]
		private void object_ctor()
		{
		}

		[ModifiesMember(".ctor")]
		public void CtorNew(UnitDescriptor unit, bool isPregen)
		{
			this.object_ctor();

			this.mod_Selections = new List<FeatureSelectionState>();
			this.mod_SpellSelections = new List<SpellSelectionData>();
			this.AlignmentRestriction = new AlignmentRestriction();
			this.StatsDistribution = new StatsDistribution();

			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game.PointBuy", "bEnabled");
				_pointBuyPlayer = UserConfig.Parser.GetValueAsInt("Game.PointBuy", "iPlayerAttributePoints");
				_pointBuyCompanion = UserConfig.Parser.GetValueAsInt("Game.PointBuy", "iCompanionAttributePoints");
			}

			if (!_useMod)
			{
				_pointBuyPlayer = 25;
				_pointBuyCompanion = 20;
			}

			this.mod_m_Unit = unit;
			this.mod_NextLevel = unit.Progression.CharacterLevel + 1;

			if (this.mod_NextLevel == 1)
			{
				this.mod_IsCharGen = true;

				if (!isPregen)
				{
					int pointCount = unit.IsCustomCompanion() ? _pointBuyCompanion : _pointBuyPlayer;
					this.StatsDistribution.Start(pointCount);
				}

				this.CanSelectPortrait = !isPregen;
				this.CanSelectRace = true;
				this.CanSelectGender = !isPregen;
				this.CanSelectAlignment = true;
				this.CanSelectName = true;
				this.CanSelectVoice = true;
			}
			else
			{
				this.mod_IsCharGen = false;
			}

			if (this.mod_NextLevel % 4 == 0)
			{
				this.AttributePoints = 1;
			}
		}

		[Obsolete]
		public LevelUpStateNew(UnitDescriptor unit, bool isPregen) : base(unit, isPregen)
		{
		}
	}
}

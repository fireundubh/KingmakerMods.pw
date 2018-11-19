using System.Collections.Generic;
using System.Linq;
using Kingmaker.Designers;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.NoFriendlyFireAOE
{
	[ModifiesType]
	public class AbilityTargetsAroundNew : AbilityTargetsAround
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[ModifiesAccessibility("m_TargetType")]
		public TargetType m_TargetTypeNew;

		[ModifiesAccessibility("m_Condition")]
		public ConditionsChecker m_ConditionNew;

		[NewMember]
		private bool HasConditions(MechanicsContext context, UnitEntityData unit)
		{
			using (context.GetDataScope(unit))
			{
				return this.m_ConditionNew.Check();
			}
		}

		[NewMember]
		[DuplicatesBody("Select")]
		public IEnumerable<TargetWrapper> source_Select(AbilityExecutionContext context, TargetWrapper anchor)
		{
			throw new DeadEndException("source_Select");
		}

		[ModifiesMember("Select", ModificationScope.Body)]
		public IEnumerable<TargetWrapper> mod_Select(AbilityExecutionContext context, TargetWrapper anchor)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bNoFriendlyFireAOE");
			}

			if (!_useMod)
			{
				return this.source_Select(context, anchor);
			}

			UnitEntityData caster = context.MaybeCaster;

			IEnumerable<UnitEntityData> targets = GameHelper.GetTargetsAround(anchor.Point, this.AoERadius);

			if (caster == null)
			{
				UberDebug.LogError("Caster is missing");
				return Enumerable.Empty<TargetWrapper>();
			}

			if (this.m_TargetTypeNew == TargetType.Enemy)
			{
				targets = targets.Where(caster.IsEnemy);
			}
			else if (this.m_TargetTypeNew == TargetType.Ally)
			{
				targets = targets.Where(caster.IsAlly);
			}

			if (this.m_ConditionNew.HasConditions)
			{
				targets = targets.Where(u => this.HasConditions(context, u)).ToList();
			}

			if (caster.IsPlayerFaction && context.AbilityBlueprint.EffectOnEnemy == AbilityEffectOnUnit.Harmful)
			{
				targets = targets.Where(target => !target.IsPlayerFaction);
			}

			return targets.Select(target => new TargetWrapper(target));
		}
	}
}

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
		[ModifiesAccessibility("m_TargetType")]
		public TargetType mod_m_TargetType;

		[ModifiesAccessibility("m_Condition")]
		public ConditionsChecker mod_m_Condition;

		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("Select")]
		public IEnumerable<TargetWrapper> source_Select(AbilityExecutionContext context, TargetWrapper anchor)
		{
			throw new DeadEndException("source_Select");
		}
		#endregion

		[ModifiesMember("Select", ModificationScope.Body)]
		public IEnumerable<TargetWrapper> mod_Select(AbilityExecutionContext context, TargetWrapper anchor)
		{
			if (!KingmakerPatchSettings.Cheats.NoFriendlyFireAOE)
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

			switch (this.mod_m_TargetType)
			{
				case TargetType.Enemy:
					targets = targets.Where(caster.IsEnemy);
					break;
				case TargetType.Ally:
					targets = targets.Where(caster.IsAlly);
					break;
			}

			if (this.mod_m_Condition.HasConditions)
			{
				targets = targets.Where(u => this.HasConditions(context, u)).ToList();
			}

			if (caster.IsPlayerFaction && context.AbilityBlueprint.EffectOnEnemy == AbilityEffectOnUnit.Harmful)
			{
				targets = targets.Where(target => !target.IsPlayerFaction);
			}

			return targets.Select(target => new TargetWrapper(target));
		}

		[NewMember]
		private bool HasConditions(MechanicsContext context, UnitEntityData unit)
		{
			using (context.GetDataScope(unit))
			{
				return this.mod_m_Condition.Check();
			}
		}
	}
}

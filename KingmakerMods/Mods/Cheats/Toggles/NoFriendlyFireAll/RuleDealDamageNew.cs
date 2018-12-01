using System;
using JetBrains.Annotations;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.NoFriendlyFireAll
{
	[ModifiesType]
	public class RuleDealDamageNew : RuleDealDamage
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("ApplyDifficultyModifiers")]
		private int source_ApplyDifficultyModifiers(int damage)
		{
			throw new DeadEndException("source_ApplyDifficultyModifiers");
		}
		#endregion

		[ModifiesMember("ApplyDifficultyModifiers")]
		private int mod_ApplyDifficultyModifiers(int damage)
		{
			if (!KingmakerPatchSettings.Cheats.NoFriendlyFire)
			{
				return this.source_ApplyDifficultyModifiers(damage);
			}

			BlueprintScriptableObject blueprint = this.Reason.Context?.AssociatedBlueprint;

			if (blueprint is BlueprintBuff)
			{
				return this.source_ApplyDifficultyModifiers(damage);
			}

			BlueprintAbility blueprintAbility = this.Reason.Context?.SourceAbility;

			if (blueprintAbility == null)
			{
				return this.source_ApplyDifficultyModifiers(damage);
			}

			if (!this.Initiator.IsPlayerFaction || !this.Target.IsPlayerFaction)
			{
				return this.source_ApplyDifficultyModifiers(damage);
			}

			if (blueprintAbility.EffectOnAlly == AbilityEffectOnUnit.Harmful || blueprintAbility.EffectOnEnemy == AbilityEffectOnUnit.Harmful)
			{
				return 0;
			}

			return this.source_ApplyDifficultyModifiers(damage);
		}

		[Obsolete]
		public RuleDealDamageNew([NotNull] UnitEntityData initiator, [NotNull] UnitEntityData target, DamageBundle damage) : base(initiator, target, damage)
		{
		}
	}
}

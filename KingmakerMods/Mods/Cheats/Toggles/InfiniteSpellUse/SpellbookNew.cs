using System;
using JetBrains.Annotations;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.InfiniteSpellUse
{
	[ModifiesType]
	public class SpellbookNew : Spellbook
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("SpendInternal")]
		private bool source_SpendInternal([NotNull] BlueprintAbility blueprint, [CanBeNull] AbilityData spell, bool doSpend, bool excludeSpecial = false)
		{
			throw new DeadEndException("source_SpendInternal");
		}

		[ModifiesMember("Spend")]
		public bool mod_Spend(AbilityData spell, bool excludeSpecial = false)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bInfiniteSpellUse");
			}

			if (!_useMod)
			{
				return this.source_SpendInternal(spell.Blueprint, spell, true, excludeSpecial);
			}

			return this.source_SpendInternal(spell.Blueprint, spell, !spell.Caster.Unit.IsPlayerFaction, excludeSpecial);
		}

		[Obsolete]
		public SpellbookNew(UnitDescriptor owner, BlueprintSpellbook blueprint) : base(owner, blueprint)
		{
		}
	}
}

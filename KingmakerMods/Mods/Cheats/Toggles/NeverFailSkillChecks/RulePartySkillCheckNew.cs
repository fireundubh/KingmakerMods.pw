using System;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem.Rules;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.NeverFailSkillChecks
{
	[ModifiesType]
	public class RulePartySkillCheckNew : RulePartySkillCheck
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("get_IsPassed")]
		public bool source_get_IsPassed()
		{
			throw new DeadEndException("source_get_IsPassed");
		}
		#endregion

		[ModifiesMember("IsPassed")]
		public bool mod_IsPassed
		{
			[ModifiesMember("get_IsPassed")]
			get
			{
				// ReSharper disable once ConvertIfStatementToReturnStatement
				if (KingmakerPatchSettings.Cheats.NeverFailSkillChecks)
				{
					return true;
				}

				return this.source_get_IsPassed();
			}
		}

		[Obsolete]
		public RulePartySkillCheckNew(StatType statType, int difficultyClass) : base(statType, difficultyClass)
		{
		}
	}
}

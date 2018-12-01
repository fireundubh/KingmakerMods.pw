using JetBrains.Annotations;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UI.SettingsUI;
using KingmakerMods.Helpers;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Cheats.Configurables.XPGain
{
	[ModifiesType("Kingmaker.Designers.GameHelper")]
	public static class GameHelperNew
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("GainExperience")]
		public static void source_GainExperience(int gained, UnitEntityData unit = null)
		{
			throw new DeadEndException("source_GainExperience");
		}

		[NewMember]
		[DuplicatesBody("GainExperienceForSkillCheck")]
		public static void source_GainExperienceForSkillCheck(int gained, [NotNull] UnitEntityData unit)
		{
			throw new DeadEndException("source_GainExperienceForSkillCheck");
		}
		#endregion

		[ModifiesMember("GainExperience")]
		public static void mod_GainExperience(int gained, UnitEntityData unit = null)
		{
			if (!KingmakerPatchSettings.XPGain.Enabled)
			{
				source_GainExperience(gained, unit);
				return;
			}

			if (unit != null && SettingsRoot.Instance.OnlyActiveCompanionsReceiveExperience.CurrentValue)
			{
				if (unit.IsDirectlyControllable)
				{
					gained = Mathf.RoundToInt(gained * KingmakerPatchSettings.XPGain.XPMultiplier);
				}

				unit.Descriptor.Progression.GainExperience(gained * 6, true);
			}
			else
			{
				Kingmaker.Game.Instance.Player.GainPartyExperience(Mathf.RoundToInt(gained * KingmakerPatchSettings.XPGain.XPMultiplier));
			}
		}

		[ModifiesMember("GainExperienceForSkillCheck")]
		public static void mod_GainExperienceForSkillCheck(int gained, [NotNull] UnitEntityData unit)
		{
			if (!KingmakerPatchSettings.XPGain.Enabled)
			{
				source_GainExperienceForSkillCheck(gained, unit);
				return;
			}

			if (SettingsRoot.Instance.OnlyInitiatorReceiveSkillCheckExperience.CurrentValue)
			{
				if (unit.IsDirectlyControllable)
				{
					gained = Mathf.RoundToInt(gained * KingmakerPatchSettings.XPGain.XPMultiplier);
				}

				unit.Descriptor.Progression.GainExperience(gained * 6, true);
			}
			else
			{
				Kingmaker.Game.Instance.Player.GainPartyExperience(Mathf.RoundToInt(gained * KingmakerPatchSettings.XPGain.XPMultiplier));
			}
		}
	}
}

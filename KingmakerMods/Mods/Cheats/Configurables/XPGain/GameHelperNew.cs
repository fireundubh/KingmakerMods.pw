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
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		private static float _xpMultiplier;

		[NewMember]
		private static bool IsModReady()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats.XPGain", "bEnabled");
				_xpMultiplier = UserConfig.Parser.GetValueAsFloat("Cheats.XPGain", "fXPMultiplier");
			}

			return _useMod;
		}

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

		[ModifiesMember("GainExperience")]
		public static void mod_GainExperience(int gained, UnitEntityData unit = null)
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				source_GainExperience(gained, unit);
				return;
			}

			if (unit != null && SettingsRoot.Instance.OnlyActiveCompanionsReceiveExperience.CurrentValue)
			{
				if (unit.IsDirectlyControllable)
				{
					gained = Mathf.RoundToInt(gained * _xpMultiplier);
				}

				unit.Descriptor.Progression.GainExperience(gained * 6, true);
			}
			else
			{
				Kingmaker.Game.Instance.Player.GainPartyExperience(Mathf.RoundToInt(gained * _xpMultiplier));
			}
		}

		[ModifiesMember("GainExperienceForSkillCheck")]
		public static void mod_GainExperienceForSkillCheck(int gained, [NotNull] UnitEntityData unit)
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				source_GainExperienceForSkillCheck(gained, unit);
				return;
			}

			if (SettingsRoot.Instance.OnlyInitiatorReceiveSkillCheckExperience.CurrentValue)
			{
				if (unit.IsDirectlyControllable)
				{
					gained = Mathf.RoundToInt(gained * _xpMultiplier);
				}

				unit.Descriptor.Progression.GainExperience(gained * 6, true);
			}
			else
			{
				Kingmaker.Game.Instance.Player.GainPartyExperience(Mathf.RoundToInt(gained * _xpMultiplier));
			}
		}
	}
}

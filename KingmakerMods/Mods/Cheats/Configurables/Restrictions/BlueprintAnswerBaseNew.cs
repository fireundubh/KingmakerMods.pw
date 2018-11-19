using Kingmaker.DialogSystem.Blueprints;
using Kingmaker.Enums;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Configurables.Restrictions
{
	[ModifiesType]
	public abstract class BlueprintAnswerBaseNew : BlueprintAnswerBase
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		public bool mod_IsAlignmentRequirementSatisfied
		{
			[ModifiesMember("get_IsAlignmentRequirementSatisfied")]
			get
			{
				if (!_cfgInit)
				{
					_cfgInit = true;
					_useMod = UserConfig.Parser.GetValueAsBool("Cheats.Restrictions", "bIgnoreDialogueAlignmentRestrictions");
				}

				if (_useMod)
				{
					return true;
				}

				return Kingmaker.Game.Instance.Player.Alignment.HasComponent(this.AlignmentRequirement);
			}
		}
	}
}

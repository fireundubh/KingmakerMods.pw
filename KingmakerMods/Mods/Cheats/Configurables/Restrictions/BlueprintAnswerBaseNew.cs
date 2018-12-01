using Kingmaker.DialogSystem.Blueprints;
using Kingmaker.Enums;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Configurables.Restrictions
{
	[ModifiesType]
	public abstract class BlueprintAnswerBaseNew : BlueprintAnswerBase
	{
		[ModifiesMember("IsAlignmentRequirementSatisfied")]
		public bool mod_IsAlignmentRequirementSatisfied
		{
			[ModifiesMember("get_IsAlignmentRequirementSatisfied")]
			get
			{
				if (KingmakerPatchSettings.Restrictions.IgnoreDialogueAlignmentRestrictions)
				{
					return true;
				}

				return Kingmaker.Game.Instance.Player.Alignment.HasComponent(this.AlignmentRequirement);
			}
		}
	}
}

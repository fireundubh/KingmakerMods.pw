using Kingmaker.UI.Common;
using Kingmaker.UI.Tooltip;
using Kingmaker.UnitLogic;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.AlwaysUnencumbered
{
	[ModifiesType]
	public class DescriptionTemplatesEncumbranceNew : DescriptionTemplatesEncumbrance
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("EncumbrancePartyPenalty")]
		public void source_EncumbrancePartyPenalty(DescriptionBricksBox box, EncumbranceHelper.CarryingCapacity capacity)
		{
			throw new DeadEndException("source_EncumbrancePartyPenalty");
		}

		[ModifiesMember("EncumbrancePartyPenalty")]
		public void mod_EncumbrancePartyPenalty(DescriptionBricksBox box, EncumbranceHelper.CarryingCapacity capacity)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bAlwaysUnencumbered");
			}

			if (!_useMod)
			{
				this.source_EncumbrancePartyPenalty(box, capacity);
				return;
			}

			box.Add(DescriptionTemplatesBase.Bricks.Separator3);
			box.Add(DescriptionTemplatesBase.Bricks.TitleH3).SetText(UIUtility.GetGlossaryEntryName("NoPenalty"));
		}
	}
}

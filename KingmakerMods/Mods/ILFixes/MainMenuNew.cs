using Kingmaker;
using Kingmaker.UI;
using KingmakerMods.Helpers;
using Patchwork;

// [IL]: Error: [Assembly-CSharp : Kingmaker.MainMenu::OnEnableGameStatistic][offset 0x00000069] Unable to resolve token.(Error: 0x80131869)

namespace KingmakerMods.Mods.ILFixes
{
	[ModifiesType]
	public class MainMenuNew : MainMenu
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("OnEnableGameStatistic")]
		private void source_OnEnableGameStatistic(DialogMessageBox.BoxButton button)
		{
			throw new DeadEndException("source_OnEnableGameStatistic");
		}
		#endregion

		[ModifiesMember("OnEnableGameStatistic")]
		private void OnEnableGameStatistic(DialogMessageBox.BoxButton button)
		{
			this.source_OnEnableGameStatistic(button);
		}
	}
}

using Kingmaker;
using Kingmaker.UI;
using Kingmaker.UI.SettingsUI;
using Patchwork;

namespace KingmakerMods.Mods.ILFixes
{
	[ModifiesType]
	public class MainMenuNew : MainMenu
	{
		// [IL]: Error: [Assembly-CSharp : Kingmaker.MainMenu::OnEnableGameStatistic][offset 0x00000069] Unable to resolve token.(Error: 0x80131869)

		[ModifiesMember("OnEnableGameStatistic")]
		private void OnEnableGameStatistic(DialogMessageBox.BoxButton button)
		{
//			PlayerPrefs.SetInt(DataPrivacy.GameStatisticKeyString, button != DialogMessageBox.BoxButton.Yes ? 0 : 1);
			SettingsRoot.Instance.GameStatistic.CurrentValue = button == DialogMessageBox.BoxButton.Yes;
			SettingsRoot.Instance.GameStatistic.DefaultValue = button == DialogMessageBox.BoxButton.Yes;
			SettingsRoot.Instance.SendSaves.CurrentValue = button == DialogMessageBox.BoxButton.Yes;
			SettingsRoot.Instance.SendSaves.DefaultValue = button == DialogMessageBox.BoxButton.Yes;
//			DataPrivacy.Initialize();
//			DataPrivacy.get_instance().Start();
		}
	}
}

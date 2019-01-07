using Kingmaker.GameModes;
using Kingmaker.UI;
using Kingmaker.UI.Common;
using Kingmaker.UI.Group;
using KingmakerMods.Helpers;
using Patchwork;

// [131.6340 - System]: Started mode Default (previous mode None)
// [131.6410]: Object reference not set to an instance of an object
// at Kingmaker.UI.StaticCanvas.SetHUDVisible (Kingmaker.GameModes.GameModeType gameMode) [0x0008e] in <ceb6329168714a46b0261229a78ce28d>:0// at Kingmaker.UI.StaticCanvas.OnGameModeStart (Kingmaker.GameModes.GameModeType gameMode) [0x0001d] in <ceb6329168714a46b0261229a78ce28d>:0// at Kingmaker.Game+<HandleGameModeChanged>c__AnonStorey7.<>m__1 (Kingmaker.PubSubSystem.IGameModeHandler h) [0x00000] in <ceb6329168714a46b0261229a78ce28d>:0// at Kingmaker.PubSubSystem.SubscriptionManager`1[TSubscriber].RaiseEvent[T] (System.Action`1[T] action) [0x000a5] in <ceb6329168714a46b0261229a78ce28d>:0
namespace KingmakerMods.Mods.Fixes
{
	[ModifiesType]
	public class StaticCanvasNew : StaticCanvas
	{
		#region Fields

		[ModifiesMember("HUDController", ModificationScope.Nothing)]
		public UISectionHUDController alias_HUDController;

		[ModifiesMember("m_FullScreenUiShown", ModificationScope.Nothing)]
		private bool alias_m_FullScreenUiShown;

		#endregion

		#region Methods

		[ModifiesMember("SetHUDVisible")]
		private void SetHUDVisible(GameModeType gameMode)
		{
			// GroupController.Instance can be null
			GroupController groupControllerInstance = GroupController.Instance;

			if (groupControllerInstance == null)
			{
				this.alias_HUDController.SetState(UISectionHUDController.HUDState.AllVisible);
				return;
			}

			switch (gameMode)
			{
				case GameModeType.Dialog:
				case GameModeType.Cutscene:
					this.alias_HUDController.SetState(UISectionHUDController.HUDState.Hidden);
					groupControllerInstance.HideAnimation(true);
					break;
				case GameModeType.FullScreenUi:
					this.alias_HUDController.SetState(UISectionHUDController.HUDState.Hidden);
					groupControllerInstance.CheckVisibleByType();
					break;
				case GameModeType.EscMode:
					break;
				case GameModeType.Rest:
					this.alias_HUDController.SetState(UISectionHUDController.HUDState.OnlyLog);
					groupControllerInstance.HideAnimation(true);
					break;
				default:
					this.alias_HUDController.SetState(UISectionHUDController.HUDState.AllVisible);
					groupControllerInstance.HideAnimation(this.alias_m_FullScreenUiShown);
					break;
			}
		}

		[NewMember]
		[DuplicatesBody("SetHUDVisible")]
		private void source_SetHUDVisible(GameModeType gameMode)
		{
			throw new DeadEndException("source_SetHUDVisible");
		}

		#endregion
	}
}

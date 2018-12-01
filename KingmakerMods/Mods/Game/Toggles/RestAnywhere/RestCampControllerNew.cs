using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Area;
using Kingmaker.Controllers.Clicks;
using Kingmaker.GameModes;
using Kingmaker.PubSubSystem;
using Kingmaker.UI;
using Kingmaker.UI.RestCamp;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Toggles.RestAnywhere
{
	[ModifiesType]
	public class RestCampControllerNew : RestCampController
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("ShowRestUI")]
		public static void source_ShowRestUI()
		{
			throw new DeadEndException("source_ShowRestUI");
		}
		#endregion

		[ModifiesMember("ShowRestUI")]
		public static void mod_ShowRestUI()
		{
			if (!KingmakerPatchSettings.Game.RestAnywhere)
			{
				source_ShowRestUI();
				return;
			}

			if (Kingmaker.Game.Instance.UnitGroupsController.Party.IsInCombat)
			{
				EventBus.RaiseEvent<IWarningNotificationUIHandler>(h => h.HandleWarning(WarningNotificationType.RestInCombatImpossible));
				return;
			}

			BlueprintArea currentlyLoadedArea = Kingmaker.Game.Instance.CurrentlyLoadedArea;

			currentlyLoadedArea.CampingSettings.CampingAllowed = true;

//			if (!currentlyLoadedArea.CampingSettings.CampingAllowed)
//			{
//				EventBus.RaiseEvent(delegate(IWarningNotificationUIHandler h) { h.HandleWarning(WarningNotificationType.RestOnThisZoneImpossible); });
//				return;
//			}

			var component = currentlyLoadedArea.GetComponent<OverrideCampingAction>();

			if (component)
			{
				component.OnRestActions.Run();

				if (component.SkipRest)
				{
					return;
				}
			}

			if (Kingmaker.Game.Instance.IsModeActive(GameModeType.FullScreenUi))
			{
				return;
			}

			if (Kingmaker.Game.Instance.IsModeActive(GameModeType.GlobalMap))
			{
				if (!Kingmaker.Game.Instance.IsModeActive(GameModeType.Pause) && !Kingmaker.Game.Instance.IsModeActive(GameModeType.FullScreenUi))
				{
					Kingmaker.Game.Instance.RestController.Start(null);
				}
			}
			else
			{
				Kingmaker.Game.Instance.ClickEventsController.SetPointerMode(PointerMode.RestMarker);
			}
		}
	}
}

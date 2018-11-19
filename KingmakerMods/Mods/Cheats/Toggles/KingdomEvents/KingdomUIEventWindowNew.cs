using Kingmaker.Blueprints;
using Kingmaker.Controllers.Rest;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Kingdom;
using Kingmaker.Kingdom.UI;
using Kingmaker.PubSubSystem;
using Kingmaker.UI.Kingdom;
using KingmakerMods.Helpers;
using Patchwork;

#pragma warning disable 169, 649

namespace KingmakerMods.Mods.Cheats.Toggles.KingdomEvents
{
	[ModifiesType]
	public class KingdomUIEventWindowNew : KingdomUIEventWindow
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[ModifiesMember("m_KingdomEventView", ModificationScope.Nothing)]
		private KingdomEventUIView mod_m_KingdomEventView;

		[ModifiesMember("m_Cart", ModificationScope.Nothing)]
		private KingdomEventHandCartController mod_m_Cart;

		[NewMember]
		[DuplicatesBody("OnClose")]
		public void source_OnClose()
		{
			throw new DeadEndException("source_OnClose");
		}

		[ModifiesMember("OnClose")]
		public void mod_OnClose()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats.KingdomEvents", "bInstantComplete");
			}

			if (!_useMod)
			{
				this.source_OnClose();
				return;
			}

			KingdomEventUIView previousView = this.mod_m_KingdomEventView;

			// deselects the view
			EventBus.RaiseEvent((IEventSceneHandler h) => h.OnEventSelected(null, this.mod_m_Cart));

			if (previousView == null)
			{
				return;
			}

			if (previousView.IsFinished || previousView.AssignedLeader == null || previousView.Blueprint.NeedToVisitTheThroneRoom)
			{
				return;
			}

			bool inProgress = previousView.IsInProgress;
			BlueprintUnit leader = previousView.AssignedLeader;

			if (!inProgress || leader == null)
			{
				return;
			}

			previousView.Event.Resolve(previousView.Task);

			if (previousView.RulerTimeRequired <= 0)
			{
				return;
			}

			foreach (UnitEntityData unitEntityData in Kingmaker.Game.Instance.Player.AllCharacters)
			{
				RestController.ApplyRest(unitEntityData.Descriptor);
			}
		}
	}
}

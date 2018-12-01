using Kingmaker.Controllers.Rest;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Kingdom;
using Kingmaker.Kingdom.Tasks;
using Kingmaker.Kingdom.UI;
using Kingmaker.PubSubSystem;
using Kingmaker.UI.Kingdom;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.KingdomEvents
{
	[ModifiesType]
	public class KingdomUIEventWindowFooterNew : KingdomUIEventWindowFooter
	{
		#region ALIASES
		[ModifiesMember("m_KingdomEventView", ModificationScope.Nothing)]
		private KingdomEventUIView alias_m_KingdomEventView;
		#endregion

		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("OnStart")]
		public void source_OnStart()
		{
			throw new DeadEndException("source_OnStart");
		}
		#endregion

		[ModifiesMember("OnStart")]
		public void mod_OnStart()
		{
			if (!KingmakerPatchSettings.KingdomEvents.InstantComplete)
			{
				this.source_OnStart();
				return;
			}

			KingdomEventUIView previousView = this.alias_m_KingdomEventView;

			EventBus.RaiseEvent((IKingdomUIStartSpendTimeEvent h) => h.OnStartSpendTimeEvent(previousView.Blueprint));

			KingdomTaskEvent task = previousView.Task;

			EventBus.RaiseEvent((IKingdomUICloseEventWindow h) => h.OnClose());

			task?.Start(false);

			if (task == null || task.IsFinished || task.AssignedLeader == null || previousView.Blueprint.NeedToVisitTheThroneRoom)
			{
				return;
			}

			task.Event.Resolve(task);

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

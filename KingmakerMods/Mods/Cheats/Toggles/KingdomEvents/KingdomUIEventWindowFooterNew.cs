using Kingmaker.Controllers.Rest;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Kingdom;
using Kingmaker.Kingdom.Tasks;
using Kingmaker.Kingdom.UI;
using Kingmaker.PubSubSystem;
using Kingmaker.UI.Kingdom;
using KingmakerMods.Helpers;
using Patchwork;

#pragma warning disable 169, 649

namespace KingmakerMods.Mods.Cheats.Toggles.KingdomEvents
{
	[ModifiesType]
	public class KingdomUIEventWindowFooterNew : KingdomUIEventWindowFooter
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[ModifiesMember("m_KingdomEventView", ModificationScope.Nothing)]
		private KingdomEventUIView mod_m_KingdomEventView;

		[NewMember]
		[DuplicatesBody("OnStart")]
		public void source_OnStart()
		{
			throw new DeadEndException("source_OnStart");
		}

		[ModifiesMember("OnStart")]
		public void mod_OnStart()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats.KingdomEvents", "bInstantComplete");
			}

			if (!_useMod)
			{
				this.source_OnStart();
				return;
			}

			KingdomEventUIView previousView = this.mod_m_KingdomEventView;

			EventBus.RaiseEvent((IKingdomUIStartSpendTimeEvent h) => h.OnStartSpendTimeEvent(previousView.Blueprint));

			KingdomTaskEvent task = previousView.Task;

			EventBus.RaiseEvent((IKingdomUICloseEventWindow h) => h.OnClose());

			if (task == null)
			{
				return;
			}

			task.Start(false);

			if (task.IsFinished || task.AssignedLeader == null || previousView.Blueprint.NeedToVisitTheThroneRoom)
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

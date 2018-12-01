using System;
using Kingmaker.Controllers.Rest;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Kingdom;
using Kingmaker.Kingdom.Tasks;
using Kingmaker.PubSubSystem;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Configurables.CurrencyFallback
{
	[ModifiesType]
	public abstract class KingdomTaskNew : KingdomTask
	{
		#region ALIASES
		[ModifiesMember("StartedOn", ModificationScope.Nothing)]
		public int alias_StartedOn
		{
			[ModifiesMember("get_StartedOn", ModificationScope.Nothing)]
			get;
			[ModifiesMember("set_StartedOn", ModificationScope.Nothing)]
			private set;
		}

		[ModifiesMember("IsStarted", ModificationScope.Nothing)]
		public bool alias_IsStarted
		{
			[ModifiesMember("get_IsStarted", ModificationScope.Nothing)]
			get;
			[ModifiesMember("set_IsStarted", ModificationScope.Nothing)]
			private set;
		}

		[ModifiesMember("OnTaskChanged", ModificationScope.Nothing)]
		private void alias_OnTaskChanged()
		{
			throw new DeadEndException("source_OnTaskChanged");
		}
		#endregion

		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("Start")]
		public virtual void source_Start(bool raiseEvent = true)
		{
			throw new DeadEndException("source_Start");
		}
		#endregion

		[ModifiesMember("Start")]
		public virtual void mod_Start(bool raiseEvent = true)
		{
			if (!KingmakerPatchSettings.CurrencyFallback.Enabled)
			{
				this.source_Start(raiseEvent);
				return;
			}

			this.alias_IsStarted = true;
			this.alias_StartedOn = KingdomState.Instance.CurrentDay;

			KingdomCurrencyFallback.SpendPoints(this.OneTimeBPCost);

			if (raiseEvent)
			{
				this.alias_OnTaskChanged();
			}

			EventBus.RaiseEvent((IKingdomTaskEventsHandler h) => h.OnTaskStarted(this));

			if (this.SkipPlayerTime <= 0)
			{
				return;
			}

			Kingmaker.Game.Instance.AdvanceGameTime(TimeSpan.FromDays(this.SkipPlayerTime));

			foreach (UnitEntityData unitEntityData in Kingmaker.Game.Instance.Player.AllCharacters)
			{
				RestController.ApplyRest(unitEntityData.Descriptor);
			}

			new KingdomTimelineManager().UpdateTimeline();
		}
	}
}

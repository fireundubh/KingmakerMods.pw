using System;
using Kingmaker.Controllers.Rest;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Kingdom;
using Kingmaker.Kingdom.Tasks;
using Kingmaker.PubSubSystem;
using KingmakerMods.Helpers;
using KingmakerMods.UserConfig;
using Patchwork;

namespace KingmakerMods.Mods.Game.Configurables.CurrencyFallback
{
	[ModifiesType]
	public abstract class KingdomTaskNew : KingdomTask
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		private static bool IsModReady()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = Parser.GetValueAsBool("Game.KingdomEvents", "bCurrencyFallback");
			}

			return _useMod;
		}

		[ModifiesMember("StartedOn", ModificationScope.Nothing)]
		public int source_StartedOn
		{
			[ModifiesMember("get_StartedOn", ModificationScope.Nothing)]
			get;
			[ModifiesMember("set_StartedOn", ModificationScope.Nothing)]
			private set;
		}

		[ModifiesMember("IsStarted", ModificationScope.Nothing)]
		public bool source_IsStarted
		{
			[ModifiesMember("get_IsStarted", ModificationScope.Nothing)]
			get;
			[ModifiesMember("set_IsStarted", ModificationScope.Nothing)]
			private set;
		}

		[ModifiesMember("OnTaskChanged", ModificationScope.Nothing)]
		private void source_OnTaskChanged()
		{
			throw new DeadEndException("source_OnTaskChanged");
		}

		[NewMember]
		[DuplicatesBody("Start")]
		public virtual void source_Start(bool raiseEvent = true)
		{
			throw new DeadEndException("source_Start");
		}

		[ModifiesMember("Start")]
		public virtual void mod_Start(bool raiseEvent = true)
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				this.source_Start(raiseEvent);
				return;
			}

			this.source_IsStarted = true;
			this.source_StartedOn = KingdomState.Instance.CurrentDay;

			KingdomCurrencyFallback.SpendPoints(this.OneTimeBPCost);

			if (raiseEvent)
			{
				this.source_OnTaskChanged();
			}

			EventBus.RaiseEvent((IKingdomTaskEventsHandler h) => h.OnTaskStarted(this));

			if (this.SkipPlayerTime > 0)
			{
				Kingmaker.Game.Instance.AdvanceGameTime(TimeSpan.FromDays(this.SkipPlayerTime));

				foreach (UnitEntityData unitEntityData in Kingmaker.Game.Instance.Player.AllCharacters)
				{
					RestController.ApplyRest(unitEntityData.Descriptor);
				}

				new KingdomTimelineManager().UpdateTimeline();
			}
		}
	}
}

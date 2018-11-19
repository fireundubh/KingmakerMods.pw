using System;
using JetBrains.Annotations;
using Kingmaker.Kingdom.Tasks;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.KingdomEvents
{
	[ModifiesType]
	public class KingdomTaskEventNew : KingdomTaskEvent
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
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats.KingdomEvents", "bInstantComplete");
			}

			return _useMod;
		}

		// Kingmaker.Kingdom.Tasks.KingdomTask.Start() checks if SkipPlayerTime is nonzero, and if so:
		//     1. advances game time
		//     2. applies rest to all characters
		//     3. updates timeline
		// With Instant Complete, we want Leader/Advisor events to complete the same way as other events.

		[NewMember]
		[DuplicatesBody("get_SkipPlayerTime")]
		public int source_get_SkipPlayerTime()
		{
			throw new DeadEndException("source_get_SkipPlayerTime");
		}

		[ModifiesMember("SkipPlayerTime")]
		public int mod_SkipPlayerTime
		{
			[ModifiesMember("get_SkipPlayerTime")]
			get
			{
				_useMod = IsModReady();

				if (!_useMod)
				{
					return this.source_get_SkipPlayerTime();
				}

				return 0;
			}
		}

		[Obsolete]
		public KingdomTaskEventNew([NotNull] KingdomEvent evt) : base(evt)
		{
		}
	}
}

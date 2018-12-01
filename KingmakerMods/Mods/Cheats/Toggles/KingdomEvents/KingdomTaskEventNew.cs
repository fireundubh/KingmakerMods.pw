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
		// Kingmaker.Kingdom.Tasks.KingdomTask.Start() checks if SkipPlayerTime is nonzero, and if so:
		//     1. advances game time
		//     2. applies rest to all characters
		//     3. updates timeline
		// With Instant Complete, we want Leader/Advisor events to complete the same way as other events.

		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("get_SkipPlayerTime")]
		public int source_get_SkipPlayerTime()
		{
			throw new DeadEndException("source_get_SkipPlayerTime");
		}
		#endregion

		[ModifiesMember("SkipPlayerTime")]
		public int mod_SkipPlayerTime
		{
			[ModifiesMember("get_SkipPlayerTime")]
			get
			{
				return KingmakerPatchSettings.KingdomEvents.InstantComplete ? 0 : this.source_get_SkipPlayerTime();
			}
		}

		[Obsolete]
		public KingdomTaskEventNew([NotNull] KingdomEvent evt) : base(evt)
		{
		}
	}
}

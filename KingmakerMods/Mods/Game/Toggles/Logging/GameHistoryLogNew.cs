using JetBrains.Annotations;
using Kingmaker;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Toggles.Logging
{
	[ModifiesType]
	public class GameHistoryLogNew : GameHistoryLog
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("AddMessage")]
		private static void source_AddMessage(GameHistoryChannel channel, [CanBeNull] UnityEngine.Object context, string message)
		{
			throw new DeadEndException("source_AddMessage");
		}
		#endregion

		[ModifiesMember("AddMessage")]
		private static void mod_AddMessage(GameHistoryChannel channel, [CanBeNull] UnityEngine.Object context, string message)
		{
			if (KingmakerPatchSettings.Game.DisableHistory)
			{
				return;
			}

			source_AddMessage(channel, context, message);
		}
	}
}

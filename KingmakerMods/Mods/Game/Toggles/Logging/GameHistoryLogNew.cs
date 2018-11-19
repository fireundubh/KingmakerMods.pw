using JetBrains.Annotations;
using Kingmaker;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Toggles.Logging
{
	[ModifiesType]
	public class GameHistoryLogNew : GameHistoryLog
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
				_useMod = UserConfig.Parser.GetValueAsBool("Game", "bDisableHistory");
			}

			return _useMod;
		}

		[NewMember]
		[DuplicatesBody("AddMessage")]
		private static void source_AddMessage(GameHistoryChannel channel, [CanBeNull] UnityEngine.Object context, string message)
		{
			throw new DeadEndException("source_AddMessage");
		}

		[ModifiesMember("AddMessage")]
		private static void mod_AddMessage(GameHistoryChannel channel, [CanBeNull] UnityEngine.Object context, string message)
		{
			_useMod = IsModReady();

			if (_useMod)
			{
				return;
			}

			source_AddMessage(channel, context, message);
		}
	}
}

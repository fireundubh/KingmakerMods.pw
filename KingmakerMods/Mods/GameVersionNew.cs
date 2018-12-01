using Kingmaker;
using KingmakerMods.Mods.Game.Configurables.Localization;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods
{
	[ModifiesType]
	public class GameVersionNew : GameVersion
	{
		#region ALIASES
		[ModifiesMember("Cached", ModificationScope.Nothing)]
		public static string alias_Cached
		{
			[ModifiesMember("get_Cached", ModificationScope.Nothing)]
			get;
			[ModifiesMember("set_Cached", ModificationScope.Nothing)]
			set;
		}
		#endregion

		[ModifiesMember("GetVersion")]
		public static string mod_GetVersion()
		{
			var gameVersion = Resources.Load<GameVersion>("Version");

			if (gameVersion == null)
			{
				return "Editor";
			}

			string version = gameVersion.Version;
			alias_Cached = version;

			string modVersionFormat = LocalizationManagerNew.LoadString("a0763979-fde3-44bb-a68f-3ee826d9c8fe");

			return string.Format(modVersionFormat, gameVersion.Version);
		}
	}
}

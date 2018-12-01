using System.Collections.Generic;
using Kingmaker.GameModes;
using Kingmaker.UI;
using KingmakerMods.Helpers;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.UI.Configurables.HighlightObjectsToggle
{
	[ModifiesType]
	public class KeyboardAccessNew : KeyboardAccess
	{
		[ModifiesMember("RegisterBinding", ModificationScope.Nothing)]
		public void mod_RegisterBinding(string name, KeyCode key, IEnumerable<GameModeType> gameModes)
		{
			throw new DeadEndException("mod_RegisterBinding");
		}

		[ModifiesMember("RegisterBinding")]
		public void mod_RegisterBinding(string name, KeyCode key, IEnumerable<GameModeType> gameModes, bool ctrl, bool alt, bool shift, bool release = false, ModificationSide side = ModificationSide.Any)
		{
			if (KingmakerPatchSettings.HighlightObjects.Enabled && name == "HighlightObjectsOff")
			{
				return;
			}

			foreach (GameModeType gameMode in gameModes)
			{
				this.mod_RegisterBinding(name, key, gameMode, ctrl, alt, shift, release, side);
			}
		}

		[ModifiesMember("RegisterBinding", ModificationScope.Nothing)]
		private void mod_RegisterBinding(string name, KeyCode key, GameModeType gameMode, bool ctrl, bool alt, bool shift, bool release, KeyboardAccess.ModificationSide side)
		{
			throw new DeadEndException("mod_RegisterBinding");
		}
	}
}

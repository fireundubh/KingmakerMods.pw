using System.Collections.Generic;
using System.Linq;
using Kingmaker.EntitySystem.Persistence;
using Kingmaker.UI.SaveLoadWindow;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Fixes
{
	/// <summary>
	/// Fixes an issue where duplicate companion portraits can appear in the portrait list
	/// </summary>
	[ModifiesType]
	public class SaveLoadPortraitsNew : SaveLoadPortraits
	{
		#region ALIASES
		[ModifiesMember("m_Portraits", ModificationScope.Nothing)]
		private List<SaveLoadPortait> alias_m_Portraits;
		#endregion

		[ModifiesMember("Set")]
		public void mod_Set(SaveInfo saveInfo)
		{
			this.Reset();

			if (saveInfo == null)
			{
				return;
			}

			var list = new List<Sprite>();

			if (saveInfo.PartyPortraits != null)
			{
				list.AddRange(saveInfo.PartyPortraits.Where(companions => companions != null).Distinct().Select(companions => companions.Data.SmallPortrait));
			}

			for (var i = 0; i < this.alias_m_Portraits.Count; i++)
			{
				this.alias_m_Portraits[i].Set(list.Count > i ? list[i] : null);
			}
		}
	}
}

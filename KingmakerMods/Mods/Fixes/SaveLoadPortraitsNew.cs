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
		[ModifiesMember("m_Portraits", ModificationScope.Nothing)]
		private List<SaveLoadPortait> mod_m_Portraits;

		[ModifiesMember("Set")]
		public void mod_Set(SaveInfo saveInfo)
		{
			this.Reset();

			if (saveInfo == null)
			{
				return;
			}

			List<Sprite> list = new List<Sprite>();

			if (saveInfo.PartyPortraits != null)
			{
				list.AddRange(saveInfo.PartyPortraits.Where(companions => companions != null).Distinct().Select(companions => companions.Data.SmallPortrait));
			}

			for (int i = 0; i < this.mod_m_Portraits.Count; i++)
			{
				this.mod_m_Portraits[i].Set(list.Count > i ? list[i] : null);
			}
		}
	}
}

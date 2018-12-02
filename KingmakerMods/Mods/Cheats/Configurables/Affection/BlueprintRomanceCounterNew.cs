using Kingmaker.Blueprints;
using KingmakerMods.Helpers;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Cheats.Configurables.Affection
{
	[ModifiesType]
	public class BlueprintRomanceCounterNew : BlueprintRomanceCounter
	{
		#region DUPLICATES

		[NewMember]
		[DuplicatesBody("Increase")]
		public void source_Increase(int v = 1)
		{
			throw new DeadEndException("source_Increase");
		}

		[NewMember]
		[DuplicatesBody("Decrease")]
		public void source_Decrease(int v = 1)
		{
			throw new DeadEndException("source_Decrease");
		}

		#endregion

		[ModifiesMember("Increase")]
		public void mod_Increase(int v = 1)
		{
			if (KingmakerPatchSettings.Affection.IncreaseMultEnabled)
			{
				v = Mathf.RoundToInt(v * KingmakerPatchSettings.Affection.IncreaseMultAmount);
				this.source_Increase(v);
				return;
			}

			if (KingmakerPatchSettings.Affection.FixedAffectionIncreaseEnabled)
			{
				v = KingmakerPatchSettings.Affection.FixedAffectionIncreaseAmount;
				this.source_Increase(v);
				return;
			}

			this.source_Increase(v);
		}

		[ModifiesMember("Decrease")]
		public void mod_Decrease(int v = 1)
		{
			if (KingmakerPatchSettings.Affection.DecreaseMultEnabled)
			{
				v = Mathf.RoundToInt(v * KingmakerPatchSettings.Affection.DecreaseMultAmount);
				this.source_Decrease(v);
				return;
			}

			if (KingmakerPatchSettings.Affection.FixedAffectionDecreaseEnabled)
			{
				v = KingmakerPatchSettings.Affection.FixedAffectionDecreaseAmount;
				this.source_Decrease(v);
				return;
			}

			this.source_Decrease(v);
		}
	}
}

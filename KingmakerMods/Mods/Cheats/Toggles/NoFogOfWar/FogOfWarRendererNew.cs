using System;
using Kingmaker.Visual;
using Kingmaker.Visual.FogOfWar;
using KingmakerMods.Helpers;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Cheats.Toggles.NoFogOfWar
{
	[ModifiesType]
	public class FogOfWarRendererNew : FogOfWarRenderer
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("Update")]
		public void source_Update()
		{
			throw new DeadEndException("source_Update");
		}
		#endregion

		[ModifiesMember("Update")]
		public void mod_Update()
		{
			if (KingmakerPatchSettings.Cheats.DisableFogOfWar)
			{
				Shader.SetGlobalFloat("_FogOfWarGlobalFlag", 0);
				return;
			}

			this.source_Update();
		}

		[Obsolete]
		public FogOfWarRendererNew(LocationMaskRenderer context) : base(context)
		{
		}
	}
}

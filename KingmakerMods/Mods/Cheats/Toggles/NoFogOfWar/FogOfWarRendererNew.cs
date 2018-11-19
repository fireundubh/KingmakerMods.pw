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
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("Update")]
		public void source_Update()
		{
			throw new DeadEndException("source_Update");
		}

		[ModifiesMember("Update")]
		public void mod_Update()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bDisableFogOfWar");
			}

			if (_useMod)
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

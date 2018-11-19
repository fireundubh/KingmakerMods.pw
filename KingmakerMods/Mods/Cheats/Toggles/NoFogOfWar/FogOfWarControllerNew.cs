using Kingmaker.Controllers;
using Kingmaker.EntitySystem;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.NoFogOfWar
{
	[ModifiesType]
	public class FogOfWarControllerNew : FogOfWarController
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("Update")]
		private void source_Update(EntityDataBase entity)
		{
			throw new DeadEndException("source_Update");
		}

		[ModifiesMember("Update")]
		private void Update(EntityDataBase entity)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bDisableFogOfWar");
			}

			if (_useMod)
			{
				return;
			}

			this.source_Update(entity);
		}
	}
}

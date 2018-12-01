using Kingmaker.Controllers;
using Kingmaker.EntitySystem;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.NoFogOfWar
{
	[ModifiesType]
	public class FogOfWarControllerNew : FogOfWarController
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("Update")]
		private void source_Update(EntityDataBase entity)
		{
			throw new DeadEndException("source_Update");
		}
		#endregion

		[ModifiesMember("Update")]
		private void Update(EntityDataBase entity)
		{
			if (KingmakerPatchSettings.Cheats.DisableFogOfWar)
			{
				return;
			}

			this.source_Update(entity);
		}
	}
}

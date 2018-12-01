using Kingmaker.Visual.FogOfWar;
using Patchwork;
using KingmakerMods.Helpers;

namespace KingmakerMods.Mods.Cheats.Toggles.NoFogOfWar
{
	[ModifiesType]
	public class FogOfWarSettingsNew : FogOfWarSettings
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("get_Radius")]
		public float source_get_Radius()
		{
			throw new DeadEndException("source_get_Radius");
		}
		#endregion

		[ModifiesMember("Radius")]
		public float mod_Radius
		{
			[ModifiesMember("get_Radius")]
			get
			{
				return KingmakerPatchSettings.Cheats.DisableFogOfWar ? float.MaxValue : this.source_get_Radius();
			}
		}
	}
}

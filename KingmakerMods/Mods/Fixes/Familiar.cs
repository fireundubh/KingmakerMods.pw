using KingmakerMods.Helpers;
using Patchwork;
#pragma warning disable 1570

namespace KingmakerMods.Mods.Fixes
{
	/// <summary>
	/// Occurs during area transitions; log noise; no gameplay impact
	///
	/// NullReferenceException: Object reference not set to an instance of an object
	///		Kingmaker.Visual.Critters.Familiar.Update () (at <ce7a2e7c39614ae6a8d3ccb1a6c99337>:0)
	/// </summary>

	[ModifiesType]
	public class Familiar : Kingmaker.Visual.Critters.Familiar
	{
		[NewMember]
		[DuplicatesBody("Update")]
		private void source_Update()
		{
			throw new DeadEndException("source_Update");
		}

		[ModifiesMember("Update")]
		private void mod_Update()
		{
			try
			{
				this.source_Update();
			}
			catch
			{
				// ignored
			}
		}
	}
}

using Kingmaker.EntitySystem.Persistence;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Toggles.SteamCloud
{
	[ModifiesType]
	public class SteamSavesReplicatorNew : SteamSavesReplicator
	{
		// Initialize cannot be patched because...
		// [FTL] Encountered a feature that isn't supported. Details: MetadataType not supported: RequiredModifier

		#region DUPLICATES
		//		[NewMember]
		//		[DuplicatesBody("Initialize")]
		//		public void source_Initialize()
		//		{
		//			throw new DeadEndException("source_Initialize");
		//		}

		[NewMember]
		[DuplicatesBody("PullUpdates")]
		public void source_PullUpdates()
		{
			throw new DeadEndException("source_PullUpdates");
		}

		[NewMember]
		[DuplicatesBody("DeleteSave")]
		public void source_DeleteSave(SaveInfo saveInfo)
		{
			throw new DeadEndException("source_DeleteSave");
		}

		[NewMember]
		[DuplicatesBody("RegisterSave")]
		public void source_RegisterSave(SaveInfo saveInfo)
		{
			throw new DeadEndException("source_RegisterSave");
		}
		#endregion

		//		[ModifiesMember("Initialize")]
		//		public void mod_Initialize()
		//		{
		//			_useMod = IsModReady();
		//
		//			if (!_useMod)
		//			{
		//				this.source_Initialize();
		//			}
		//		}

		[ModifiesMember("PullUpdates")]
		public void mod_PullUpdates()
		{
			if (!KingmakerPatchSettings.Game.DisableSteamCloud)
			{
				this.source_PullUpdates();
			}
		}

		[ModifiesMember("DeleteSave")]
		public void mod_DeleteSave(SaveInfo saveInfo)
		{
			if (!KingmakerPatchSettings.Game.DisableSteamCloud)
			{
				this.source_DeleteSave(saveInfo);
			}
		}

		[ModifiesMember("RegisterSave")]
		public void mod_RegisterSave(SaveInfo saveInfo)
		{
			if (!KingmakerPatchSettings.Game.DisableSteamCloud)
			{
				this.source_RegisterSave(saveInfo);
			}
		}
	}
}

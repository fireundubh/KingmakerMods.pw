using Kingmaker.EntitySystem.Persistence;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Toggles.SteamCloud
{
	[ModifiesType]
	public class SteamSavesReplicatorNew : SteamSavesReplicator
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		private static bool IsModReady()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game", "bDisableSteamCloud");
			}

			return _useMod;
		}

		// Initialize cannot be patched because...
		// [FTL] Encountered a feature that isn't supported. Details: MetadataType not supported: RequiredModifier

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
			_useMod = IsModReady();

			if (!_useMod)
			{
				this.source_PullUpdates();
			}
		}

		[ModifiesMember("DeleteSave")]
		public void mod_DeleteSave(SaveInfo saveInfo)
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				this.source_DeleteSave(saveInfo);
			}
		}

		[ModifiesMember("RegisterSave")]
		public void mod_RegisterSave(SaveInfo saveInfo)
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				this.source_RegisterSave(saveInfo);
			}
		}
	}
}

using Kingmaker.UI;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.UI.Configurables.HighlightObjectsToggle
{
	[ModifiesType]
	public class KeyboardAccessNew : KeyboardAccess
	{
		#region CONFIGURATION
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
				_useMod = UserConfig.Parser.GetValueAsBool("UI.HighlightObjectsToggle", "bEnabled");
			}

			return _useMod;
		}
		#endregion

		#region DUPLICATED METHODS
		[NewMember]
		[DuplicatesBody("RegisterBuiltinBindings")]
		public void source_RegisterBuiltinBindings()
		{
			throw new DeadEndException("source_RegisterBuiltinBindings");
		}
		#endregion

		[ModifiesMember("RegisterBuiltinBindings")]
		public void mod_RegisterBuiltinBindings()
		{
			this.source_RegisterBuiltinBindings();

			_useMod = IsModReady();

			if (_useMod)
			{
				this.UnregisterBinding("HighlightObjectsOff");
			}
		}
	}
}

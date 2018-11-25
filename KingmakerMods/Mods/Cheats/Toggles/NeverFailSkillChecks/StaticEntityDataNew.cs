using System;
using Kingmaker.EntitySystem;
using Kingmaker.View;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.NeverFailSkillChecks
{
	[ModifiesType]
	public class StaticEntityDataNew : StaticEntityData
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
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bNeverFailSkillChecks");
			}

			return _useMod;
		}
		#endregion

		#region DUPLICATED METHODS
		[NewMember]
		[DuplicatesBody("get_IsPerceptionCheckPassed")]
		public bool source_get_IsPerceptionCheckPassed()
		{
			throw new DeadEndException("source_get_IsPerceptionCheckPassed");
		}
		#endregion

		[ModifiesMember("IsPerceptionCheckPassed")]
		public bool mod_IsPerceptionCheckPassed
		{
			[ModifiesMember("get_IsPerceptionCheckPassed")]
			get
			{
				_useMod = IsModReady();

				if (_useMod)
				{
					return true;
				}

				return this.source_get_IsPerceptionCheckPassed();
			}
			[ModifiesMember("set_IsPerceptionCheckPassed", ModificationScope.Nothing)]
			set
			{
				throw new DeadEndException("source_set_IsPerceptionCheckPassed");
			}
		}

		[Obsolete]
		public StaticEntityDataNew(EntityViewBase view) : base(view)
		{
		}
	}
}

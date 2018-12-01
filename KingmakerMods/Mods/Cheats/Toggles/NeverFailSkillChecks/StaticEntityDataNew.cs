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
		#region DUPLICATES
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
				// ReSharper disable once ConvertIfStatementToReturnStatement
				if (KingmakerPatchSettings.Cheats.NeverFailSkillChecks)
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

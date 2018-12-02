using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.Undetectable
{
	[ModifiesType]
	public class UnitStealthNew : UnitStealth
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("AddSpottedBy")]
		public bool source_AddSpottedBy(UnitEntityData unit)
		{
			throw new DeadEndException("source_AddSpottedBy");
		}
		#endregion

		[ModifiesMember("AddSpottedBy")]
		public bool mod_AddSpottedBy(UnitEntityData unit)
		{
			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (KingmakerPatchSettings.Cheats.Undetectable)
			{
				return false;
			}

			return this.source_AddSpottedBy(unit);
		}
	}
}

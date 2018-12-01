using Kingmaker.Controllers;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Parts;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.AlwaysUnencumbered
{
	[ModifiesType]
	public class PartyEncumbranceControllerNew : PartyEncumbranceController
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("UpdatePartyEncumbrance")]
		public static void source_UpdatePartyEncumbrance()
		{
			throw new DeadEndException("source_UpdatePartyEncumbrance");
		}

		[NewMember]
		[DuplicatesBody("UpdateUnitEncumbrance")]
		public static void source_UpdateUnitEncumbrance(UnitDescriptor unit)
		{
			throw new DeadEndException("source_UpdateUnitEncumbrance");
		}
		#endregion

		[ModifiesMember("UpdatePartyEncumbrance")]
		public static void mod_UpdatePartyEncumbrance()
		{
			if (!KingmakerPatchSettings.Cheats.AlwaysUnencumbered)
			{
				source_UpdatePartyEncumbrance();
				return;
			}

			if (Kingmaker.Game.Instance.Player.Encumbrance == Encumbrance.Light)
			{
				return;
			}

			Kingmaker.Game.Instance.Player.Encumbrance = Encumbrance.Light;

			EventBus.RaiseEvent<IPartyEncumbranceHandler>(x => x.ChangePartyEncumbrance());
		}

		[ModifiesMember("UpdateUnitEncumbrance")]
		public static void mod_UpdateUnitEncumbrance(UnitDescriptor unit)
		{
			if (!KingmakerPatchSettings.Cheats.AlwaysUnencumbered)
			{
				source_UpdateUnitEncumbrance(unit);
				return;
			}

			if (unit.Encumbrance == Encumbrance.Light)
			{
				return;
			}

			unit.Encumbrance = Encumbrance.Light;
			unit.Remove<UnitPartEncumbrance>();

			EventBus.RaiseEvent<IUnitEncumbranceHandler>(x => x.ChangeUnitEncumbrance(unit));
			EventBus.RaiseEvent<IUIUnitStatsRefresh>(h => h.Refresh());
		}
	}
}

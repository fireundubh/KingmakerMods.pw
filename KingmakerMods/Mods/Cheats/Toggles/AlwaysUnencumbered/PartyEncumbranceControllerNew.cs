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
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

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

		[ModifiesMember("UpdatePartyEncumbrance")]
		public static void mod_UpdatePartyEncumbrance()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bAlwaysUnencumbered");
			}

			if (!_useMod)
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
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bAlwaysUnencumbered");
			}

			if (!_useMod)
			{
				source_UpdateUnitEncumbrance(unit);
				return;
			}

			const Encumbrance encumbrance = Encumbrance.Light;

			if (unit.Encumbrance == encumbrance)
			{
				return;
			}

			unit.Encumbrance = encumbrance;

			if (unit.Encumbrance == Encumbrance.Light)
			{
				unit.Remove<UnitPartEncumbrance>();
			}
			else
			{
				unit.Ensure<UnitPartEncumbrance>().Init(unit.Encumbrance);
			}

			EventBus.RaiseEvent<IUnitEncumbranceHandler>(x => x.ChangeUnitEncumbrance(unit));

			EventBus.RaiseEvent<IUIUnitStatsRefresh>(h => h.Refresh());
		}
	}
}

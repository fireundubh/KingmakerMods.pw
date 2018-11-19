using System;
using Kingmaker.Blueprints.Area;
using Kingmaker.Controllers.Rest;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Toggles.RestAnywhere
{
	[ModifiesType]
	public class RestControllerNew : RestController
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("CanRest")]
		public static bool source_CanRest(TimeSpan restTime)
		{
			throw new DeadEndException("source_CanRest");
		}

		[ModifiesMember("CanRest")]
		public static bool mod_CanRest(TimeSpan restTime)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game", "bRestAnywhere");
			}

			if (!_useMod)
			{
				return source_CanRest(restTime);
			}

			if (Kingmaker.Game.Instance.Player.IsInCombat)
			{
				return false;
			}

			CampingSettings campingSettings = Kingmaker.Game.Instance.CurrentlyLoadedArea.CampingSettings;
			campingSettings.CampingAllowed = true;

//			if (!campingSettings.CampingAllowed)
//			{
//				return false;
//			}

			int rationsCount = Kingmaker.Game.Instance.Player.Inventory.Count(Kingmaker.Game.Instance.BlueprintRoot.RestItem);
			int neededRations = CalculateNeededRations(restTime);
			bool huntersAssigned = Kingmaker.Game.Instance.Player.Camping.Hunters.Count > 0;

			return huntersAssigned || rationsCount >= neededRations;
		}
	}
}

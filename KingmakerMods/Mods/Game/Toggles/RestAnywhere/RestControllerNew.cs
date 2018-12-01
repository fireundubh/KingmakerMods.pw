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
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("CanRest")]
		public static bool source_CanRest(TimeSpan restTime)
		{
			throw new DeadEndException("source_CanRest");
		}
		#endregion

		[ModifiesMember("CanRest")]
		public static bool mod_CanRest(TimeSpan restTime)
		{
			if (!KingmakerPatchSettings.Game.RestAnywhere)
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

			bool huntersAssigned = Kingmaker.Game.Instance.Player.Camping.Hunters.Count > 0;

			int rationsCount = Kingmaker.Game.Instance.Player.Inventory.Count(Kingmaker.Game.Instance.BlueprintRoot.RestItem);
			int neededRations = CalculateNeededRations(restTime);

			return huntersAssigned || rationsCount >= neededRations;
		}
	}
}

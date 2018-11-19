using System.Linq;
using Kingmaker.Achievements;
using Kingmaker.Globalmap;
using Kingmaker.Globalmap.State;
using Kingmaker.Kingdom;
using Kingmaker.Kingdom.Blueprints;
using KingmakerMods.Helpers;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Game.Configurables.CurrencyFallback
{
	[ModifiesType]
	public class LocationDataNew : LocationData
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("ClaimResource")]
		public void source_ClaimResource()
		{
			throw new DeadEndException("ClaimResource");
		}

		[ModifiesMember("ClaimResource")]
		public void mod_ClaimResource()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game.KingdomEvents", "bCurrencyFallback");
			}

			if (!_useMod)
			{
				this.source_ClaimResource();
				return;
			}

			if (this.Resource != ResourceStateType.CanClaim)
			{
				return;
			}

			KingdomCurrencyFallback.SpendPoints(KingdomRoot.Instance.DefaultMapResourceCost);

			KingdomState.Instance.Resources.Add(this.Blueprint);

			this.Blueprint.ResourceStats.Apply();

			if (GlobalMapRules.Instance && GlobalMapRules.Instance.ClaimedResourceVisual)
			{
				GlobalMapLocation locationObject = GlobalMapRules.Instance.GetLocationObject(this.Blueprint);

				if (locationObject)
				{
					Object.Instantiate(GlobalMapRules.Instance.ClaimedResourceVisual, locationObject.transform, false);
				}
			}

			if (KingdomRoot.Instance.Locations.Count(l => l.HasKingdomResource) == KingdomState.Instance.Resources.Count)
			{
				Kingmaker.Game.Instance.Player.Achievements.Unlock(AchievementType.IntensiveDevelopment);
			}
		}
	}
}

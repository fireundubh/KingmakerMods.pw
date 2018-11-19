using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Controllers.GlobalMap;
using Kingmaker.ElementsSystem;
using Kingmaker.Globalmap;
using Kingmaker.Globalmap.Blueprints;
using Kingmaker.Globalmap.State;
using Kingmaker.Utility;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.Abracadabra
{
	[ModifiesType]
	public class LocationRevealControllerNew : LocationRevealController
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		[DuplicatesBody("Tick")]
		public void source_Tick()
		{
			throw new DeadEndException("source_Tick");
		}

		[NewMember]
		private bool IsHiddenOrWaypoint(LocationData location)
		{
			switch (location.Blueprint.Type)
			{
				case LocationType.HiddenLocation:
				case LocationType.Waypoint:
				case LocationType.SystemWaypoint:
					return true;
				default:
					return false;
			}
		}

		[ModifiesMember("Tick")]
		public void mod_Tick()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bAbracadabra");
			}

			if (!_useMod)
			{
				this.source_Tick();
				return;
			}

			GlobalMapState globalMap = Kingmaker.Game.Instance.Player.GlobalMap;

			Dictionary<BlueprintLocation, LocationData>.ValueCollection locations = globalMap.Locations.Values;

			// don't run foreach on tick if we don't have to
			// this will return true until all locations, except hidden locations and waypoints, are revealed
			bool canRevealLocations = locations.Any(l => !l.IsRevealed && !this.IsHiddenOrWaypoint(l));

			if (!canRevealLocations)
			{
				return;
			}

			// removed distance checks
			foreach (LocationData locationData in locations)
			{
				if (locationData.IsRevealed)
				{
					continue;
				}

				GlobalMapLocation locationObject = GlobalMapRules.Instance.GetLocationObject(locationData.Blueprint);

				if (!locationObject)
				{
					continue;
				}

				ConditionsChecker possibleToRevealCondition = locationData.Blueprint.PossibleToRevealCondition;

				LocationType type = locationData.Blueprint.Type;

				switch (type)
				{
					case LocationType.Location:
						if (!possibleToRevealCondition.HasConditions || possibleToRevealCondition.Check())
						{
							GlobalMapRules.Instance.RevealLocation(locationObject);
						}

						break;
					case LocationType.Landmark:
						if (!possibleToRevealCondition.HasConditions || possibleToRevealCondition.Check())
						{
							GlobalMapRules.Instance.RevealLocation(locationObject);
						}

						break;
					default:
						if (type != LocationType.HiddenLocation)
						{
							continue;
						}

						if (possibleToRevealCondition.HasConditions && !possibleToRevealCondition.Check())
						{
							continue;
						}

						GlobalMapRules.Instance.RevealLocation(locationObject);
						break;
				}
			}
		}
	}
}

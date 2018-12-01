using Kingmaker.Globalmap.Blueprints;
using Kingmaker.Globalmap.State;
using Patchwork;

// [IL]: Error: [Assembly-CSharp : Kingmaker.Globalmap.State.LocationData::GetInfo][offset 0x00000052][found ref 'System.Object'][expected ref 'Kingmaker.Globalmap.Blueprints.ILocationInfo'] Unexpected type on the stack.(Error: 0x80131854)

namespace KingmakerMods.Mods.ILFixes
{
	[ModifiesType]
	public class LocationDataNew : LocationData
	{
		[ModifiesMember("GetInfo")]
		public ILocationInfo mod_GetInfo()
		{
			// ReSharper disable once LoopCanBePartlyConvertedToQuery
			foreach (LocationVariation variation in this.Blueprint.Variations)
			{
				if (variation.Conditions.Check())
				{
					return variation;
				}
			}

			return (ILocationInfo) this.KingdomSettlement ?? this.Blueprint;
		}
	}
}

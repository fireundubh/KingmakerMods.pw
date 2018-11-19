using Kingmaker.Globalmap.Blueprints;
using Kingmaker.Globalmap.State;
using Patchwork;

namespace KingmakerMods.Mods.ILFixes
{
	[ModifiesType]
	public class LocationDataNew : LocationData
	{
		// [IL]: Error: [Assembly-CSharp : Kingmaker.Globalmap.State.LocationData::GetInfo][offset 0x00000052][found ref 'System.Object'][expected ref 'Kingmaker.Globalmap.Blueprints.ILocationInfo'] Unexpected type on the stack.(Error: 0x80131854)

		[ModifiesMember("GetInfo")]
		public ILocationInfo mod_GetInfo()
		{
			for (int i = 0; i < this.Blueprint.Variations.Length; ++i)
			{
				LocationVariation variation = this.Blueprint.Variations[i];

				if (variation.Conditions.Check())
				{
					return variation;
				}
			}

			return (ILocationInfo) this.KingdomSettlement ?? this.Blueprint;
		}
	}
}

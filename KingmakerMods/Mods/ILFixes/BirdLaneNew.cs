using Kingmaker.Visual.Critters;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.ILFixes
{
	[ModifiesType]
	public class BirdLaneNew : BirdLane
	{
		// [IL]: Error: [Assembly-CSharp : Kingmaker.Visual.Critters.Birds::OnDrawGizmos][offset 0x00000040][found ref 'System.Object'][expected ref 'System.Collections.Generic.IEnumerable`1[Kingmaker.Visual.Critters.BirdLane]'] Unexpected type on the stack.(Error: 0x80131854)

		[ModifiesMember("Points")]
		public Transform[] mod_Points;
	}
}

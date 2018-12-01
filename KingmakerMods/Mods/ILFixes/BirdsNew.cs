using System.Collections.Generic;
using System.Linq;
using Kingmaker.Visual.Critters;
using Patchwork;
using UnityEngine;

// [IL]: Error: [Assembly-CSharp : Kingmaker.Visual.Critters.Birds::OnDrawGizmos][offset 0x00000040][found ref 'System.Object'][expected ref 'System.Collections.Generic.IEnumerable`1[Kingmaker.Visual.Critters.BirdLane]'] Unexpected type on the stack.(Error: 0x80131854)

namespace KingmakerMods.Mods.ILFixes
{
	[ModifiesType]
	public class BirdsNew : Birds
	{
		[ModifiesAccessibility("m_Lanes")]
		private List<BirdLane> mod_m_Lanes = new List<BirdLane>();

		[ModifiesMember("OnDrawGizmos")]
		public void mod_OnDrawGizmos()
		{
			if (!this.DrawLines)
			{
				return;
			}

			Gizmos.color = new Color(0.62f, 0.81f, 0.73f);

			IEnumerable<BirdLane> birdLanes = !Application.isPlaying ? (IEnumerable<BirdLane>) (object) this.GetComponentsInChildren<BirdLane>() : (IEnumerable<BirdLane>) this.mod_m_Lanes;

			foreach (BirdLane birdLane in birdLanes.Where(birdLane => birdLane.Points != null && birdLane.Points.Length > 1))
			{
				for (var i = 0; i < birdLane.Points.Length - 1; ++i)
				{
					Gizmos.DrawLine(birdLane.Points[i].position, birdLane.Points[i + 1].position);
				}
			}
		}
	}
}

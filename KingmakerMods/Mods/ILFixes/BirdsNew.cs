using System.Collections.Generic;
using Kingmaker.Visual.Critters;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.ILFixes
{
	[ModifiesType]
	public class BirdsNew : Birds
	{
		// [IL]: Error: [Assembly-CSharp : Kingmaker.Visual.Critters.Birds::OnDrawGizmos][offset 0x00000040][found ref 'System.Object'][expected ref 'System.Collections.Generic.IEnumerable`1[Kingmaker.Visual.Critters.BirdLane]'] Unexpected type on the stack.(Error: 0x80131854)

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

			foreach (BirdLane birdLane in !Application.isPlaying ? (IEnumerable<BirdLane>) (object) this.GetComponentsInChildren<BirdLane>() : (IEnumerable<BirdLane>) this.mod_m_Lanes)
			{
				if (birdLane.Points == null || birdLane.Points.Length <= 1)
				{
					continue;
				}

				for (int index = 0; index < birdLane.Points.Length - 1; ++index)
				{
					Gizmos.DrawLine(birdLane.Points[index].position, birdLane.Points[index + 1].position);
				}
			}
		}
	}
}

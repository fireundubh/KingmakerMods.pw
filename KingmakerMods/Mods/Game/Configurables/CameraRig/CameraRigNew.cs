using System.Collections.Generic;
using Kingmaker.Visual;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Game.Configurables.CameraRig
{
	[ModifiesType]
	public class CameraRigNew : Kingmaker.View.CameraRig
	{
		#region ALIASES
		[ModifiesMember("m_ScrollRubberBand", ModificationScope.Nothing)]
		private float alias_m_ScrollRubberBand;

		[ModifiesMember("m_ScrollRubberBandCamp", ModificationScope.Nothing)]
		private float alias_m_ScrollRubberBandCamp;

		[ModifiesMember("m_ScrollScreenThreshold", ModificationScope.Nothing)]
		private float alias_m_ScrollScreenThreshold;

		[ModifiesMember("m_ScrollSpeed", ModificationScope.Nothing)]
		private float alias_m_ScrollSpeed;

		[ModifiesMember("m_DragScrollSpeed", ModificationScope.Nothing)]
		private float alias_m_DragScrollSpeed;

		[ModifiesMember("m_ShakeFxList", ModificationScope.Nothing)]
		private List<CameraShakeFx> alias_m_ShakeFxList;
		#endregion

		[MemberAlias(".ctor", typeof(MonoBehaviour))]
		private void monobehavior_ctor()
		{
		}

		[ModifiesMember(".ctor")]
		public void CtorNew()
		{
			this.monobehavior_ctor();

			this.alias_m_DragScrollSpeed = 1f;
			this.alias_m_ScrollRubberBand = 4f;
			this.alias_m_ScrollRubberBandCamp = 3f;
			this.alias_m_ScrollScreenThreshold = 4f;
			this.alias_m_ScrollSpeed = 25f;
			this.alias_m_ShakeFxList = new List<CameraShakeFx>();

			if (!KingmakerPatchSettings.CameraRig.Enabled)
			{
				return;
			}

			this.alias_m_DragScrollSpeed = KingmakerPatchSettings.CameraRig.DragScrollSpeed;
			this.alias_m_ScrollRubberBand = KingmakerPatchSettings.CameraRig.ScrollRubberBand;
			this.alias_m_ScrollRubberBandCamp = KingmakerPatchSettings.CameraRig.ScrollRubberBandCamp;
			this.alias_m_ScrollScreenThreshold = KingmakerPatchSettings.CameraRig.ScrollScreenThreshold;
			this.alias_m_ScrollSpeed = KingmakerPatchSettings.CameraRig.ScrollSpeed;
		}
	}
}

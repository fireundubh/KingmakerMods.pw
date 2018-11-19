using System.Collections.Generic;
using Kingmaker.Visual;
using Kingmaker.Visual.FogOfWar;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Game.Configurables.CameraRig
{
	[ModifiesType]
	public class CameraRigNew : Kingmaker.View.CameraRig
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[ModifiesMember("m_ScrollRubberBand", ModificationScope.Nothing)]
		private float mod_m_ScrollRubberBand;

		[ModifiesMember("m_ScrollRubberBandCamp", ModificationScope.Nothing)]
		private float mod_m_ScrollRubberBandCamp;

		[ModifiesMember("m_ScrollScreenThreshold", ModificationScope.Nothing)]
		private float mod_m_ScrollScreenThreshold;

		[ModifiesMember("m_ScrollSpeed", ModificationScope.Nothing)]
		private float mod_m_ScrollSpeed;

		[ModifiesMember("m_DragScrollSpeed", ModificationScope.Nothing)]
		private float mod_m_DragScrollSpeed;

		[ModifiesMember("m_ShakeFxList", ModificationScope.Nothing)]
		private List<CameraShakeFx> mod_m_ShakeFxList;

		[MemberAlias(".ctor", typeof(MonoBehaviour))]
		private void monobehavior_ctor()
		{
		}

		[ModifiesMember(".ctor")]
		public void CtorNew()
		{
			this.monobehavior_ctor();

			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game.CameraRig", "bEnabled");
			}

			this.mod_m_ScrollRubberBand = 4f;
			this.mod_m_ScrollRubberBandCamp = 3f;
			this.mod_m_ScrollScreenThreshold = 4f;
			this.mod_m_ScrollSpeed = 25f;
			this.mod_m_DragScrollSpeed = 1f;
			this.mod_m_ShakeFxList = new List<CameraShakeFx>();

			if (_useMod)
			{
				this.mod_m_ScrollRubberBand = UserConfig.Parser.GetValueAsFloat("Game.CameraRig", "fScrollRubberBand");
				this.mod_m_ScrollRubberBandCamp = UserConfig.Parser.GetValueAsFloat("Game.CameraRig", "fScrollRubberBandCamp");
				this.mod_m_ScrollScreenThreshold = UserConfig.Parser.GetValueAsFloat("Game.CameraRig", "fScrollScreenThreshold");
				this.mod_m_ScrollSpeed = UserConfig.Parser.GetValueAsFloat("Game.CameraRig", "fScrollSpeed");
				this.mod_m_DragScrollSpeed = UserConfig.Parser.GetValueAsFloat("Game.CameraRig", "fDragScrollSpeed");
			}
		}
	}
}

using System;
using Kingmaker.GameModes;
using KingmakerMods.Helpers;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Game.Configurables.CameraZoom
{
	[ModifiesType]
	public class CameraZoomNew : Kingmaker.View.CameraZoom
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		private static float _defaultFovMax;

		[NewMember]
		private static float _cutsceneFovMax;

		[NewMember]
		private static float _dialogFovMax;

		[NewMember]
		private static float _globalMapFovMax;

		[NewMember]
		private static float _settlementFovMax;

		[ModifiesMember("FovMax")]
		public float mod_FovMax;

		[NewMember]
		[DuplicatesBody("TickZoom")]
		public void source_TickZoom()
		{
			throw new DeadEndException("source_TickZoom");
		}

		[ModifiesMember("TickZoom")]
		public void mod_TickZoom()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game.CameraZoom", "bEnabled");
				_defaultFovMax = UserConfig.Parser.GetValueAsFloat("Game.CameraZoom", "fCameraZoomMax");
				_cutsceneFovMax = UserConfig.Parser.GetValueAsFloat("Game.CameraZoom", "fCameraZoomMaxCutscene");
				_dialogFovMax = UserConfig.Parser.GetValueAsFloat("Game.CameraZoom", "fCameraZoomMaxDialog");
				_globalMapFovMax = UserConfig.Parser.GetValueAsFloat("Game.CameraZoom", "fCameraZoomMaxGlobalMap");
				_settlementFovMax = UserConfig.Parser.GetValueAsFloat("Game.CameraZoom", "fCameraZoomMaxSettlement");
			}

			if (!_useMod)
			{
				this.source_TickZoom();
				return;
			}

			if (!this.IsScrollBusy)
			{
				this.m_PlayerScrollPosition += this.IsOutOfScreen ? 0f : Input.GetAxis("Mouse ScrollWheel");
			}

			switch (Kingmaker.Game.Instance.CurrentMode)
			{
				case GameModeType.Cutscene:
					this.mod_FovMax = _cutsceneFovMax;
					break;
				case GameModeType.Dialog:
					this.mod_FovMax = _dialogFovMax;
					break;
				case GameModeType.GlobalMap:
					this.mod_FovMax = _globalMapFovMax;
					break;
				case GameModeType.KingdomSettlement:
					this.mod_FovMax = _settlementFovMax;
					break;
				default:
					this.mod_FovMax = _defaultFovMax;
					break;
			}

			this.m_ScrollPosition = this.m_PlayerScrollPosition;
			this.m_ScrollPosition = Mathf.Clamp(this.m_ScrollPosition, 0f, this.m_ZoomLenght);
			this.m_SmoothScrollPosition = Mathf.Lerp(this.m_SmoothScrollPosition, this.m_ScrollPosition, Time.unscaledDeltaTime * this.m_Smooth);
			this.m_Camera.fieldOfView = Mathf.Lerp(this.mod_FovMax, this.FovMin, this.CurrentNormalizePosition);
			this.m_PlayerScrollPosition = this.m_ScrollPosition;
		}
	}
}

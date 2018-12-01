using Kingmaker.GameModes;
using KingmakerMods.Helpers;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Game.Configurables.CameraZoom
{
	[ModifiesType]
	public class CameraZoomNew : Kingmaker.View.CameraZoom
	{
		#region ALIASES
		[ModifiesMember("FovMax", ModificationScope.Nothing)]
		public float alias_FovMax;
		#endregion

		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("TickZoom")]
		public void source_TickZoom()
		{
			throw new DeadEndException("source_TickZoom");
		}
		#endregion

		[ModifiesMember("TickZoom")]
		public void mod_TickZoom()
		{
			if (!KingmakerPatchSettings.CameraZoom.Enabled)
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
					this.alias_FovMax = KingmakerPatchSettings.CameraZoom.CameraZoomMaxCutscene;
					break;
				case GameModeType.Dialog:
					this.alias_FovMax = KingmakerPatchSettings.CameraZoom.CameraZoomMaxDialog;
					break;
				case GameModeType.GlobalMap:
					this.alias_FovMax = KingmakerPatchSettings.CameraZoom.CameraZoomMaxGlobalMap;
					break;
				case GameModeType.KingdomSettlement:
					this.alias_FovMax = KingmakerPatchSettings.CameraZoom.CameraZoomMaxSettlement;
					break;
				default:
					this.alias_FovMax = KingmakerPatchSettings.CameraZoom.CameraZoomMax;
					break;
			}

			this.m_ScrollPosition = this.m_PlayerScrollPosition;
			this.m_ScrollPosition = Mathf.Clamp(this.m_ScrollPosition, 0f, this.m_ZoomLenght);
			this.m_SmoothScrollPosition = Mathf.Lerp(this.m_SmoothScrollPosition, this.m_ScrollPosition, Time.unscaledDeltaTime * this.m_Smooth);
			this.m_Camera.fieldOfView = Mathf.Lerp(this.alias_FovMax, this.FovMin, this.CurrentNormalizePosition);
			this.m_PlayerScrollPosition = this.m_ScrollPosition;
		}
	}
}

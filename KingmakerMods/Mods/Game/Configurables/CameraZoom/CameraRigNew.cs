using Kingmaker.GameModes;
using Kingmaker.Visual.FogOfWar;
using KingmakerMods.Helpers;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Game.Configurables.CameraZoom
{
	[ModifiesType]
	public class CameraRigNew : Kingmaker.View.CameraRig
	{
		#region ALIASES
		[ModifiesMember("m_CameraAttachPoint", ModificationScope.Nothing)]
		private Transform alias_m_CameraAttachPoint;

		[ModifiesMember("m_CameraMapAttachPoint", ModificationScope.Nothing)]
		private Transform alias_m_CameraMapAttachPoint;
		#endregion

		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("SetMapMode")]
		public void source_SetMapMode(bool isGlobalMap)
		{
			throw new DeadEndException("source_SetMapMode");
		}
		#endregion

		[ModifiesMember("SetMapMode")]
		public void mod_SetMapMode(bool isGlobalMap)
		{
			if (!KingmakerPatchSettings.CameraZoom.Enabled)
			{
				this.source_SetMapMode(isGlobalMap);
				return;
			}

			if (this.Camera)
			{
				this.Camera.transform.SetParent(isGlobalMap ? this.alias_m_CameraMapAttachPoint : this.alias_m_CameraAttachPoint);

				this.SetCameraFieldOfView(isGlobalMap);

				if (!isGlobalMap)
				{
					this.Camera.transform.localPosition = Vector3.zero;
					this.Camera.transform.localRotation = Quaternion.identity;
				}
			}

			if (this.MapLight)
			{
				this.MapLight.SetActive(isGlobalMap);
			}

			this.ConfigureFogOfWarScreenSpaceRenderers(isGlobalMap);

			QualitySettings.shadowDistance = isGlobalMap ? 100f : 62.5f;
		}

		[NewMember]
		private void ConfigureFogOfWarScreenSpaceRenderers(bool isGlobalMap)
		{
			var components = this.GetComponentInChildren<FogOfWarScreenSpaceRenderer>();

			if (components)
			{
				components.enabled = !isGlobalMap;
			}
		}

		[NewMember]
		private void SetCameraFieldOfView(bool isGlobalMap)
		{
			if (isGlobalMap)
			{
				this.Camera.fieldOfView = KingmakerPatchSettings.CameraZoom.CameraZoomMaxGlobalMap;
				return;
			}

			switch (Kingmaker.Game.Instance.CurrentMode)
			{
				case GameModeType.Cutscene:
					this.Camera.fieldOfView = KingmakerPatchSettings.CameraZoom.CameraZoomMaxCutscene;
					break;
				case GameModeType.Dialog:
					this.Camera.fieldOfView = KingmakerPatchSettings.CameraZoom.CameraZoomMaxDialog;
					break;
				default:
					this.Camera.fieldOfView = this.CameraZoom.FovMax;
					break;
			}
		}
	}
}

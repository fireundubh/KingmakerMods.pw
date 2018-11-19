using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		private static float _cutsceneFovMax;

		[NewMember]
		private static float _dialogFovMax;

		[NewMember]
		private static float _globalMapFovMax;

		[ModifiesMember("m_CameraAttachPoint", ModificationScope.Nothing)]
		private Transform mod_m_CameraAttachPoint;

		[ModifiesMember("m_CameraMapAttachPoint", ModificationScope.Nothing)]
		private Transform mod_m_CameraMapAttachPoint;

		[NewMember]
		[DuplicatesBody("SetMapMode")]
		public void source_SetMapMode(bool isGlobalMap)
		{
			throw new DeadEndException("source_SetMapMode");
		}

		[ModifiesMember("SetMapMode")]
		public void mod_SetMapMode(bool isGlobalMap)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game.CameraZoom", "bEnabled");
				_cutsceneFovMax = UserConfig.Parser.GetValueAsFloat("Game.CameraZoom", "fCameraZoomMaxCutscene");
				_dialogFovMax = UserConfig.Parser.GetValueAsFloat("Game.CameraZoom", "fCameraZoomMaxDialog");
				_globalMapFovMax = UserConfig.Parser.GetValueAsFloat("Game.CameraZoom", "fCameraZoomMaxGlobalMap");
			}

			if (!_useMod)
			{
				this.source_SetMapMode(isGlobalMap);
				return;
			}

			if (this.Camera)
			{
				this.Camera.transform.SetParent(isGlobalMap ? this.mod_m_CameraMapAttachPoint : this.mod_m_CameraAttachPoint);

				if (isGlobalMap)
				{
					this.Camera.fieldOfView = _globalMapFovMax;
				}
				else
				{
					switch (Kingmaker.Game.Instance.CurrentMode)
					{
						case GameModeType.Cutscene:
							this.Camera.fieldOfView = _cutsceneFovMax;
							break;
						case GameModeType.Dialog:
							this.Camera.fieldOfView = _dialogFovMax;
							break;
						default:
							this.Camera.fieldOfView = this.CameraZoom.FovMax;
							break;
					}
				}

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

			FogOfWarScreenSpaceRenderer componentInChildren = this.GetComponentInChildren<FogOfWarScreenSpaceRenderer>();

			if (componentInChildren)
			{
				componentInChildren.enabled = !isGlobalMap;
			}

			QualitySettings.shadowDistance = !isGlobalMap ? 62.5f : 100f;
		}
	}
}

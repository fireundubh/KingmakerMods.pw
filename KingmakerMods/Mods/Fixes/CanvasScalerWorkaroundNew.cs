﻿using Kingmaker.UI;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Fixes
{
	/// <summary>
	/// Fixes an issue where Canvas was improperly cast to RectTransform generating warnings
	/// </summary>
	[ModifiesType]
	public class CanvasScalerWorkaroundNew : CanvasScalerWorkaround
	{
		[ModifiesMember("ApplyWorkaround")]
		private void mod_ApplyWorkaround()
		{
			int childCount = this.transform.childCount;

			for (var i = 0; i < childCount; i++)
			{
				Transform child = this.transform.GetChild(i);

				// RectTransform rectTransform = child as RectTransform;

				child.SetParent(this.transform);
				var rectTransform = child.GetComponent<RectTransform>();

				if (rectTransform != null)
				{
					Vector2 anchoredPosition = rectTransform.anchoredPosition;
				}
			}
		}
	}
}

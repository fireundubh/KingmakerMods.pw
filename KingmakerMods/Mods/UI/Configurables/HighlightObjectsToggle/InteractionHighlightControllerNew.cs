using System;
using Kingmaker.Controllers;
using Kingmaker.Controllers.MapObjects;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.UI.SettingsUI;
using Kingmaker.Utility;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.UI.Configurables.HighlightObjectsToggle
{
	[ModifiesType]
	public class InteractionHighlightControllerNew : InteractionHighlightController
	{
		#region ALIASES

		[ModifiesMember("m_IsHighlighting", ModificationScope.Nothing)]
		private bool alias_m_IsHighlighting;

		[ModifiesMember("m_Inactive", ModificationScope.Nothing)]
		private bool alias_m_Inactive;

		#endregion

		#region DUPLICATES

		[NewMember]
		[DuplicatesBody("Activate")]
		public void source_Activate()
		{
			throw new DeadEndException("");
		}

		[NewMember]
		[DuplicatesBody("Deactivate")]
		public void source_Deactivate()
		{
			throw new DeadEndException("");
		}

		#endregion

		[NewMember]
		private TimeSpan m_LastTickTime;

		[NewMember]
		private void ToggleHighlight()
		{
			if (this.alias_m_Inactive)
			{
				return;
			}

			this.alias_m_IsHighlighting = !this.alias_m_IsHighlighting;

			UpdateHighlights();

			EventBus.RaiseEvent((IInteractionHighlightUIHandler h) => h.HandleHighlightChange(this.alias_m_IsHighlighting));
		}

		[NewMember]
		private static void UpdateHighlights(bool raiseEvent = false)
		{
			foreach (MapObjectEntityData mapObjectEntityData in Kingmaker.Game.Instance.State.MapObjects)
			{
				mapObjectEntityData.View.UpdateHighlight();
			}

			foreach (UnitEntityData unitEntityData in Kingmaker.Game.Instance.State.Units)
			{
				unitEntityData.View.UpdateHighlight(raiseEvent);
			}
		}

		[ModifiesMember("Activate")]
		public void mod_Activate()
		{
			if (!KingmakerPatchSettings.HighlightObjects.Enabled)
			{
				this.source_Activate();
				return;
			}

			Kingmaker.Game.Instance.Keyboard.Bind(SettingsRoot.Instance.HighlightObjects.name + UIConsts.SuffixOn, this.ToggleHighlight);

			this.alias_m_Inactive = false;
		}

		[ModifiesMember("Deactivate")]
		public void mod_Deactivate()
		{
			if (!KingmakerPatchSettings.HighlightObjects.Enabled)
			{
				this.source_Deactivate();
				return;
			}

			Kingmaker.Game.Instance.Keyboard.Unbind(SettingsRoot.Instance.HighlightObjects.name + UIConsts.SuffixOn, this.ToggleHighlight);

			if (this.alias_m_IsHighlighting)
			{
				this.ToggleHighlight();
			}

			this.alias_m_Inactive = true;
		}

		[ModifiesMember("Tick")]
		public void mod_Tick()
		{
			if (!KingmakerPatchSettings.HighlightObjects.Enabled || this.alias_m_Inactive || !this.alias_m_IsHighlighting)
			{
				return;
			}

			if (Kingmaker.Game.Instance.TimeController.GameTime - this.m_LastTickTime < KingmakerPatchSettings.HighlightObjects.SecondsBetweenTicksGameTime.Seconds())
			{
				return;
			}

			this.m_LastTickTime = Kingmaker.Game.Instance.TimeController.GameTime;

			UpdateHighlights(true);
		}
	}
}

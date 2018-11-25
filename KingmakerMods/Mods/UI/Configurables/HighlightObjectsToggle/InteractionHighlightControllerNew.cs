using System;
using Kingmaker.Controllers;
using Kingmaker.Controllers.MapObjects;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.UI.Configurables.HighlightObjectsToggle
{
	[ModifiesType]
	public class InteractionHighlightControllerNew : InteractionHighlightController
	{
		#region CONFIGURATION
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		private static int _secondsBetweenTicksGameTime;

		[NewMember]
		private static bool IsModReady()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("UI.HighlightObjectsToggle", "bEnabled");
				_secondsBetweenTicksGameTime = UserConfig.Parser.GetValueAsInt("UI.HighlightObjectsToggle", "iSecondsBetweenTicksGameTime");
			}

			return _useMod;
		}
		#endregion

		#region DUPLICATED METHODS
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

		[ModifiesMember("m_IsHighlighting", ModificationScope.Nothing)]
		private bool source_m_IsHighlighting;

		[ModifiesMember("m_Inactive", ModificationScope.Nothing)]
		private bool source_m_Inactive;

		[NewMember]
		private TimeSpan m_LastTickTime;

		[ModifiesMember("Activate")]
		public void mod_Activate()
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				this.source_Activate();
				return;
			}

			Kingmaker.Game.Instance.Keyboard.Bind("HighlightObjectsOn", this.ToggleHighlight);

			this.source_m_Inactive = false;
		}

		[ModifiesMember("Deactivate")]
		public void mod_Deactivate()
		{
			_useMod = IsModReady();

			if (!_useMod)
			{
				this.source_Deactivate();
				return;
			}

			Kingmaker.Game.Instance.Keyboard.Unbind("HighlightObjectsOn", this.ToggleHighlight);

			if (this.source_m_IsHighlighting)
			{
				this.ToggleHighlight();
			}

			this.source_m_Inactive = true;
		}

		[ModifiesMember("Tick")]
		public void mod_Tick()
		{
			if (!_useMod || this.source_m_Inactive || !this.source_m_IsHighlighting)
			{
				return;
			}

			if (Kingmaker.Game.Instance.TimeController.GameTime - this.m_LastTickTime < _secondsBetweenTicksGameTime.Seconds())
			{
				return;
			}

			this.m_LastTickTime = Kingmaker.Game.Instance.TimeController.GameTime;

			this.UpdateHighlights(true);
		}

		[NewMember]
		private void UpdateHighlights(bool raiseEvent = false)
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

		[NewMember]
		private void ToggleHighlight()
		{
			_useMod = IsModReady();

			if (this.source_m_Inactive)
			{
				return;
			}

			this.source_m_IsHighlighting = !this.source_m_IsHighlighting;

			this.UpdateHighlights();

			EventBus.RaiseEvent((IInteractionHighlightUIHandler h) => h.HandleHighlightChange(this.source_m_IsHighlighting));
		}
	}
}

using Kingmaker.EntitySystem.Entities;
using Kingmaker.UI.Overtip;
using Patchwork;
#pragma warning disable 1570

namespace KingmakerMods.Mods.Fixes
{
	/// <summary>
	/// Occurs whenever highlight key is pressed
	///
	/// Object reference not set to an instance of an object
	///		at Kingmaker.UI.Overtip.OvertipControllerBase.HandleHighlightChange () [0x00079] in <ce7a2e7c39614ae6a8d3ccb1a6c99337>:0
	///		at Kingmaker.UI.Overtip.OvertipControllerBase.HandleHighlightChange (System.Boolean isOn) [0x0004e] in <ce7a2e7c39614ae6a8d3ccb1a6c99337>:0
	///		at Kingmaker.Controllers.MapObjects.InteractionHighlightController.<HighlightOn>m__0 (Kingmaker.PubSubSystem.IInteractionHighlightUIHandler h) [0x00000] in <ce7a2e7c39614ae6a8d3ccb1a6c99337>:0
	///		at Kingmaker.PubSubSystem.SubscriptionManager`1[TSubscriber].RaiseEvent[T] (System.Action`1[T] action) [0x000a5] in <ce7a2e7c39614ae6a8d3ccb1a6c99337>:0
	/// </summary>
	[ModifiesType]
	public class OvertipControllerBaseNew : OvertipControllerBase
	{
		[ModifiesMember("HandleHighlightChange")]
		public virtual void mod_HandleHighlightChange()
		{
			UnitEntityData unit = this.Unit;
			MapObjectEntityData mapObject = this.MapObject;

			if (unit != null)
			{
				this.ObjectIsHovered = unit.View.MouseHighlighted;
				this.ObjectIsHovered = this.ObjectIsHovered && (!unit.Descriptor.State.IsDead || unit.IsPlayerFaction);
			}
			else if (mapObject != null)
			{
				this.ObjectIsHovered = mapObject.View.Highlighted && !this.BarkVisible;
			}
			else
			{
				this.ObjectIsHovered = false;
			}

			this.TweenComponentsToPlace();
		}

		[ModifiesMember("HandleHighlightChange")]
		public virtual void mod_HandleHighlightChange(bool isOn)
		{
			UnitEntityData unit = this.Unit;

			this.ForceHotKeyPressed = isOn;
			this.ForceHotKeyPressed = this.ForceHotKeyPressed && unit != null && (!unit.Descriptor.State.IsDead || unit.IsPlayerFaction);

			this.HandleHighlightChange();
		}
	}
}

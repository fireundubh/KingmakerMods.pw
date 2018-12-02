using System.Collections.Generic;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UI.Common;
using Kingmaker.UI.Group;
using KingmakerMods.Helpers;
using Patchwork;

// Occurs when opening the inventory screen if WithRemote == true

// line numbers from dotPeek decompilation output

// [196.7151]: Index was out of range. Must be non-negative and less than the size of the collection.
// Parameter name: index
// at System.ThrowHelper.ThrowArgumentOutOfRangeException (System.ExceptionArgument argument, System.ExceptionResource resource) [0x00029] in <e1a80661d61443feb3dbdaac88eeb776>:0
// at System.ThrowHelper.ThrowArgumentOutOfRangeException () [0x00000] in <e1a80661d61443feb3dbdaac88eeb776>:0
// at System.Collections.Generic.List`1[T].get_Item (System.Int32 index) [0x00009] in <e1a80661d61443feb3dbdaac88eeb776>:0
// at Kingmaker.UI.Group.GroupController.SetGroup () [0x0001a] in GroupController.cs:190
// at Kingmaker.UI.Group.GroupController.FullScreenChanged (System.Boolean state, Kingmaker.UI.FullScreenUITypes.FullScreenUIType fullScreenUIType) [0x00108] in GroupController.cs:386
// at Kingmaker.UI.Group.GroupController.HandleFullScreenUiChanged (System.Boolean state, Kingmaker.UI.FullScreenUITypes.FullScreenUIType fullScreenUIType) [0x00000] in GroupController.cs:337
// at Kingmaker.UI.ServiceWindow.FullScreenTabsWindow.<OnShow>m__0 (Kingmaker.PubSubSystem.IFullScreenUIHandler h) [0x00000] in FullScreenTabsWindow.cs:37
// at Kingmaker.PubSubSystem.SubscriptionManager`1[TSubscriber].RaiseEvent[T] (System.Action`1[T] action) [0x00090] in SubscriptionManager`1.cs:59

namespace KingmakerMods.Mods.Fixes
{
	[ModifiesType]
	public class GroupControllerNew : GroupController
	{
		[ModifiesMember("m_StartIndex", ModificationScope.Nothing)]
		private int alias_m_StartIndex;

		[ModifiesMember("SetCharacter", ModificationScope.Nothing)]
		private void SetCharacter(UnitEntityData character, int index)
		{
			throw new DeadEndException("alias_SetCharacter");
		}

		[NewMember]
		private void SetCharactersInGroup(IEnumerable<UnitEntityData> group, ref int index)
		{
			foreach (UnitEntityData unit in group)
			{
				this.SetCharacter(unit, index++);
			}
		}

		[ModifiesMember("SetGroup")]
		private void mod_SetGroup()
		{
			var index = 0;

			List<UnitEntityData> group = UIUtility.GetGroup(this.WithRemote);

			if (this.WithRemote)
			{
				this.SetCharactersInGroup(group, ref index);
				return;
			}

			this.SetCharactersInGroup(group, ref index);

			for (int j = index; j < 6; j++)
			{
				this.SetCharacter(null, j);
			}
		}
	}
}

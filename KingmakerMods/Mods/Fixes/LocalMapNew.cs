using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kingmaker.UI.ServiceWindow.LocalMap;
using Patchwork;

//		InvalidOperationException: Collection was modified; enumeration operation may not execute.
//			System.ThrowHelper.ThrowInvalidOperationException () (at <e1a80661d61443feb3dbdaac88eeb776>:0)
//			System.Collections.Generic.List`1[T].ForEach () (at <e1a80661d61443feb3dbdaac88eeb776>:0)
//			Kingmaker.UI.ServiceWindow.LocalMap.LocalMap+MarkSet.UpdateMarks () (at <35fa66bfb9784f98af1b75af220daab8>:0)
//			Kingmaker.UI.ServiceWindow.LocalMap.LocalMap.Update () (at <35fa66bfb9784f98af1b75af220daab8>:0)

namespace KingmakerMods.Mods.Fixes
{
	/// <summary>
	/// Fixes an issue where the list of local map marks was altered in another thread causing InvalidOperationExceptions
	/// </summary>
	[ModifiesType]
	public class LocalMapNew : LocalMap
	{
		[ModifiesType]
		public class mod_MarkSet : MarkSet
		{
			[ToggleFieldAttributes(FieldAttributes.InitOnly)]
			[ModifiesAccessibility("m_Marks")]
			private List<LocalMapMark> mod_m_Marks = new List<LocalMapMark>();

			[ModifiesMember("UpdateMarks")]
			public void mod_UpdateMarks()
			{
				this.mod_m_Marks.ToList().ForEach(m => m.DoUpdate());
			}
		}
	}
}

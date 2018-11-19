using System.Collections.Generic;
using System.Linq;
using Kingmaker.AreaLogic.SummonPool;
using Kingmaker.Blueprints;
using Kingmaker.Designers;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.EventConditionActionSystem.ContextData;
using Kingmaker.EntitySystem.Entities;
using Patchwork;

namespace KingmakerMods.Mods.Fixes
{
	/// <summary>
	/// Fixes an issue where the SummonPool units was altered in another thread causing NullReferenceExceptions
	/// </summary>
	[ModifiesType]
	public class SummonPoolUnitsNew : SummonPoolUnits
	{
		[ModifiesMember("RunAction")]
		public void mod_RunAction()
		{
			BlueprintSummonPool bpSummonPool = this.SummonPool;

			if (bpSummonPool == null)
			{
				return;
			}

			ISummonPool summonPool = GameHelper.GetSummonPool(bpSummonPool);

			IEnumerable<UnitEntityData> units = summonPool?.Units.ToList();

			if (units == null)
			{
				return;
			}

			foreach (UnitEntityData unit in units.Where(unit => unit != null))
			{
				using (new SummonPoolUnitData(unit))
				{
					if (this.Conditions.Check())
					{
						this.Actions.Run();
					}
				}
			}
		}
	}
}

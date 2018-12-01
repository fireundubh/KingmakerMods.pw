using System;
using Kingmaker.Kingdom;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Game.Configurables.CurrencyFallback
{
	[NewType]
	public static class KingdomCurrencyFallback
	{
		public static bool CanSpend(int pointCost)
		{
			if (KingdomState.Instance.BP - pointCost >= 0)
			{
				return true;
			}

			int currencyMult = KingmakerPatchSettings.CurrencyFallback.CurrencyMultiplier;

			return KingdomState.Instance.BP * currencyMult + Kingmaker.Game.Instance.Player.Money - pointCost * currencyMult >= 0;
		}

		public static bool SpendPoints(int pointCost)
		{
			int pointDebt = KingdomState.Instance.BP - pointCost;

			if (pointDebt >= 0)
			{
				KingdomState.Instance.BP -= pointCost;
				return true;
			}

			int pointCostNew = pointCost - Mathf.Abs(pointDebt);

			int goldCost = Mathf.Abs(pointDebt) * KingmakerPatchSettings.CurrencyFallback.CurrencyMultiplier;

			if (!Kingmaker.Game.Instance.Player.SpendMoney(goldCost))
			{
				return false;
			}

			KingdomState.Instance.BP -= pointCostNew;
			return true;
		}

		public static Tuple<int, int> SplitCost(int pointCost)
		{
			int pointDebt = KingdomState.Instance.BP - pointCost;

			if (pointDebt >= 0)
			{
				return new Tuple<int, int>(pointCost, 0);
			}

			int pointCostNew = pointCost - Mathf.Abs(pointDebt);

			int goldCost = Mathf.Abs(pointDebt) * KingmakerPatchSettings.CurrencyFallback.CurrencyMultiplier;

			return new Tuple<int, int>(pointCostNew, goldCost);
		}
	}
}

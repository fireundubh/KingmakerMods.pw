using System;
using Kingmaker.Kingdom;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Game.Configurables.CurrencyFallback
{
	[NewType]
	public static class KingdomCurrencyFallback
	{
		private static int _currencyMult;

		public static bool CanSpend(int pointCost)
		{
			_currencyMult = UserConfig.Parser.GetValueAsInt("Game.KingdomEvents", "iCurrencyMultiplier");

			if (KingdomState.Instance.BP - pointCost >= 0)
			{
				return true;
			}

			return KingdomState.Instance.BP * _currencyMult + Kingmaker.Game.Instance.Player.Money - pointCost * _currencyMult >= 0;
		}

		public static bool SpendPoints(int pointCost)
		{
			_currencyMult = UserConfig.Parser.GetValueAsInt("Game.KingdomEvents", "iCurrencyMultiplier");

			int pointDebt = KingdomState.Instance.BP - pointCost;

			if (pointDebt >= 0)
			{
				KingdomState.Instance.BP -= pointCost;
				return true;
			}

			int pointCostNew = pointCost - Mathf.Abs(pointDebt);

			int goldCost = Mathf.Abs(pointDebt) * _currencyMult;

			if (!Kingmaker.Game.Instance.Player.SpendMoney(goldCost))
			{
				return false;
			}

			KingdomState.Instance.BP -= pointCostNew;
			return true;
		}

		public static Tuple<int, int> SplitCost(int pointCost)
		{
			_currencyMult = UserConfig.Parser.GetValueAsInt("Game.KingdomEvents", "iCurrencyMultiplier");

			int pointDebt = KingdomState.Instance.BP - pointCost;

			if (pointDebt >= 0)
			{
				return new Tuple<int, int>(pointCost, 0);
			}

			int pointCostNew = pointCost - Mathf.Abs(pointDebt);

			int goldCost = Mathf.Abs(pointDebt) * _currencyMult;

			return new Tuple<int, int>(pointCostNew, goldCost);
		}
	}
}

using Kingmaker.Blueprints.Root;
using KingmakerMods.Helpers;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Game.Configurables.CompanionCost
{
	[ModifiesType]
	public class PlayerNew : Kingmaker.Player
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		private static bool _useMult;

		[NewMember]
		private static int _costMult;

		[NewMember]
		private static bool _usePartyLevelAsCostMult;

		[NewMember]
		[DuplicatesBody("GetCustomCompanionCost")]
		public int source_GetCustomCompanionCost()
		{
			throw new DeadEndException("source_GetCustomCompanionCost");
		}

		[ModifiesMember("GetCustomCompanionCost")]
		public int mod_GetCustomCompanionCost()
		{
			if (!_cfgInit)
			{
				_cfgInit = true;

				_usePartyLevelAsCostMult = UserConfig.Parser.GetValueAsBool("Game.CompanionCost", "bUsePartyLevelAsCostMultiplier");

				_useMult = UserConfig.Parser.GetValueAsBool("Game.CompanionCost", "bUseMultiplier");
				_costMult = UserConfig.Parser.GetValueAsInt("Game.CompanionCost", "iCostMultiplier");

				_useMod = _usePartyLevelAsCostMult || _useMult;
			}

			if (!_useMod)
			{
				return this.source_GetCustomCompanionCost();
			}

			if (_usePartyLevelAsCostMult)
			{
				return BlueprintRoot.Instance.CustomCompanionBaseCost * this.PartyLevel;
			}

			if (_useMult)
			{
				return BlueprintRoot.Instance.CustomCompanionBaseCost * _costMult;
			}

			return (int) (BlueprintRoot.Instance.CustomCompanionBaseCost * Mathf.Pow(this.PartyLevel, 2));
		}
	}
}

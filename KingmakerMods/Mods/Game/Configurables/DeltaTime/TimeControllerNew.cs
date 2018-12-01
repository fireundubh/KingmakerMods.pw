using Kingmaker.Controllers;
using Kingmaker.GameModes;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Configurables.DeltaTime
{
	[ModifiesType]
	public class TimeControllerNew : TimeController
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("get_DeltaTime")]
		public float source_get_DeltaTime()
		{
			throw new DeadEndException("source_get_DeltaTime");
		}
		#endregion

		[ModifiesMember("DeltaTime")]
		public float mod_DeltaTime
		{
			[ModifiesMember("get_DeltaTime")]
			get
			{
				if (!KingmakerPatchSettings.DeltaTime.Enabled)
				{
					return this.source_get_DeltaTime();
				}

				switch (Kingmaker.Game.Instance.CurrentMode)
				{
					case GameModeType.Default:
						return this.source_get_DeltaTime() * (Kingmaker.Game.Instance.Player.IsInCombat ? KingmakerPatchSettings.DeltaTime.CombatMultiplier : KingmakerPatchSettings.DeltaTime.OutOfCombatMultiplier);
					case GameModeType.GlobalMap:
					case GameModeType.Kingdom:
					case GameModeType.KingdomSettlement:
						return this.source_get_DeltaTime() * KingmakerPatchSettings.DeltaTime.GlobalMapMultiplier;
					default:
						return this.source_get_DeltaTime();
				}
			}
			[ModifiesMember("set_DeltaTime", ModificationScope.Nothing)]
			set
			{
				throw new DeadEndException("set_DeltaTime");
			}
		}
	}
}

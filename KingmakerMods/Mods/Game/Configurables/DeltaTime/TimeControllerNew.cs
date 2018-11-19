using Kingmaker.Controllers;
using Kingmaker.GameModes;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Game.Configurables.DeltaTime
{
	[ModifiesType]
	public class TimeControllerNew : TimeController
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[NewMember]
		private static float _gameSpeedOutOfCombat;

		[NewMember]
		private static float _gameSpeedCombat;

		[NewMember]
		private static float _gameSpeedGlobalMap;

		[MemberAlias(".ctor", typeof(object))]
		private void object_ctor()
		{
		}

		[ModifiesMember(".ctor")]
		public void CtorNew()
		{
			this.object_ctor();

			this.DebugTimeScale = 1f;
			this.PlayerTimeScale = 1f;

			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game.DeltaTime", "bEnabled");
				_gameSpeedOutOfCombat = UserConfig.Parser.GetValueAsFloat("Game.DeltaTime", "fOutOfCombatMultiplier");
				_gameSpeedCombat = UserConfig.Parser.GetValueAsFloat("Game.DeltaTime", "fCombatMultiplier");
				_gameSpeedGlobalMap = UserConfig.Parser.GetValueAsFloat("Game.DeltaTime", "fGlobalMapMultiplier");
			}
		}

		// workaround for getting the compiler-generated DeltaTime backing field

		[NewMember]
		[DuplicatesBody("get_DeltaTime")]
		public float source_get_DeltaTime()
		{
			throw new DeadEndException("source_get_DeltaTime");
		}

		[ModifiesMember("DeltaTime")]
		public float mod_DeltaTime
		{
			[ModifiesMember("get_DeltaTime")]
			get
			{
				if (!_useMod)
				{
					return source_get_DeltaTime();
				}

				switch (Kingmaker.Game.Instance.CurrentMode)
				{
					case GameModeType.Default:
						switch (Kingmaker.Game.Instance.Player.IsInCombat)
						{
							case true:
								return source_get_DeltaTime() * _gameSpeedCombat;
							case false:
								return source_get_DeltaTime() * _gameSpeedOutOfCombat;
						}

						break;
					case GameModeType.GlobalMap:
					case GameModeType.Kingdom:
					case GameModeType.KingdomSettlement:
						return source_get_DeltaTime() * _gameSpeedGlobalMap;
					default:
						return source_get_DeltaTime();
				}

				return source_get_DeltaTime();
			}
			[NewMember]
			[DuplicatesBody("set_DeltaTime")]
			set { throw new DeadEndException("set_DeltaTime"); }
		}
	}
}

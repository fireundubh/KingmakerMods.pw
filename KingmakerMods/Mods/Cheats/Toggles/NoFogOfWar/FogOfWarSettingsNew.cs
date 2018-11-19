using Kingmaker.Visual.FogOfWar;
using Patchwork;
using System.Linq;

namespace KingmakerMods.Mods.Cheats.Toggles.NoFogOfWar
{
	[ModifiesType]
	public class FogOfWarSettingsNew : FogOfWarSettings
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[ModifiesMember("Radius")]
		public float mod_Radius
		{
			[ModifiesMember("get_Radius")]
			get
			{
				if (!_cfgInit)
				{
					_cfgInit = true;
					_useMod = UserConfig.Parser.GetValueAsBool("Cheats", "bDisableFogOfWar");
				}

				if (!_useMod)
				{
					float radiusBase;

					if (this.IsGlobalMap)
					{
						if (Kingmaker.Game.Instance != null && Kingmaker.Game.Instance.Player != null && Kingmaker.Game.Instance.Player.ControllableCharacters != null)
						{
							float highestPerceptionScore = Kingmaker.Game.Instance.Player.ControllableCharacters.Select(u => u.Stats.SkillPerception.ModifiedValue).Max();
							radiusBase = 1f + highestPerceptionScore / 4f;
						}
						else
						{
							radiusBase = 7f;
						}
					}
					else
					{
						radiusBase = 11.7f;
					}

					return radiusBase + this.BorderOffset;
				}

				return float.MaxValue;
			}
		}
	}
}

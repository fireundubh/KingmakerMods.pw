using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Utility;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Fixes
{
	[ModifiesType("Kingmaker.Designers.GameHelper")]
	public static class GameHelperNew
	{
		// Occurred four times in a row when player sent kobolds to worg for dinner to steal their drugs

		// Object reference not set to an instance of an object
		//		at Kingmaker.Designers.GameHelper.ApplyBuff (Kingmaker.EntitySystem.Entities.UnitEntityData target, Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff buff, System.Nullable`1[T] duration) [0x0002f] in <ce7a2e7c39614ae6a8d3ccb1a6c99337>:0		//		at Kingmaker.Designers.EventConditionActionSystem.Actions.AttachBuff.RunAction () [0x00036] in <ce7a2e7c39614ae6a8d3ccb1a6c99337>:0		//		at Kingmaker.ElementsSystem.ActionList.Run () [0x00023] in <ce7a2e7c39614ae6a8d3ccb1a6c99337>:0
		//
		// NullReferenceException in Attach Buff (FadeInOut) (asset $AttachBuff$3f4b810b-1777-4656-95d3-2aeca7459703)
		//		Kingmaker.ElementsSystem.ActionList.Run() (at :0)
		//		Kingmaker.AreaLogic.Cutscenes.CommandAction.OnRun(Kingmaker.AreaLogic.Cutscenes.CutscenePlayerData player) (at :0)
		//		Kingmaker.AreaLogic.Cutscenes.CommandBase.Run(Kingmaker.AreaLogic.Cutscenes.CutscenePlayerData player) (at :0)
		//		Kingmaker.AreaLogic.Cutscenes.CutscenePlayerData+TrackData.StartCommand() (at :0)
		//		Kingmaker.AreaLogic.Cutscenes.CutscenePlayerData+TrackData.StartNextCommand() (at :0)
		//		Kingmaker.AreaLogic.Cutscenes.CutscenePlayerData+TrackData.Tick(Kingmaker.AreaLogic.Cutscenes.CutscenePlayerData player) (at :0)
		//		Kingmaker.AreaLogic.Cutscenes.CutscenePlayerData.TickScene() (at :0)
		//		Kingmaker.Controllers.CutsceneController.Tick() (at :0)
		//		Kingmaker.GameModes.GameMode.Tick() (at :0)
		//		Kingmaker.Game.Tick() (at :0)
		//		Kingmaker.Runner.Update() (at :0)

		[NewMember]
		[DuplicatesBody("ApplyBuff")]
		public static Buff source_ApplyBuff(UnitEntityData target, BlueprintBuff buff, Rounds? duration = null)
		{
			throw new DeadEndException("source_ApplyBuff");
		}

		[ModifiesMember("ApplyBuff")]
		public static Buff mod_ApplyBuff(UnitEntityData target, BlueprintBuff buff, Rounds? duration = null)
		{
			if (target == null)
			{
				UberDebug.LogError("target == null, returning null without calling target.Descriptor.AddBuff");
				return null;
			}

			if (buff == null)
			{
				UberDebug.LogError("buff == null, returning null without calling target.Descriptor.AddBuff");
				return null;
			}

			return source_ApplyBuff(target, buff, duration);
		}
	}
}

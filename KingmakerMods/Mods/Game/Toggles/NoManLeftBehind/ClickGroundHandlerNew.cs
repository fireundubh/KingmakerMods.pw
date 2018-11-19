using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kingmaker.Controllers.Clicks.Handlers;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Formations;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.Utility;
using KingmakerMods.Helpers;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Game.Toggles.NoManLeftBehind
{
	[ModifiesType]
	public class ClickGroundHandlerNew : ClickGroundHandler
	{
		[NewMember]
		private static bool _cfgInit;

		[NewMember]
		private static bool _useMod;

		[ModifiesMember("m_UnitWaitAgentList")]
		[ToggleFieldAttributes(FieldAttributes.InitOnly)]
		private static List<UnitEntityData> mod_m_UnitWaitAgentList = new List<UnitEntityData>();

		[NewMember]
		[DuplicatesBody("RunCommand")]
		private static void source_RunCommand(UnitEntityData unit, Vector3 p, float? speedLimit, float orientation, float delay)
		{
			throw new DeadEndException("source_RunCommand");
		}

		[NewMember]
		[DuplicatesBody("MoveSelectedUnitsToPoint")]
		public static void source_MoveSelectedUnitsToPoint(Vector3 worldPosition, Vector3 direction, bool preview = false, float formationSpaceFactor = 1f, Action<UnitEntityData, Vector3, float?, float, float> commandRunner = null)
		{
			throw new DeadEndException("source_MoveSelectedUnitsToPoint");
		}

		[ModifiesMember("RunCommand")]
		private static void mod_RunCommand(UnitEntityData unit, Vector3 p, float? speedLimit, float orientation, float delay)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game", "bNoManLeftBehind");
			}

			if (!_useMod)
			{
				source_RunCommand(unit, p, speedLimit, orientation, delay);
				return;
			}

			UnitMoveTo unitMoveTo = new UnitMoveTo(p, 0.3f) {MovementDelay = delay, Orientation = orientation, SpeedLimit = speedLimit, OverrideSpeed = speedLimit};

			unit.Commands.Run(unitMoveTo);

			if (unit.Commands.Queue.FirstOrDefault(c => c is UnitMoveTo) == unitMoveTo || Kingmaker.Game.Instance.IsPaused)
			{
				ShowDestination(unit, unitMoveTo.Target, false);
			}
		}

		[ModifiesMember("MoveSelectedUnitsToPoint")]
		public static void mod_MoveSelectedUnitsToPoint(Vector3 worldPosition, Vector3 direction, bool preview = false, float formationSpaceFactor = 1f, Action<UnitEntityData, Vector3, float?, float, float> commandRunner = null)
		{
			if (!_cfgInit)
			{
				_cfgInit = true;
				_useMod = UserConfig.Parser.GetValueAsBool("Game", "bNoManLeftBehind");
			}

			if (!_useMod)
			{
				source_MoveSelectedUnitsToPoint(worldPosition, direction, preview, formationSpaceFactor, commandRunner);
				return;
			}

			if (!preview)
			{
				mod_m_UnitWaitAgentList.Clear();
			}

			List<UnitEntityData> selectedUnits = Kingmaker.Game.Instance.UI.SelectionManager.GetSelectedUnits();

			List<UnitEntityData> allUnits;

			if (selectedUnits.Count == 1)
			{
				allUnits = selectedUnits;
			}
			else
			{
				allUnits = Kingmaker.Game.Instance.Player.ControllableCharacters.Where(c => c.IsDirectlyControllable).ToList();
			}

			float orientation = Mathf.Atan2(direction.x, direction.z) * 57.29578f;

			float speedLimit = 0f;

			if (selectedUnits.Count > 1)
			{
				speedLimit = selectedUnits.Aggregate(float.MinValue, (current, u) => Mathf.Max(current <= 0f ? 0f : current, u.ModifiedSpeedMps));
			}

			if (selectedUnits.Count <= 0)
			{
				return;
			}

			int[] array = new int[allUnits.Count];

			for (int i = 0; i < array.Length; i++)
			{
				array[i] = i;
			}

			Array.Sort(array, (o1, o2) => (allUnits[o1].Position - worldPosition).sqrMagnitude.CompareTo((allUnits[o2].Position - worldPosition).sqrMagnitude));

			PartyFormationHelper.FillFormationPositions(worldPosition, FormationAnchor.Front, direction, allUnits, selectedUnits, formationSpaceFactor);

			int count = 0;

			for (int i = 0; i < allUnits.Count; i++)
			{
				UnitEntityData unit = allUnits[i];

				if (!selectedUnits.HasItem(unit))
				{
					continue;
				}

				if (preview)
				{
					ShowDestination(unit, PartyFormationHelper.ResultPositions[i], true);
				}
				else
				{
					if (commandRunner == null)
					{
						commandRunner = mod_RunCommand;
					}

					commandRunner(unit, PartyFormationHelper.ResultPositions[i], speedLimit > unit.CurrentSpeedMps ? speedLimit : unit.CurrentSpeedMps, orientation, array[count] * 0.05f);
				}

				count++;
			}

			float previousMagnitude = 0f;

			for (int i = 0; i < allUnits.Count; i++)
			{
				UnitEntityData unit = allUnits[i];

				if (!selectedUnits.HasItem(unit))
				{
					continue;
				}

				float currentMagnitude = (worldPosition - PartyFormationHelper.ResultPositions[i]).To2D().magnitude;

				if (currentMagnitude > previousMagnitude)
				{
					previousMagnitude = currentMagnitude;
				}
			}

			for (int i = 0; i < selectedUnits.Count; i++)
			{
				UnitEntityData selectedUnit = selectedUnits[i];

				if (allUnits.HasItem(selectedUnit))
				{
					continue;
				}

				Vector3 vector = selectedUnits.Count == 1 ? worldPosition : GeometryUtils.ProjectToGround(worldPosition - direction.normalized * (previousMagnitude + 2f));

				if (preview)
				{
					ShowDestination(selectedUnit, vector, true);
				}
				else
				{
					if (commandRunner == null)
					{
						commandRunner = mod_RunCommand;
					}

					commandRunner(selectedUnit, vector, speedLimit > selectedUnit.CurrentSpeedMps ? speedLimit : selectedUnit.CurrentSpeedMps, orientation, 0f);
				}
			}

			if (preview)
			{
				Kingmaker.Game.Instance.UI.ClickPointerManager.ShowPreviewArrow(worldPosition, direction);
			}
			else
			{
				Kingmaker.Game.Instance.UI.ClickPointerManager.CancelPreview();
			}

			EventBus.RaiseEvent<IClickActionHandler>(h => h.OnMoveRequested(worldPosition));
		}
	}
}

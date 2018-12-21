using System;
using System.IO;
using Kingmaker;
using Kingmaker.Stores;
using Kingmaker.Utility;
using KingmakerMods.Helpers;
using Patchwork;
using UberLogger;
using UnityEngine;

namespace KingmakerMods.Mods.Game.Toggles.Logging
{
	[ModifiesType]
	public class GameStarterNew : GameStarter
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("Awake")]
		public void source_Awake()
		{
			throw new DeadEndException("source_Awake");
		}
		#endregion

		[ModifiesMember("Awake")]
		public void mod_Awake()
		{
			if (!KingmakerPatchSettings.Game.Logging)
			{
				this.source_Awake();
				return;
			}

			try
			{
				string persistentDataPath = ApplicationPaths.persistentDataPath;

				Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);

				UberLogger.Logger.Enabled = true;

				if (UberLogger.Logger.Enabled)
				{
					string text = Path.Combine(persistentDataPath, "GameLog.txt");

					if (File.Exists(text))
					{
						File.Copy(text, Path.Combine(persistentDataPath, "GameLogPrev.txt"), true);

						// just clear the old log, instead of deleting it so we can keep it open
						File.Create(text).Close();
					}

					UberLogger.Logger.AddLogger(new UberLoggerFile(Application.isEditor ? "EditorLogFull.txt" : "GameLogFull.txt", persistentDataPath));
					UberLogger.Logger.AddLogger(new UberLoggerFilter(new UberLoggerFile(Application.isEditor ? "EditorLog.txt" : "GameLog.txt", persistentDataPath), LogSeverity.Warning, "MatchLight"));
				}
			}
			catch (Exception exception)
			{
				Debug.LogError("Can't initialize log file");
				Debug.LogException(exception);
			}

			AkSoundEngine.SetRTPCValue("AudioLevel", this.MasterVolume.NormalizedValue * 100f, null, 0);
			AkSoundEngine.SetRTPCValue("MusicLevel", this.MusicVolume.NormalizedValue * 100f, null, 0);
			StoreManager.InitializeStore();
			UberDebug.LogSystem("GameStarter.Awake finished");
		}
	}
}

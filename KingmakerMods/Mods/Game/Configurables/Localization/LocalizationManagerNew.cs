using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kingmaker.Localization;
using Kingmaker.Localization.Shared;
using Kingmaker.TextTools;
using KingmakerMods.Helpers;
using Newtonsoft.Json;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Game.Configurables.Localization
{
	[ModifiesType]
	public class LocalizationManagerNew : LocalizationManager
	{
		[ModifiesMember("s_CurrentLocale", ModificationScope.Nothing)]
		private static Locale source_s_CurrentLocale;

		[NewMember]
		private static void LoadPacks()
		{
			List<LocalizationPack> currentPacks = new List<LocalizationPack>();

			DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Application.dataPath, "Mods", "Localization/" + LocalizationManagerNew.source_s_CurrentLocale));
			FileInfo[] files;

			try
			{
				files = directoryInfo.GetFiles("*.json", SearchOption.AllDirectories);
			}
			catch (DirectoryNotFoundException ex)
			{
				UberDebug.LogError("Failed to load localization packs in: {0}", directoryInfo.FullName);
				UberDebug.LogException(ex);
				return;
			}

			foreach (FileInfo file in files.Where(file => file.Exists))
			{
				try
				{
					using (StreamReader streamReader = new StreamReader(file.FullName))
					{
						currentPacks.Add(JsonConvert.DeserializeObject<LocalizationPack>(streamReader.ReadToEnd()));
					}
				}
				catch (Exception ex)
				{
					UberDebug.LogError("Failed to load localization pack: {0}", file.FullName);
					UberDebug.LogException(ex);
				}
			}

			foreach (LocalizationPack currentPack in currentPacks)
			{
				foreach (KeyValuePair<string, string> keyValuePair in currentPack.Strings)
				{
					if (CurrentPack == null)
					{
						continue;
					}

					if (CurrentPack.Strings.ContainsKey(keyValuePair.Key))
					{
						CurrentPack.Strings.Remove(keyValuePair.Key);
					}

					CurrentPack.Strings.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		[ModifiesMember("CurrentLocale")]
		public static Locale mod_CurrentLocale
		{
			[ModifiesMember("get_CurrentLocale")]
			get { return LocalizationManagerNew.source_s_CurrentLocale; }
			[ModifiesMember("set_CurrentLocale")]
			set
			{
				Locale locale = LocalizationManagerNew.source_s_CurrentLocale;

				LocalizationManagerNew.source_s_CurrentLocale = value;

				PlayerPrefs.SetString("LocalePref", value.ToString());

				if (Application.isPlaying)
				{
					LocalizationManagerNew.CurrentPack = LocalizationManagerNew.source_LoadPack(LocalizationManagerNew.source_s_CurrentLocale);
					LocalizationManagerNew.LoadPacks();
				}

				if (locale != LocalizationManagerNew.source_s_CurrentLocale)
				{
					LocalizationManagerNew.source_OnLocaleChanged();
				}
			}
		}

		[ModifiesMember("LoadPack", ModificationScope.Nothing)]
		private static LocalizationPack source_LoadPack(Locale locale)
		{
			throw new DeadEndException("source_LoadPack");
		}

		[ModifiesMember("OnLocaleChanged", ModificationScope.Nothing)]
		private static void source_OnLocaleChanged()
		{
			throw new DeadEndException("source_OnLocaleChanged");
		}

		[NewMember]
		public static string LoadString(string assetId)
		{
			string result;

			try
			{
				string text = CurrentPack?.GetText(assetId) ?? "<null>";

				if (Application.isPlaying)
				{
					result = TextTemplateEngine.Process(text);
				}
				else
				{
					result = text;
				}
			}
			finally
			{
			}

			return result;
		}
	}
}

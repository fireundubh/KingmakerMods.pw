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
		#region Public Properties

		[ModifiesMember("CurrentLocale")]
		public static Locale mod_CurrentLocale
		{
			[ModifiesMember("get_CurrentLocale")]
			get
			{
				return alias_s_CurrentLocale;
			}
			[ModifiesMember("set_CurrentLocale")]
			set
			{
				Locale locale = alias_s_CurrentLocale;

				alias_s_CurrentLocale = value;

				PlayerPrefs.SetString("LocalePref", value.ToString());

				if (Application.isPlaying)
				{
					CurrentPack = alias_LoadPack(alias_s_CurrentLocale);
					LoadPacks();
				}

				if (locale != alias_s_CurrentLocale)
				{
					alias_OnLocaleChanged();
				}
			}
		}

		#endregion

		#region Public Methods and Operators

		[NewMember]
		public static string LoadString(string assetId)
		{
			string text = CurrentPack?.GetText(assetId) ?? "<null>";

			return Application.isPlaying ? TextTemplateEngine.Process(text) : text;
		}

		#endregion

		#region Methods

		[NewMember]
		private static void LoadPacks()
		{
			var currentPacks = new List<LocalizationPack>();

			var directoryInfo = new DirectoryInfo(Path.Combine(Application.dataPath, "Mods", "Localization/" + alias_s_CurrentLocale));
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
					using (var streamReader = new StreamReader(file.FullName))
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

			if (CurrentPack == null)
			{
				return;
			}

			foreach (LocalizationPack currentPack in currentPacks)
			{
				foreach (KeyValuePair<string, string> keyValuePair in currentPack.Strings)
				{
					if (CurrentPack.Strings.ContainsKey(keyValuePair.Key) == true)
					{
						CurrentPack.Strings.Remove(keyValuePair.Key);
					}

					CurrentPack.Strings.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		#endregion

		#region Aliases

		[ModifiesMember("s_CurrentLocale", ModificationScope.Nothing)]
		private static Locale alias_s_CurrentLocale;

		[ModifiesMember("LoadPack", ModificationScope.Nothing)]
		private static LocalizationPack alias_LoadPack(Locale locale)
		{
			throw new DeadEndException("source_LoadPack");
		}

		[ModifiesMember("OnLocaleChanged", ModificationScope.Nothing)]
		private static void alias_OnLocaleChanged()
		{
			throw new DeadEndException("source_OnLocaleChanged");
		}

		#endregion
	}
}

using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using IniParser;
using IniParser.Model;
using IniParser.Parser;
using Patchwork;

namespace KingmakerMods.UserConfig
{
	[NewType]
	public static class Parser
	{
		private static readonly IniData parsedFile;

		private static readonly bool loggingEnabled;

		static Parser()
		{
			IniDataParser parser = new IniDataParser();

			IniParser.Model.Configuration.IniParserConfiguration config = parser.Configuration;
			config.AllowDuplicateKeys = true;
			config.OverrideDuplicateKeys = true;
			config.SkipInvalidLines = true;
			config.ThrowExceptionsOnError = false;

			FileIniDataParser fileParser = new FileIniDataParser(parser);
			parsedFile = fileParser.ReadFile("KingmakerMods.ini");

			loggingEnabled = LoggingEnabled();
		}

		public static string GetAllIniDataAsString()
		{
			return parsedFile.ToString();
		}

		public static IniData GetIniData()
		{
			return parsedFile;
		}

		public static bool LoggingEnabled()
		{
			string value = parsedFile["System"]["bINILogging"];

			bool result;
			bool state = bool.TryParse(value, out result);

			return state && result;
		}

		private static void LogSuccess(string Category, string KeyName, string value)
		{
			if (!loggingEnabled)
			{
				return;
			}

			UberDebug.LogSystem("[fireundubh] Successfully parsed {0}.{1} with provided value ({2})", Category, KeyName, value);
		}

		private static void LogFailure(string Category, string KeyName, string value, bool isBoolean = false)
		{
			if (!loggingEnabled)
			{
				return;
			}

			if (isBoolean)
			{
				UberDebug.LogError("[fireundubh] Cannot parse {0}.{1} with provided value ({2}) - returning false", Category, KeyName, value);
			}
			else
			{
				UberDebug.LogError("[fireundubh] Cannot parse {0}.{1} with provided value ({2}) - throwing exception", Category, KeyName, value);
			}
		}

		public static bool GetValueAsBool(string Category, string KeyName)
		{
			string value = parsedFile[Category][KeyName];

			// remove all whitespace from string
			value = Utilities.TrimAll(value);

			bool result;
			bool state = bool.TryParse(value, out result);

			if (state)
			{
				LogSuccess(Category, KeyName, value);
				return result;
			}

			LogFailure(Category, KeyName, value, true);
			return false;
		}

		public static int GetValueAsInt(string Category, string KeyName)
		{
			string value = parsedFile[Category][KeyName];

			// remove all whitespace from string
			value = Utilities.TrimAll(value);

			int result;
			bool state = int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result);

			if (state)
			{
				LogSuccess(Category, KeyName, value);
				return result;
			}

			LogFailure(Category, KeyName, value);
			throw new FormatException("[fireundubh] UserConfig.Parser.GetValueAsInt()");
		}

		public static float GetValueAsFloat(string Category, string KeyName)
		{
			string value = parsedFile[Category][KeyName];

			// remove all whitespace from string
			value = Utilities.TrimAll(value);

			float result;
			bool state = float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result);

			if (state)
			{
				LogSuccess(Category, KeyName, value);
				return result;
			}

			LogFailure(Category, KeyName, value);
			throw new FormatException("[fireundubh] UserConfig.Parser.GetValueAsFloat()");
		}

		public static string GetValueAsString(string Category, string KeyName)
		{
			return parsedFile[Category][KeyName];
		}

		public static int[] GetValueAsIntArray(string Category, string KeyName)
		{
			string value = parsedFile[Category][KeyName];

			// remove all alphabetical characters from string
			value = Regex.Replace(value, "[^0-9. ,]", "");

			// remove all whitespace from string
			value = Utilities.TrimAll(value);

			// remove last comma from string
			value = value.TrimEnd(',');

			int[] result;

			try
			{
				result = value.Split(',').Select(int.Parse).ToArray();
			}
			catch (Exception e)
			{
				LogFailure(Category, KeyName, value);
				throw;
			}

			LogSuccess(Category, KeyName, value);
			return result;
		}
	}
}

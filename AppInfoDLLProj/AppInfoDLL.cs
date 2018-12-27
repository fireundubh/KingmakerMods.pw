using System.Linq;
using Patchwork.AutoPatching;
using System.IO;

namespace AppInfoDLLProj
{
	[AppInfoFactory]
	public class AppInfoDLL : AppInfoFactory
	{
		#region Public Methods and Operators

		public static string Combine(params string[] paths)
		{
			return paths.Aggregate(@"", Path.Combine);
		}

		public override AppInfo CreateInfo(DirectoryInfo folderInfo)
		{
			var ai = new AppInfo
			{
				AppName = "Pathfinder: Kingmaker",
				AppVersion = "1.0.21",
				BaseDirectory = folderInfo,
				GogAppID = "1982293831",
				SteamAppID = "640820"
			};

			string appPath = Combine(ai.BaseDirectory.ToString(), "Kingmaker.exe");
			ai.Executable = new FileInfo(appPath);

			ai.GogArguments = string.Format("/command=runGame /gameId={0} /path=\"{1}\"", ai.GogAppID, ai.BaseDirectory);
			ai.SteamArguments = string.Format("-applaunch {0}", ai.SteamAppID);

			return ai;
		}

		#endregion
	}
}

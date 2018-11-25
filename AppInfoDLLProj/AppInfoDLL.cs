using System.Linq;
using Patchwork.AutoPatching;
using System.IO;

namespace AppInfoDLLProj
{
	[AppInfoFactory]
	public class AppInfoDLL : AppInfoFactory
	{
		public override AppInfo CreateInfo(DirectoryInfo folderInfo)
		{
			AppInfo ai = new AppInfo
			{
				AppName = "Pathfinder: Kingmaker",
				AppVersion = "1.0.16",
				BaseDirectory = folderInfo
			};

			ai.Executable = new FileInfo(Combine(ai.BaseDirectory.ToString(), "Kingmaker.exe"));
			//ai.IconLocation = new FileInfo("D:/Games/Tyranny/goggame-1266051739.ico");

			return ai;
		}

		public static string Combine(params string[] paths)
		{
			string current = paths.Aggregate(@"", Path.Combine);
			return current;
		}
	}
}

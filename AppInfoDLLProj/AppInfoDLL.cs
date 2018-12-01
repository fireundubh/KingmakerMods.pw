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
			var ai = new AppInfo
			{
				AppName = "Pathfinder: Kingmaker",
				AppVersion = "1.0.17",
				BaseDirectory = folderInfo
			};

			string appPath = Combine(ai.BaseDirectory.ToString(), "Kingmaker.exe");
			ai.Executable = new FileInfo(appPath);

			return ai;
		}

		public static string Combine(params string[] paths)
		{
			return paths.Aggregate(@"", Path.Combine);
		}
	}
}

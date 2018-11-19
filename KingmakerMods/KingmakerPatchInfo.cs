using System.IO;
using System.Linq;
using Patchwork;
using Patchwork.AutoPatching;

[assembly: PatchAssembly]

namespace KingmakerMods
{
	[PatchInfo]
	public class KingmakerPatchInfo : IPatchInfo
	{
		public static string Combine(params string[] paths)
		{
			string current = paths.Aggregate(@"", Path.Combine);
			return current;
		}

		public FileInfo GetTargetFile(AppInfo app)
		{
			string file = Combine(app.BaseDirectory.FullName, "Kingmaker_Data", "Managed", "Assembly-CSharp.dll");
			FileInfo info = new FileInfo(file);
			return info;
		}

		public string CanPatch(AppInfo app)
		{
			return null;
		}

		public string PatchVersion
		{
			get { return "1.0.0.000"; }
		}

		public string Requirements
		{
			get { return "None"; }
		}

		public string PatchName
		{
			get { return "KingmakerMods"; }
		}
	}
}

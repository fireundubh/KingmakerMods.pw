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

		public FileInfo GetTargetFile(AppInfo app)
		{
			string filePath = Combine(app.BaseDirectory.FullName, "Kingmaker_Data", "Managed", "Assembly-CSharp.dll");
			return new FileInfo(filePath);
		}

		public string CanPatch(AppInfo app)
		{
			return null;
		}

		public static string Combine(params string[] paths)
		{
			return paths.Aggregate(@"", Path.Combine);
		}
	}
}

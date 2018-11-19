using Patchwork;

namespace KingmakerMods.Helpers
{
	/// <summary>
	/// Indicates that the code should be unreachable.
	/// </summary>
	[NewType]
	public class DeadEndException : KingmakerModsException
	{
		public DeadEndException(string location) : base($"Code should be unreachable. Location: {location}")
		{
		}
	}
}

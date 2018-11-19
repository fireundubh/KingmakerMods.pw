using System;
using System.Runtime.Serialization;
using Patchwork;

namespace KingmakerMods.Helpers
{
	[NewType]
	public class KingmakerModsException : Exception
	{
		public KingmakerModsException(string message) : base(message)
		{
		}

		public KingmakerModsException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected KingmakerModsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

using System;

namespace Taksa.Framework
{
	public class WrongExpectedStreamVersionException : Exception
	{
		public WrongExpectedStreamVersionException(string message, Exception innerException = null)
			: base(message, innerException)
		{
		}
	}
}
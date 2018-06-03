using System;

namespace Taksa.Domain.Bribes
{
	public static class Exceptions
	{
		public class BribeNotFoundException : Exception
		{
			public BribeNotFoundException() {}

			public BribeNotFoundException(Guid id) 
				: base($"Bribe with id '{id}' was not found") {}
		}
	}
}

using System;
using Taksa.Framework;

namespace Taksa.Domain.Bribes
{
	public class BribeTimestamp : Value<BribeTimestamp>
	{
		public DateTimeOffset Value { get; }

		public BribeTimestamp(DateTimeOffset value)
		{
			Value = value;
		}
	}
}

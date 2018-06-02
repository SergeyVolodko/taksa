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

		public static explicit operator DateTimeOffset(BribeTimestamp self) => self.Value;

		public static explicit operator BribeTimestamp(DateTimeOffset value) => new BribeTimestamp(value);
	}
}

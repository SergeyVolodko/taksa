using Taksa.Framework;

namespace Taksa.Domain.Bribes
{
	public class BribeName : Value<BribeName>
	{
		public string Value { get; }

		public BribeName(string value)
		{
			Value = value;
		}

		public static explicit operator string(BribeName self) => self.Value;

		public static explicit operator BribeName(string value) => new BribeName(value);
	}
}

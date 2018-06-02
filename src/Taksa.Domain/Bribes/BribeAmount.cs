using Taksa.Framework;

namespace Taksa.Domain.Bribes
{
	public class BribeAmount : Value<BribeAmount>
	{
		public MoneyRange Value { get; }

		public BribeAmount(MoneyRange value)
		{
			Value = value;
		}

		public static explicit operator MoneyRange(BribeAmount self) => self.Value;

		public static explicit operator BribeAmount(MoneyRange value) => new BribeAmount(value);
	}
}

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
	}
}

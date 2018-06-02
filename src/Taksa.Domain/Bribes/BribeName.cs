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
	}
}

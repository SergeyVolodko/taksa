using Taksa.Framework;

namespace Taksa.Domain.Bribes
{
	public class BribeComment : Value<BribeComment>
	{
		public string Value { get; }

		public BribeComment(string value)
		{
			Value = value;
		}
	}
}

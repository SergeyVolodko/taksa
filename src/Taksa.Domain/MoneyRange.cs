using Exact.Go.Api.Domain.Core.Models;

namespace Taksa.Domain
{
	public class MoneyRange
	{
		public Money Start { get; set; }

		public Money End { get; set; }

		public MoneyRange()
		{
			Start = Money.Zero;
			End = Money.Zero;
		}
	}
}

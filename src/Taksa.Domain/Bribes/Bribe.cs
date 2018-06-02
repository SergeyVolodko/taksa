using System;
using Taksa.Framework;

namespace Taksa.Domain.Bribes
{
	public class Bribe : Aggregate
	{
		private BribeName name;
		private BribeComment comment;

		private BribeAmount amount;

		private DateTimeOffset timestamp;
		//private bool isPublished;

		protected override void When(object e)
		{
			throw new System.NotImplementedException();
		}
	}
}

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
			switch (e)
			{
				case Events.V1.BribeCreated x:
					Id = x.Id;
					name = (BribeName)x.Name;
					amount = (BribeAmount)x.Amount;
					timestamp = x.Timestamp;
					break;
			}
		}

		public static Bribe Create(
			BribeName name,
			BribeAmount amount,
			DateTimeOffset timestamp)
		{
			var bribe = new Bribe();
			bribe.Apply(new Events.V1.BribeCreated
			{
				Id = Guid.NewGuid(),
				Name = (string)name,
				Amount = (MoneyRange)amount,
				Timestamp = timestamp,
				CreatedAt = DateTimeOffset.UtcNow
			});

			return bribe;
		}
	}
}

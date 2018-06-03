using System;
using Taksa.Framework;

namespace Taksa.Domain.Bribes
{
	public class Bribe : Aggregate
	{
		private ServiceName name;

		private BribeComment comment;

		private ServiceCategory category;

		//private BribeAddress address;

		private BribeAmount amount;

		private BribeTimestamp timestamp;

		private bool isPublished;

		protected override void When(object e)
		{
			switch (e)
			{
				case Events.V1.BribeCreated x:
					Id = x.Id;
					name = (ServiceName)x.Name;
					amount = (BribeAmount)x.Amount;
					timestamp = (BribeTimestamp)x.Timestamp;
					break;

				case Events.V1.BribePublished x:
					category = new ServiceCategory(x.CategoryLocal, x.CategoryInternational);
					isPublished = true;
					break;
			}
		}

		public static Bribe Create(
			ServiceName name,
			BribeAmount amount,
			BribeTimestamp timestamp)
		{
			var bribe = new Bribe();
			bribe.Apply(new Events.V1.BribeCreated
			{
				Id = Guid.NewGuid(),
				Name = (string)name,
				Amount = (MoneyRange)amount,
				Timestamp = (DateTimeOffset)timestamp,
				CreatedAt = DateTimeOffset.UtcNow
			});

			return bribe;
		}

		public void Publish(
			ServiceCategory category,
			DateTimeOffset publishedAt) =>
			Apply(new Events.V1.BribePublished
			{
				Id = Id,
				CategoryLocal = category.LocalValue,
				CategoryInternational = category.InternationalValue,
				PublishedAt = publishedAt
			});
	}
}

using System;
using Taksa.Framework;

namespace Taksa.Domain.Bribes
{
	public class Bribe : Aggregate
	{
		private ServiceName name;

		private BribeComment comment;

		private ServiceCategory categoryLocal;

		private ServiceCategory categoryInternational;

		private Address addressLocal;

		private Address addressInternational;

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
					addressLocal = x.AddressLocal;
					timestamp = (BribeTimestamp)x.Timestamp;
					break;

				case Events.V1.BribePublished x:
					categoryInternational = (ServiceCategory) x.CategoryInternational;
					isPublished = true;
					break;
			}
		}

		public static Bribe Create(
			ServiceName name,
			BribeAmount amount,
			Address addressLocal,
			BribeTimestamp timestamp)
		{
			var bribe = new Bribe();
			bribe.Apply(new Events.V1.BribeCreated
			{
				Id = Guid.NewGuid(),
				Name = (string)name,
				Amount = (MoneyRange)amount,
				Timestamp = (DateTimeOffset)timestamp,
				AddressLocal = addressLocal,
				CreatedAt = DateTimeOffset.UtcNow
			});

			return bribe;
		}

		public void Publish(
			ServiceCategory categoryInternational,
			DateTimeOffset publishedAt) =>
			Apply(new Events.V1.BribePublished
			{
				Id = Id,
				CategoryInternational = (string)categoryInternational,
				PublishedAt = publishedAt
			});
	}
}

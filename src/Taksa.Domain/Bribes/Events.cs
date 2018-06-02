using System;

namespace Taksa.Domain.Bribes
{
	public static class Events
	{
		public static class V1
		{
			public class BribeCreated
			{
				public Guid Id { get; set; }
				public string Name { get; set; }
				public MoneyRange Amount { get; set; }
				public DateTimeOffset Timestamp { get; set; }
				public DateTimeOffset CreatedAt { get; set; }

				public override string ToString()
					=> $"Bribe {Id} was created.";
			}
		}
	}
}

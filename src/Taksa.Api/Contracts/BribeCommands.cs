﻿using System;
using Taksa.Domain;

namespace Taksa.Api.Contracts
{
	public static class BribeCommands
	{
		public static class V1
		{
			public class Create
			{
				public DateTimeOffset timestamp { get; set; }

				public string service_name { get; set; }

				public MoneyRange amount { get; set; }

				public AddressDto address_local { get; set; }

				public override string ToString() => $"Creating new bribe";
			}

			public class Publish
			{
				public Guid bribe_id { get; set; }

				//public string category_local { get; set; }
				
				public string category_international { get; set; }
			}
		}
	}
}

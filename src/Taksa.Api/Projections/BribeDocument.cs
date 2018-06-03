using System;
using Taksa.Api.Contracts;

namespace Taksa.Api.Projections
{
	public class BribeDocument
	{
		public Guid id { get; set; }

		public AddressDto address { get; set; }

		public string service_name { get; set; }

		public string service_category { get; set; }
	}
}

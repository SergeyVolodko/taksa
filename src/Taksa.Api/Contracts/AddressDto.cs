using Taksa.Domain;

namespace Taksa.Api.Contracts
{
	public class AddressDto
	{
		public long latitude { get; set; }

		public long longitude { get; set; }

		public string full_address { get; set; }

		public string street { get; }

		public string city { get; }

		public string post_code { get; }

		public string country { get; }

		public static explicit operator Address(AddressDto self)
			=> new Address(
				self.latitude,
				self.longitude,
				self.full_address,
				self.street,
				self.city,
				self.post_code,
				self.country);
	}
}

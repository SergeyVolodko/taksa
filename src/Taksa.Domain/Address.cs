using Taksa.Framework;

namespace Taksa.Domain
{
	public class Address : Value<Address>
	{
		public long Latitude { get; set; }

		public long Longitude { get; set; }

		public string FullAddress { get; set; }

		public string Street { get; }

		public string City { get; }

		public string PostCode { get; }

		public string Province { get; }

		public string Country { get; }

		public Address(
			long latitude,
			long longitude,
			string fullAddress,
			string street,
			string city,
			string postCode,
			string province,
			string country)
		{
			Latitude = latitude;
			Longitude = longitude;
			FullAddress = fullAddress;
			Street = street;
			City = city;
			PostCode = postCode;
			Province = province;
			Country = country;
		}
	}
}

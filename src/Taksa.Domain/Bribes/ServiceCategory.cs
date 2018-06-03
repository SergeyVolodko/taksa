using Taksa.Framework;

namespace Taksa.Domain.Bribes
{
	public class ServiceCategory : Value<ServiceCategory>
	{
		public string Value { get; }

		public ServiceCategory(
			string value)
		{
			Value = value;
		}

		public static explicit operator string(ServiceCategory self) => self.Value;

		public static explicit operator ServiceCategory(string value) => new ServiceCategory(value);
	}
}

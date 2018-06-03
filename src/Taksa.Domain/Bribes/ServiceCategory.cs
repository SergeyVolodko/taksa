using Taksa.Framework;

namespace Taksa.Domain.Bribes
{
	public class ServiceCategory : Value<ServiceCategory>
	{
		public string LocalValue { get; }
		public string InternationalValue { get; }

		public ServiceCategory(
			string localValue,
			string internationalValue)
		{
			LocalValue = localValue;
			InternationalValue = internationalValue;
		}

		//public static explicit operator string(BribeName self) => self.Value;

		//public static explicit operator BribeName(string value) => new BribeName(value);
	}
}

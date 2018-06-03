using Taksa.Framework;

namespace Taksa.Domain.Bribes
{
	public class ServiceName : Value<ServiceName>
	{
		public string Value { get; }

		public ServiceName(string value)
		{
			Value = value;
		}

		public static explicit operator string(ServiceName self) => self.Value;

		public static explicit operator ServiceName(string value) => new ServiceName(value);
	}
}

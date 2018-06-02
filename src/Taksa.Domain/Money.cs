using System;
using System.Collections.Generic;
using System.Linq;
//using Newtonsoft.Json;

namespace Exact.Go.Api.Domain.Core.Models
{
	public class Money : IComparable<Money>, IEquatable<Money>
	{
		private readonly string defaultCurrency = "EUR";
		private string currency;

		///// <summary>
		///// Attribute JsonProperty is needed because the setter is private, else the property will stay empty when deserializing from json.
		///// </summary>
		//[JsonProperty]
		public decimal Amount { get; private set; }

		///// <summary>
		///// Attribute JsonProperty is needed because the setter is private, else the property will stay empty when deserializing from json.
		///// </summary>
		//[JsonProperty]
		public string Currency
		{
			get
			{
				return currency;
			}
			private set
			{
				currency = string.IsNullOrWhiteSpace(value) ? defaultCurrency : value;
			}
		}

		public static Money Zero => new Money(0);

		//[JsonConstructor]
		public Money()
		{
			// Always default currency on deserialization
			currency = defaultCurrency;
		}

		public Money(decimal amount) : this(amount, null) { }

		public Money(decimal amount, string currencyCode)
		{
			Amount = amount;
			Currency = string.IsNullOrWhiteSpace(currencyCode) ? defaultCurrency : currencyCode;
		}

		public static Money operator + (Money moneyX, Money moneyY)
		{
			if (moneyX == null || moneyY == null)
			{
				throw new ArgumentNullException($"One of the input objects is null.");
			}

			if (moneyX.Currency != moneyY.Currency)
			{
				throw new InvalidOperationException("Currency mismatch.");
			}

			return new Money
			{
				Amount = moneyX.Amount + moneyY.Amount,
				Currency = moneyX.Currency
			};
		}

		public static Money operator - (Money moneyX, Money moneyY)
		{
			if (moneyX == null || moneyY == null)
			{
				throw new ArgumentNullException($"One of the input objects is null.");
			}

			if (moneyX.Currency != moneyY.Currency)
			{
				throw new InvalidOperationException("Currency mismatch.");
			}

			return new Money
			{
				Amount = moneyX.Amount - moneyY.Amount,
				Currency = moneyX.Currency
			};
		}
		public static Money operator * (Money money, decimal number)
		{
			if (money == null)
			{
				throw new ArgumentNullException($"Money object is null.");
			}

			return new Money
			{
				Amount = money.Amount * number,
				Currency = money.Currency
			};
		}
		//public Money WithCurrency(string currencyCode)
		//{
		//	Currency = currencyCode;

		//	return this;
		//}

		//public Money WithDefaultCurrency()
		//{
		//	Currency = defaultCurrency;

		//	return this;
		//}

		public Money Round(int decimals)
		{
			Amount = Math.Round(Amount, decimals);

			return this;
		}

		public int CompareTo(Money other)
		{
			if (other == null)
			{
				return -1;
			}

			return this.Amount.CompareTo(other.Amount);
		}

		public bool Equals(Money other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return string.Equals(currency, other.currency, StringComparison.InvariantCulture) && Amount == other.Amount;
		}
	}

	public static class MoneyEnumerable
	{
		public static Money Sum(this IEnumerable<Money> moneyCollection)
		{
			return moneyCollection
				.Aggregate(Money.Zero,
					(moneyX, moneyY) => moneyX + moneyY);
		}
	}
}

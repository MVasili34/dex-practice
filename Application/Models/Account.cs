using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Account
	{
		private Currency currency;
		private decimal amount;

		public Account() { }

		public Account(Currency Currency, decimal Amount)
		{
			this.Currency = Currency;
			this.Amount = Amount;
		}

		public Currency Currency
		{
			get => currency;
			set => currency = value;
		}

		public decimal Amount
		{
			get => amount;
			set => amount = value;
		}
	}
}

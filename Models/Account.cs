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
		public Account(Currency currency, decimal amount)
		{
			Currency = currency;
			Amount = amount;
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

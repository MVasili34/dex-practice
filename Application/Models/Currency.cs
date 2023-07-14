using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public struct Currency
	{
		private string currencyCode = string.Empty;
		private string currencyName = string.Empty;

		public Currency() { }

		public Currency(string CurrencyCode, string CurrencyName)
		{
			this.CurrencyCode = CurrencyCode;
			this.CurrencyName = CurrencyName;
		}

		public string CurrencyCode
		{
			get => currencyCode;
			set => currencyCode = value;
		}
		public string CurrencyName
		{
			get => currencyName;
			set => currencyName = value;
		}
	}
}

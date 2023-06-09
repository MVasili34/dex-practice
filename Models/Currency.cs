using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public struct Currency
	{
		private string currencyCode = String.Empty;
		private string name = String.Empty;

		public Currency() { }

		public Currency(string code, string call)
		{
			CurrencyCode = code;
			Name = call;
		}

		public string CurrencyCode
		{
			get => currencyCode;
			set => currencyCode = value;
		}
		public string Name
		{
			get => name;
			set => name = value;
		}
	}
}

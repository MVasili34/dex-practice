
namespace Models;

public struct Currency
{
	private string _currencyCode = string.Empty;
	private string _currencyName = string.Empty;

	public Currency() { }

	public Currency(string currencyCode, string currencyName)
	{
		CurrencyCode = currencyCode;
		CurrencyName = currencyName;
	}

	public string CurrencyCode
	{
		get => _currencyCode;
		set => _currencyCode = value;
	}
	public string CurrencyName
	{
		get => _currencyName;
		set => _currencyName = value;
	}
}

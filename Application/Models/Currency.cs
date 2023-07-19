
namespace Models;

public struct Currency
{
	private string _currencyCode = string.Empty;
	private string _currencyName = string.Empty;

	public Currency() { }

	public Currency(string CurrencyCode, string CurrencyName)
	{
		this.CurrencyCode = CurrencyCode;
		this.CurrencyName = CurrencyName;
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

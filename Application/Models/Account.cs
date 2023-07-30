namespace Models;

public class Account
{
	private Currency _currency;
	private decimal _amount;

	public Account() { }

	public Account(Currency currency, decimal amount)
	{
		Currency = currency;
		Amount = amount;
	}

	public Currency Currency
	{
		get => _currency;
		set => _currency = value;
	}

	public decimal Amount
	{
		get => _amount;
		set => _amount = value;
	}
}

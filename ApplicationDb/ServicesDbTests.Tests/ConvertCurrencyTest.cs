using Xunit.Abstractions;
using ServicesDb;

namespace ServicesDbTests.Tests;

public class ConvertCurrencyTest
{
	private readonly ITestOutputHelper _output;

	public ConvertCurrencyTest(ITestOutputHelper _output)
	{
		this._output = _output;
	}

	[Fact]
	public async Task GetExschangeRateTest()
	{
		ConvertCurrency convert = new() { From = "EUR", To = "USD" };
		CurrencyService service = new("AmdorenKeyHere");

		AmdorenResponse response = await service.Convert(convert);

		_output.WriteLine($"Error: {response.Error}-{response.ErrorMessage}, Amount: {response.Amount}");
	}

	[Fact]
	public async Task ConvertAmountTest()
	{
		ConvertCurrency convert = new() { From = "USD", To = "RUB", Amount = 10 };
		CurrencyService service = new("AmdorenKeyHere");

		AmdorenResponse response = await service.Convert(convert);

		_output.WriteLine($"Error: {response.Error}-{response.ErrorMessage}, Amount: {response.Amount}");
	}

	[Fact]
	public async Task ApiKeyFailureTest()
	{
		ConvertCurrency convert = new() { From = "USD", To = "RUB", Amount = 10 };
		CurrencyService service = new("12345");

		AmdorenResponse response = await service.Convert(convert);

		Assert.Equal(110, response.Error);
	}

	[Fact]
	public async Task WrongAmountTest()
	{
		ConvertCurrency convert = new() { From = "USD", To = "RUB", Amount = (decimal)-10.5 };
		CurrencyService service = new("AmdorenKeyHere");

		AmdorenResponse response = await service.Convert(convert);

		Assert.Equal(300, response.Error);
	}
}

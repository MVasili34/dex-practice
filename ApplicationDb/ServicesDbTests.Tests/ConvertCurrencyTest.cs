using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ServicesDb;

namespace ServicesDbTests.Tests;

public class ConvertCurrencyTest
{
	private readonly ITestOutputHelper Output;

	public ConvertCurrencyTest(ITestOutputHelper Output)
	{
		this.Output = Output;
	}

	[Fact] //тестирование сервиса, не передавая сумму (выводится курс для 1 доллара)
	public async Task GetExschangeRateTest()
	{
		ConvertCurrency convert = new() { From = "EUR", To = "USD" };
		CurrencyService service = new("L4tz65xgEvSuCcSPnYjzjBzhU4EAWd");
		AmdorenResponse response = await service.Convert(convert);
		Output.WriteLine($"Error: {response.Error}-{response.Error_Message}, Amount: {response.Amount}");
	}

	[Fact] //тестирование сервиса, передавая сумму
	public async Task ConvertAmountTest()
	{
		ConvertCurrency convert = new() { From = "USD", To = "RUB", Amount = 10 };
		CurrencyService service = new("L4tz65xgEvSuCcSPnYjzjBzhU4EAWd");
		AmdorenResponse response = await service.Convert(convert);
		Output.WriteLine($"Error: {response.Error}-{response.Error_Message}, Amount: {response.Amount}");
	}

	[Fact] //тестирование серевиса, передавая неверный API ключ
	public async Task ApiKeyFailureTest()
	{
		ConvertCurrency convert = new() { From = "USD", To = "RUB", Amount = 10 };
		CurrencyService service = new("12345");
		AmdorenResponse response = await service.Convert(convert);
		Assert.Equal(110, response.Error);
	}

	[Fact] //тестирование курса, передавая неверное значение суммы
	public async Task WrongAmountTest()
	{
		ConvertCurrency convert = new() { From = "USD", To = "RUB", Amount = (decimal)-10.5 };
		CurrencyService service = new("L4tz65xgEvSuCcSPnYjzjBzhU4EAWd");
		AmdorenResponse response = await service.Convert(convert);
		Assert.Equal(300, response.Error);
	}
}

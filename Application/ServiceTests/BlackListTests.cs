using Services;
using static Services.TestDataGenerator;

namespace ServiceTests.Tests;

public class BlackListTests
{
	private readonly BankService _bankService = new();

	[Fact]
	public void BonusEmployeeTest()
	{
		Employee employee = GenerateEmployees(1).First();
		int expected = employee.Salary + 1000;

		_bankService.AddBonus(employee);

		Assert.Equal(expected, employee.Salary);
	}

	[Fact]
	public void BonusNullTest()
	{
		Client client = null!;

		Assert.Throws<ArgumentNullException>(() =>  _bankService.AddBonus(client));
	}

	[Fact]
	public void BonusClientTest()
	{
		Client client = GenerateClients(1).First();

		_bankService.AddBonus(client);

		Assert.Contains("BONUS", client.AddressInfo);
    }

	[Fact]
	public void CheckBlackListEmployeeTest()
	{
	    Employee employee = GenerateEmployees(1).First();

		_bankService.AddToBlackList(employee);
		bool? status = _bankService.IsPersonInBlackList(employee);

		Assert.True(status);
	}

	[Fact]
	public void CheckBlackListClientTest()
	{
		Client client = GenerateClients(1).First();

		_bankService.AddToBlackList(client);
		bool? status = _bankService.IsPersonInBlackList(client);

		Assert.True(status);
	}

    [Fact]
	public void CheckBlackListNullTest()
	{
		Client client = null!;

		Assert.Throws<ArgumentNullException>(() => _bankService.IsPersonInBlackList(client));
    }
}

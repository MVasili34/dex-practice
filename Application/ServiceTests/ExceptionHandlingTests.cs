using Services.Storage;
using Services.Exceptions;
using static Services.TestDataGenerator;

namespace ServiceTests.Tests;

public class ExceptionHandlingTests
{
    private readonly IClientStorage _clientService;
    private readonly IEmployeeStorage _employeeService;

	public ExceptionHandlingTests(IClientStorage clientStorage, IEmployeeStorage employeeStorage)
	{
		_clientService = clientStorage;
        _employeeService = employeeStorage;
    }

    [Fact]
	public void ClientBelow18Test()
	{
		Client client = GenerateClients(1).First();

		client.DateOfBirth = new DateOnly(2010, 11, 24);

		Assert.Throws<Below18Exception>(() => _clientService.Add(client));
	}

	[Fact]
	public void ClientNoPassportTest()
	{
		Client client = GenerateClients(1).First();

		client.Passport = string.Empty;

		Assert.Throws<PassportNullException>(() => _clientService.Add(client));
	}

	[Fact]
	public void ClientDefaultAccountExistsTest()
	{
		Client client = GenerateClients(1).First();

		_clientService.Add(client);
		List<Account> accounts = _clientService.Data[client];

        Assert.Single(accounts);
	}

	[Fact]
	public void ClientAddingAccountTest()
	{
		Client client = GenerateClients(1).First();

		_clientService.Add(client);
		_clientService.AddAccount(client, new(new Currency("BS212", "Usd."), 10));
		int amount = _clientService.Data[client].Count;

        Assert.Equal(2, amount);
	}

	[Fact]
	public void ClientAddingAccountExceptionTest()
	{
		Client client = GenerateClients(1).First();

		_clientService.Add(client);
		Account account = new(new Currency("BS212", ""), 10);

		Assert.Throws<IncorrectAccountException>(() => _clientService.AddAccount(client, account));
	}

	[Fact]
	public void ClientRemoveExceptionTest()
	{
		Client client = GenerateClients(1).First();

		Assert.Throws<FailedToRemoveException>(() => _clientService.Delete(client));
	}

	[Fact]
	public void EmployeeBelow18Test()
	{
		Employee employee = GenerateEmployees(1).First();

		employee.DateOfBirth = new DateOnly(2020, 10, 11);

		Assert.Throws<Below18Exception>(() => _employeeService.AddEmployee(employee));
	}

	[Fact]
	public void EmployeeNoPassportTest()
	{
		Employee employee = GenerateEmployees(1).First();

		employee.Passport = string.Empty;

		Assert.Throws<NullContractException>(() => _employeeService.AddEmployee(employee));
	}

	[Fact]
	public void EmployeeRemoveExceptionTest()
	{
		Employee employee = GenerateEmployees(1).First();

		Assert.Throws<FailedToRemoveException>(() => _employeeService.DeleteEmployee(employee));
	}

	[Fact]
	public void EmployeeEditExceptionTest()
	{
		Employee employee = GenerateEmployees(1).First();
		employee.Contract = "Something";
		_employeeService.AddEmployee(employee);

		employee = _employeeService.Data.First();
		employee.Contract = "";

		Assert.Throws<IncorrectEmployeeException>(() => _employeeService.UpdateEmployee(0, employee));
	}
}

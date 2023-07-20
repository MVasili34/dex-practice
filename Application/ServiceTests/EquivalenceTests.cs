using Models;
using Services;

namespace ServiceTests.Tests;

public class EquivalenceTests
{

	[Fact]
	public void GetHashCodeNecessityPositivClientTest()
	{
		Dictionary<Client, List<Account>> accounts = TestDataGenerator.GenerateClientsWithAccounts()
			.ToDictionary(pair => pair.Key, pair => pair.Value);
		Client client = accounts.Keys.First();

		Client clientCopy = new(client.FirstName, client.LastName,
			client.DateOfBirth, client.Phone, client.Passport,
			client.Company, client.AddressInfo);
		
		Assert.Equal(accounts[client], accounts[clientCopy]);
	}
	
	[Fact]
	public void GetHashCodeNecessityPositivEmployeeTest()
	{
		List<Employee> employees = TestDataGenerator.GenerateEmployees(100).ToList();

		Employee employeeCopy = new(employees[65].FirstName, employees[65].LastName,
			employees[65].DateOfBirth, employees[65].Phone, employees[65].Passport,
			employees[65].Position, employees[65].Salary, employees[65].Contract);

		//метод IndexOf также использует метод Equals для сравнения объектов
		Assert.Equal(65, employees.IndexOf(employeeCopy));
	}
}

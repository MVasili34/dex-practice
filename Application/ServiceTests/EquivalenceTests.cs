using Models;
using Services;
using static System.Console;

namespace ServiceTests.Tests
{
	public class EquivalenceTests
	{

		[Fact]
		public static void GetHashCodeNecessityPositivTest()
		{
			Dictionary<Client, List<Account>>?  accounts = TestDataGenerator.GenerateClientsWithAccounts()
				.ToDictionary(pair => pair.Key, pair => pair.Value);

			Client testclient = accounts.Keys.First();

			Client someClient = new(testclient.FirstName, testclient.LastName,
				testclient.DateOfBirth, testclient.Phone, testclient.Passport,
				testclient.Company, testclient.AddressInfo);
			
			Assert.Equal(accounts[testclient], accounts[someClient]);
		}
		
		[Fact]
		public static void GetHashCodeNecessityPositivTestEmployees()
		{
			List<Employee> Employees = TestDataGenerator.GenerateEmployees(100).ToList();
			Employee someEmployee = new(Employees[65].FirstName, Employees[65].LastName,
				Employees[65].DateOfBirth, Employees[65].Phone, Employees[65].Passport,
				Employees[65].Position, Employees[65].Salary, Employees[65].Contract);

			//метод IndexOf также использует метод Equals для сравнения объектов
			Assert.Equal(65, Employees.IndexOf(someEmployee));
		}
	}
}

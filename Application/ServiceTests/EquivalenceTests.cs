using Models;
using Services;
using static System.Console;

namespace ServiceTests
{
	public class EquivalenceTests
	{

		[Fact]
		public static void GetHashCodeNecessityPositivTest()
		{
			Dictionary<Client, List<Account>>?  accounts = TestDataGenerator.GenerateAccounts()
				.ToDictionary(pair => pair.Key, pair => pair.Value);

			//создание нового объекта клиента
			Client someClient = new(accounts.Keys.First().FirstName, accounts.Keys.First().LastName,
				accounts.Keys.First().DateOfBirth, accounts.Keys.First().Phone, accounts.Keys.First().Passport,
				accounts.Keys.First().Company, accounts.Keys.First().AdressInfo);

			Assert.Equal(accounts[accounts.Keys.First()], accounts[someClient]);
		}
		
		[Fact]
		public static void GetHashCodeNecessityPositivTestEmployees()
		{
			List<Employee>? Employees = TestDataGenerator.GenerateEmployees(100).ToList();
			//создание нового объекта клиента
			if (Employees is not null)
			{
				Employee someEmployee = new(Employees[65].FirstName, Employees[65].LastName,
					Employees[65].DateOfBirth, Employees[65].Phone, Employees[65].Passport,
					Employees[65].Position, Employees[65].Salary, Employees[65].Contract);

				//метод Contains также использует методы Equals и GetHashCode для проведения сравнения объектов
				Assert.Equal(65, Employees.IndexOf(someEmployee));
			}
		}
		
	}
}

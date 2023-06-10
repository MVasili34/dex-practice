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

			//�������� ������ ������� �������
			Client someClient = new(accounts.Keys.First().FirstName, accounts.Keys.First().LastName,
				accounts.Keys.First().DateOfBirth, accounts.Keys.First().Phone, accounts.Keys.First().Passport,
				accounts.Keys.First().Company, accounts.Keys.First().AdressInfo);

			Assert.Equal(accounts[accounts.Keys.First()], accounts[someClient]);
		}
		
		[Fact]
		public static void GetHashCodeNecessityPositivTestEmployees()
		{
			List<Employee>? Employees = TestDataGenerator.GenerateEmployees(100).ToList();
			//�������� ������ ������� �������
			if (Employees is not null)
			{
				Employee someEmployee = new(Employees[65].FirstName, Employees[65].LastName,
					Employees[65].DateOfBirth, Employees[65].Phone, Employees[65].Passport,
					Employees[65].Position, Employees[65].Salary, Employees[65].Contract);

				//����� Contains ����� ���������� ������ Equals � GetHashCode ��� ���������� ��������� ��������
				Assert.Equal(65, Employees.IndexOf(someEmployee));
			}
		}
		
	}
}

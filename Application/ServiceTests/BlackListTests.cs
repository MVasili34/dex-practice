using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Services.TestDataGenerator;

namespace ServiceTests.Tests
{
	public class BlackListTests
	{
		private static List<Client> clients = GenerateClints(5).ToList();
		private static List<Employee> employees = GenerateEmployees(5).ToList();

		[Fact]
		public static void BonusEmployeeTest()
		{
			BankService bankService = new();
			int expected = employees[0].Salary+1000;
			bankService.AddBonus(employees[0]);
			Assert.Equal(expected, employees[0].Salary);
		}

		[Fact]
		public static void BonusClientTest()
		{
			BankService bankService = new();
			bankService.AddBonus(clients[0]);
			Assert.Contains("BONUS", clients[0].AdressInfo);
		}

		[Fact]
		public static void BlackListEmployeeTest()
		{
			BankService bankService = new();
			bankService.AddToBlackList(employees[0]);
			Assert.True(bankService.IsPersonInBlackList(employees[0]));
		}
		[Fact]
		public static void BlackListClientTest()
		{
			BankService bankService = new();
			bankService.AddToBlackList(clients[0]);
			bankService.AddToBlackList(clients[1]);
			Assert.True(bankService.IsPersonInBlackList(clients[0]) is true && 
				bankService.IsPersonInBlackList(clients[1]) is true);
		}
	}
}

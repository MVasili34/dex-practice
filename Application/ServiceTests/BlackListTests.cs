using Models;
using Services;
using static Services.TestDataGenerator;

namespace ServiceTests.Tests
{
	public class BlackListTests
	{
		private static List<Client> clients = GenerateClients(5).ToList();
		private static List<Employee> employees = GenerateEmployees(5).ToList();
		private static BankService bankService = new();

        [Fact]
		public static void BonusEmployeeTest()
		{
			int expected = employees[0].Salary+1000;

			bankService.AddBonus(employees[0]);

			Assert.Equal(expected, employees[0].Salary);
		}

        [Fact]
		public static void BonusNullTest()
		{
			clients[4] = null!;

			Assert.Throws<ArgumentNullException>(() =>  bankService.AddBonus(clients[4]));
		}

        [Fact]
        public static void BonusClientTest()
        {
            bankService.AddBonus(clients[0]);

            Assert.Contains("BONUS", clients[0].AddressInfo);
        }

        [Fact]
		public static void CheckBlackListEmployeeTest()
		{
			bankService.AddToBlackList(employees[0]);

			bool? status = bankService.IsPersonInBlackList(employees[0]);

			Assert.True(status);
		}

		[Fact]
		public static void CheckBlackListClientTest()
		{
			bankService.AddToBlackList(clients[1]);

			bool? status = bankService.IsPersonInBlackList(clients[1]);

			Assert.True(status);
		}

        [Fact]
        public static void CheckBlackListNullTest()
        {
            clients[4] = null!;

            Assert.Throws<ArgumentNullException>(() => bankService.IsPersonInBlackList(clients[4]));
        }
    }
}

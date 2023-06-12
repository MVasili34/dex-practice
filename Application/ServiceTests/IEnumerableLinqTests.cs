using Models;
using Services;
using Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTests.Tests
{
	public class IEnumerableLinqTests
	{
		[Fact]
		public static void ClientFilterCheck()
		{
			ClientService service = new(clients);
			var response = service.FilterMethod("И", "А", "3", "3", new(1990, 01, 01), new(2010, 01, 01))
				.ToDictionary(pair => pair.Key, pair => pair.Value);
			Assert.True(response.ContainsKey(new Client("Имя", "Фамилия", DateOnly.Parse("21.01.2001"), "434413244", "AB234", "NONE", "Адрес"))
				&& response.Count() == 1);
		}
		[Fact]
		public static void EmployeeFilterCheck()
		{
			EmployeeService service = new(employees);
			var response = service.FilterMethod("И", "А", "3", "3", new(1990, 01, 01), new(2010, 01, 01)).ToList();
			Assert.True(response.Contains(employees[0]) && response.Count() == 1);
		}

		[Fact]
		public static void ClientOldestCheck()
		{
			ClientService service = new(clients);
			var response = service.GetOldestClients().ToDictionary(pair => pair.Key, pair => pair.Value);
			Assert.True(response.ContainsKey(new Client("Света", "Петровна", DateOnly.Parse("19.01.1967"), "32493294", "AB423", "NONE", "Адрес"))
				&& response.Count() == 1);
		}

		[Fact]
		public static void EmployeeOldestCheck()
		{
			EmployeeService service = new(employees);
			var response = service.GetOldestEmployees().ToList();
			Assert.True(response.Contains(employees[1]) && response.Count() == 1);
		}

		[Fact]
		public static void ClientYoungestCheck()
		{
			ClientService service = new(clients);
			var response = service.GetYoungestClients().ToDictionary(pair => pair.Key, pair => pair.Value);
			Assert.True(response.ContainsKey(new Client("Имя", "Фамилия", DateOnly.Parse("21.01.2001"), "434413244", "AB234", "NONE", "Адрес"))
				&& response.Count() == 1);
		}

		[Fact]
		public static void EmployeeYoungestCheck()
		{
			EmployeeService service = new(employees);
			var response = service.GetYoungestEmployees().ToList();
			Assert.True(response.Contains(employees[0]) && response.Count() == 1);
		}

		[Fact]
		public static void ClientAvarageAgeCheck()
		{
			ClientService service = new(clients);
			Assert.Equal(34, service.GetAvarageAge());
		}

		[Fact]
		public static void EmployeeAvarageAgeCheck()
		{
			EmployeeService service = new(employees);
			Assert.Equal(34, service.GetAvarageAge());
		}

		[Fact]
		public static void AddingClientToStorage()
		{
			ClientStorage clientStorage = new();
			Client newClient = new("FName", "LName", new(2001, 02, 21),
				"123456", "AB1234", "NONE", "Adress");
			clientStorage.AddClient(newClient);
			Assert.True(clientStorage.clients.ContainsKey(newClient));
		}

		[Fact]
		public static void AddingEmployeeToStorage()
		{
			EmployeeStorage employeeStorage = new();
			Employee newEmployee = new("FName", "LName", new(2001, 02, 21),
				"123456", "AB1234", "Уборщик", 10000);
			employeeStorage.AddEmployee(newEmployee);
			Assert.True(employeeStorage.employees?.Contains(newEmployee));
		}
		private static Dictionary<Client, List<Account>> clients = new()
		{
			{ new Client("Имя", "Фамилия", DateOnly.Parse("21.01.2001"), "434413244", "AB234", "NONE", "Адрес"), new List<Account>()
			{ new Account(new Currency("RUS", "Рубль"), 20000)} },
			{ new Client("Света", "Петровна", DateOnly.Parse("19.01.1967"), "32493294", "AB423", "NONE", "Адрес"), new List<Account>()
			{ new Account(new Currency("RUS", "Рубль"), 10000)} },
			{ new Client("Иннокентий", "Петров", DateOnly.Parse("12.01.1996"), "634643", "AB254", "4234", "Адрес"), new List<Account>()
			{ new Account(new Currency("RUS", "Рубль"), 10000)} },
		};
		private static List<Employee> employees = new() 
		{
			new Employee("Имя", "Фамилия", DateOnly.Parse("21.01.2001"), "434413244", "AB234", "NONE", 20000),
			new Employee("Света", "Петровна", DateOnly.Parse("19.01.1967"), "32493294", "AB423", "NONE", 21331),
			new Employee("Иннокентий", "Петров", DateOnly.Parse("12.01.1996"), "634643", "AB254", "4234", 34224)
		};
	}
}

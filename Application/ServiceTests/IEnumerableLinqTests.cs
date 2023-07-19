using Models;
using Services;
using Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Services.TestDataGenerator;

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

            Assert.True(response.ContainsKey(clients.Keys.First()) && response.Count() == 1);
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

            Assert.True(response.ContainsKey(new Client("Света", "Петровна", new DateOnly(1967, 3, 19), 
                "32493294", "AB423", "NONE", "Адрес")) && response.Count() == 1);
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

            Assert.True(response.ContainsKey(clients.Keys.First()) && response.Count() == 1);
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
            Client client = GenerateClients(1).First();

            clientStorage.AddClient(client);

            Assert.True(clientStorage.Clients.ContainsKey(client));
        }

        [Fact]
        public static void AddingEmployeeToStorage()
        {
            EmployeeStorage employeeStorage = new();
            Employee employee = GenerateEmployees(1).First();

            employeeStorage.AddEmployee(employee);

            Assert.True(employeeStorage.Employees?.Contains(employee));
        }
        private static Dictionary<Client, List<Account>> clients = new()
        {
            { new Client("Имя", "Фамилия", new DateOnly(2001, 1, 21), "434413244", "AB234", "NONE", "Адрес"), 
                new List<Account>()
                    { new Account(new Currency("RUB", "Рубль"), 20000)} },
            { new Client("Света", "Петровна", new DateOnly(1967, 3, 19), "32493294", "AB423", "NONE", "Адрес"), 
                new List<Account>()
                    { new Account(new Currency("RUB", "Рубль"), 10000)} },
            { new Client("Иннокентий", "Петров", new DateOnly(1996, 1, 12), "634643", "AB254", "4234", "Адрес"), 
                new List<Account>()
                    { new Account(new Currency("RUB", "Рубль"), 10000)} },
        };
        private static List<Employee> employees = new()
        {
            new Employee("Имя", "Фамилия", new DateOnly(2001, 1, 21), "434413244", "AB234", "NONE", 20000),
            new Employee("Света", "Петровна", new DateOnly(1967, 3, 19), "32493294", "AB423", "NONE", 21331),
            new Employee("Иннокентий", "Петров", new DateOnly(1996, 1, 12), "634643", "AB254", "4234", 34224)
        };
    }
}

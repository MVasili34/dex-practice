using Models;
using Services;
using Services.Storage;

namespace PracticeWithIEnumerableLinq
{
	internal class Program
	{
		static void Main(string[] args)
		{
			TestWintClients();
			Console.WriteLine("Для проложения и перехода к тесту с сотрудниками нажмите любую кнопку...");
			Console.ReadKey();
			TestWithEmployees();
			Console.ReadKey();
		}

		static void TestWintClients()
		{
			ClientStorage storage = new();
			storage.AddClient(new Client("Имя", "Фамилия", DateOnly.Parse("21.01.2001"), "434413244", "AB234", "NONE", "Адрес"));
			ClientService service = new(storage.clients);
			Dictionary<Client, List<Account>> filteredclients = service.FilterMethod("И", "А", "1", "B",
				DateOnly.Parse("2.01.1990"), DateOnly.Parse("2.01.2010")).ToDictionary(pair => pair.Key, pair => pair.Value); ;
			Console.WriteLine("Список клиентов до фильтрации:");
			service.PrintOut();
			Console.WriteLine("\nСписок клиентов после фильтрации:");
			service.PrintOut(filteredclients);

			filteredclients = service.GetOldestClients().ToDictionary(pair => pair.Key, pair => pair.Value); ;
			Console.WriteLine("\nСписок старейших клиентов:");
			service.PrintOut(filteredclients);

			filteredclients = service.GetYoungestClients().ToDictionary(pair => pair.Key, pair => pair.Value); ;
			Console.WriteLine("\nСписок самых молодых клиентов:");
			service.PrintOut(filteredclients);

			Console.WriteLine($"\nСредний возраст клиентов: {service.GetAvarageAge()}");
		}

		static void TestWithEmployees()
		{
			EmployeeStorage storage = new();
			storage.AddEmployee(new Employee("Имя", "Фамилия", DateOnly.Parse("21.01.2001"), "434413244", "AB3413", "Какая-то", 2012));
			EmployeeService service = new(storage.employees);
			List<Employee> filteredclients = service.FilterMethod("и", "а", "4", "B",
				DateOnly.Parse("2.01.1990"), DateOnly.Parse("2.01.2010")).ToList();
			Console.WriteLine("Список сотрудников до фильтрации:");
			service.PrintOut();
			Console.WriteLine("\nСписок сотрудников после фильтрации:");
			service.PrintOut(filteredclients);

			filteredclients = service.GetOldestEmployees().ToList();
			Console.WriteLine("\nСписок старейших сотрудников:");
			service.PrintOut(filteredclients);

			filteredclients = service.GetYoungestEmployees().ToList();
			Console.WriteLine("\nСписок самых молодых сотрудников:");
			service.PrintOut(filteredclients);

			Console.WriteLine($"\nСредний возраст сотрудников: {service.GetAvarageAge()}");
		}
	}
}
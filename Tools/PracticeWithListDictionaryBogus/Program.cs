using Models;
using System.Diagnostics;
using static Services.TestDataGenerator;

namespace PracticeWithListDictionaryBogus;

internal class Program
{
	private static Stopwatch _timer = null!;
	static void Main(string[] args)
	{
        List<Client> clients = GenerateClients(1000).ToList();
        Dictionary<string, Client> dictionary = GenerateClientsDictionary(clients!)
			.ToDictionary(pair => pair.Key, pair=>pair.Value);
        List<Employee> employees = GenerateEmployees(1000).ToList();

		Console.WriteLine("Поиск клиента по телефону с применением StopWatch:");
        SearchClientByPhoneInList(clients, clients[400].Phone);

		Console.WriteLine("Поиск клиента по телефону с применением StopWatch в словаре:");
		SearchClientByPhoneInDictionary(dictionary, clients[400].Phone);

		Console.WriteLine($"Выборка клиентов, возраст которых ниже определенного значения (меньше 25 лет):");
		GetClientsBelowAge(clients);

		Console.WriteLine($"Поиск сотрудника с минимальной заработной платой (минимальная ЗП - 1000):");
		SearchEmployeeMinSalary(employees);

		Console.WriteLine("Cравнение скорости поиска по словарю двумя методами");
		SearchInDictionarySpeed(dictionary, clients.Last().Phone);

		Console.ReadKey();
	}

	static void SearchClientByPhoneInList(IEnumerable<Client> clients, string phone)
	{
		_timer = Stopwatch.StartNew();
		bool status = clients.Any(c => c.Phone == phone);
        Console.WriteLine(status);
		Console.WriteLine($"Прошло тиков {_timer.ElapsedTicks} \n");
	}

	static void SearchClientByPhoneInDictionary(IDictionary<string, Client> clients, string phone)
	{
        _timer = Stopwatch.StartNew();
		bool status = clients.Any(c => c.Key == phone);
        Console.WriteLine(status);
		Console.WriteLine($"Прошло тиков {_timer.ElapsedTicks} \n");
	}

	static void GetClientsBelowAge(IEnumerable<Client> clients)
	{
        _timer = Stopwatch.StartNew();
		int found = clients.Where(c => (DateTime.Now.Year - c.DateOfBirth.Year < 25)).Count();
		Console.WriteLine($"Найдено {found}. Прошло тиков {_timer.ElapsedTicks}\n");
	}

	static void SearchEmployeeMinSalary(IEnumerable<Employee> employees)
	{
        _timer = Stopwatch.StartNew();
		int found = employees.Where(p => p.Salary == employees.Min(employee => employee.Salary)).Count();
		Console.WriteLine($"Всего таких сотрудников: {found}. Прошло тиков  {_timer.ElapsedTicks} \n");
	}

	static void SearchInDictionarySpeed(IDictionary<string, Client> clients, string phone)
	{
        _timer = Stopwatch.StartNew();
		clients.Reverse().FirstOrDefault();
		long elapsedTime1 = _timer.ElapsedTicks;

        _timer = Stopwatch.StartNew();
        Console.WriteLine(clients.ContainsKey(phone));
		long elapsedTime2 = _timer.ElapsedTicks;

		if (elapsedTime1 > elapsedTime2)
			Console.WriteLine("Поиск ByKeyValue быстрее; ");
		else
			Console.WriteLine("Поиск FirstOrDefault быстрее; ");
		Console.WriteLine($"FirstOrDefault: {elapsedTime1} тиков; ByKeyValue: {elapsedTime2} тиков;");
	}
}
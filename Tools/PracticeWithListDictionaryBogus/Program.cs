using Models;
using System.Diagnostics;
using static Services.TestDataGenerator;

namespace PracticeWithListDictionaryBogus;

internal class Program
{
	private static List<Client>? _clients;
	private static Dictionary<string, Client>? _dictionary;
	private static List<Employee>? _employees;
	private static Stopwatch? _timer;
	static void Main(string[] args)
	{
		_clients = GenerateClients(1000).ToList();
		_dictionary = GenerateClientsDictionary(_clients!).ToDictionary(pair => pair.Key, pair=>pair.Value);
		_employees = GenerateEmployees(1000).ToList();

		Console.WriteLine("Поиск клиента по телефону с применением StopWatch:");
        SearchClientByPhoneInList(_clients);

		Console.WriteLine("Поиск клиента по телефону с применением StopWatch в словаре:");
		SearchClientByPhoneInDictionary(_dictionary, _clients[400].Phone);

		Console.WriteLine($"Выборка клиентов, возраст которых ниже определенного значения (меньше 25 лет):");
		GetClientsBelowAge(_clients);

		Console.WriteLine($"Поиск сотрудника с минимальной заработной платой (минимальная ЗП - 1000):");
		SearchEmployeeMinSalary(_employees);

		Console.WriteLine("Cравнение скорости поиска по словарю двумя методами");
		SearchInDictionarySpeed(_dictionary, _clients.Last().Phone);

		Console.ReadKey();
	}

	static void SearchClientByPhoneInList(List<Client>? clients)
	{
		_timer = Stopwatch.StartNew();
		Console.WriteLine(clients?.Any(c => c.Phone == clients[400].Phone));
		Console.WriteLine($"Прошло тиков {_timer.ElapsedTicks} \n");
	}

	static void SearchClientByPhoneInDictionary(Dictionary<string, Client>? clients, string? phone)
	{
        _timer = Stopwatch.StartNew();
        Console.WriteLine(clients?.Any(c => c.Key == phone));
		Console.WriteLine($"Прошло тиков {_timer.ElapsedTicks} \n");
	}

	static void GetClientsBelowAge(List<Client>? clients)
	{
        _timer = Stopwatch.StartNew();
		var needed = clients?.Where(c => (DateTime.Now.Year - c.DateOfBirth.Year < 25)).ToList();
		Console.WriteLine($"Найдено {needed?.Count()}. Прошло тиков {_timer.ElapsedTicks}\n");
	}

	static void SearchEmployeeMinSalary(List<Employee>? employees)
	{
        _timer = Stopwatch.StartNew();
		var needed = employees?.Where(p => p.Salary == employees.Min(employee => employee.Salary))
			.ToList();
		Console.WriteLine($"Всего таких сотрудников: {needed?.Count()}. Прошло тиков  {_timer.ElapsedTicks} \n");
	}

	static void SearchInDictionarySpeed(Dictionary<string, Client> clients, string phone)
	{
        _timer = Stopwatch.StartNew();
		clients?.Reverse().FirstOrDefault();
		long elapsedTime3 = _timer.ElapsedTicks;

        _timer = Stopwatch.StartNew();
        Console.WriteLine(clients?.ContainsKey(phone));
		long elapsedTime4 = _timer.ElapsedTicks;

		if (elapsedTime3 > elapsedTime4)
			Console.WriteLine("Поиск ByKeyValue быстрее; ");
		else
			Console.WriteLine("Поиск FirstOrDefault быстрее; ");
		Console.WriteLine($"FirstOrDefault: {elapsedTime3} тиков; ByKeyValue: {elapsedTime4} тиков;");
	}
}
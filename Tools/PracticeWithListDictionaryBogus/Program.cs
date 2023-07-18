using Models;
using System.Diagnostics;
using static Services.TestDataGenerator;

namespace PracticeWithListDictionaryBogus;

internal class Program
{
	static List<Client>? clients;
	static Dictionary<string, Client>? dictionary;
	static List<Employee>? employees;
	static Stopwatch? timer;
	static void Main(string[] args)
	{
		clients = GenerateClients(1000).ToList();
		dictionary = GenerateClientsDictionary(clients!).ToDictionary(pair => pair.Key, pair=>pair.Value);
		employees = GenerateEmployees(1000).ToList();

		Console.WriteLine("Поиск клиента по телефону с применением StopWatch:");
        SearchClientByPhoneInList(clients);

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

	static void SearchClientByPhoneInList(List<Client>? clients)
	{
		timer = Stopwatch.StartNew();
		Console.WriteLine(clients?.Any(phone => phone.Phone == clients[400].Phone));
		Console.WriteLine($"Прошло тиков {timer.ElapsedTicks} \n");
	}

	static void SearchClientByPhoneInDictionary(Dictionary<string, Client>? clients, string? phone)
	{
        timer = Stopwatch.StartNew();
        Console.WriteLine(clients?.Any(t => t.Key == phone));
		Console.WriteLine($"Прошло тиков {timer.ElapsedTicks} \n");
	}

	static void GetClientsBelowAge(List<Client>? clients)
	{
        timer = Stopwatch.StartNew();
		var needed = clients?.Where(d => (DateTime.Now.Year - d.DateOfBirth.Year < 25)).ToList();
		Console.WriteLine($"Найдено {needed?.Count()}. Прошло тиков {timer.ElapsedTicks}\n"); ;
	}

	static void SearchEmployeeMinSalary(List<Employee>? employees)
	{
        timer = Stopwatch.StartNew();
		var needed = employees?.Where(d => d.Salary == employees.Min(e => e.Salary)).ToList();
		Console.WriteLine($"Всего таких сотрудников: {needed?.Count()}. Прошло тиков  {timer.ElapsedTicks} \n"); ;
	}

	static void SearchInDictionarySpeed(Dictionary<string, Client> clients, string phone)
	{
        timer = Stopwatch.StartNew();
		clients?.Reverse().FirstOrDefault();
		long elapsedTime3 = timer.ElapsedTicks;

        timer = Stopwatch.StartNew();
        Console.WriteLine(clients?.ContainsKey(phone));
		long elapsedTime4 = timer.ElapsedTicks;
		if (elapsedTime3 > elapsedTime4)
			Console.WriteLine("Поиск ByKeyValue быстрее; ");
		else
			Console.WriteLine("Поиск FirstOrDefault быстрее; ");
		Console.WriteLine($"FirstOrDefault: {elapsedTime3} тиков; ByKeyValue: {elapsedTime4} тиков;");
	}
}
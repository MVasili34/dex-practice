using Models;
using System.Diagnostics;
using static Services.TestDataGenerator;

namespace PracticeWithListDictionaryBogus
{
	internal class Program
	{
		static List<Client>? clients;
		static Dictionary<string, Client>? dictionary;
		static List<Employee>? employees;
		static void Main(string[] args)
		{
			clients = GenerateClints(1000).ToList();
			dictionary = GenerateClintesDictionary(clients!).ToDictionary(pair => pair.Key, pair=>pair.Value);
			employees = GenerateEmployees(1000).ToList();

			Console.WriteLine("Поиск клиента по телефону с применением StopWatch:");
			A(clients);

			Console.WriteLine("Поиск клиента по телефону с применением StopWatch в словаре:");
			if (clients is not null)
				B(dictionary, clients[400].Phone);

			Console.WriteLine($"Выборка клиентов, возраст которых ниже определенного значения (меньше 25 лет):");
			C(clients);

			Console.WriteLine($"Поиск сотрудника с минимальной заработной платой (минимальная ЗП - 1000):");
			D(employees);

			Console.WriteLine("Cравнение скорости поиска по словарю двумя методами");
			if (clients is not null)
				E(dictionary, clients[clients.Count - 1].Phone);

			Console.ReadKey();
		}

		//поиск клиента по телефону с применением StopWatch
		static void A(List<Client>? someclients)
		{
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			Console.WriteLine(someclients?.Any(phone => phone.Phone == someclients[400].Phone));
			stopWatch.Stop();
			Console.WriteLine("Прошло тиков " + stopWatch.ElapsedTicks + "\n");

		}
		//поиск клиента по телефону с применением StopWatch в словаре
		static void B(Dictionary<string, Client>? someclients, string? phone)
		{

			Stopwatch stopWatch2 = new Stopwatch();
			stopWatch2.Start();
			Console.WriteLine(someclients?.Any(t => t.Key == phone));
			stopWatch2.Stop();
			Console.WriteLine("Прошло тиков " + stopWatch2.ElapsedTicks + "\n");
		}

		//выборка клиентов, возраст которых ниже определенного значения;
		static void C(List<Client>? someclients)
		{

			Stopwatch stopWatch2 = new Stopwatch();
			stopWatch2.Start();
			var needed = someclients?.Where(d => (DateTime.Now.Year - d.DateOfBirth.Year < 25)).ToList();
			stopWatch2.Stop();
			Console.WriteLine($"Найдено {needed?.Count()}. Прошло тиков {stopWatch2.ElapsedTicks}\n"); ;
		}

		//поиск сотрудника с минимальной заработной платой;
		static void D(List<Employee>? someemployees)
		{

			Stopwatch stopWatch2 = new Stopwatch();
			stopWatch2.Start();
			var needed = someemployees?.Where(d => d.Salary == someemployees.Min(e => e.Salary)).ToList();
			stopWatch2.Stop();
			Console.WriteLine($"Всего таких сотрудников: {needed?.Count()}. Прошло тиков  {stopWatch2.ElapsedTicks} \n"); ;
		}

		//сравнение скорости поиска по словарю двумя методами
		static void E(Dictionary<string, Client>? someclients, string? phone)
		{

			Stopwatch stopWatch3 = new Stopwatch();
			stopWatch3.Start();
			someclients?.Reverse().FirstOrDefault();
			stopWatch3.Stop();
			long elapsedTime3 = stopWatch3.ElapsedTicks;
			if (someclients is not null && phone is not null)
			{
				stopWatch3.Start();
				if (someclients.ContainsKey(phone))
					Console.WriteLine(true);
				stopWatch3.Stop();
				long elapsedTime4 = stopWatch3.ElapsedTicks;
				if (elapsedTime3 > elapsedTime4)
					Console.WriteLine($"Поиск FirstOrDefault медленнее; \nFirstOrDefault: {elapsedTime3}\nByKeyValue: {elapsedTime4}");
				else
					Console.WriteLine($"Поиск FirstOrDefault быстрее; \nFirstOrDefault: {elapsedTime3}\nByKeyValue: {elapsedTime4}");
			}
		}
	}
}
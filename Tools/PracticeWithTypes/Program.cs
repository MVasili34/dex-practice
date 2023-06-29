using Models;

namespace PracticeWithTypes;

internal class Program
{
	static List<Employee>? employees;
	static List<Currency>? currencies;
	static void Main(string[] args)
	{
		employees = new List<Employee>() {
			new Employee("Агатов", "Пётр", DateOnly.Parse("21.02.2001"), "1993123311", "AB1331", "Директор", 199999),
			new Employee("Окотов", "Иван", DateOnly.Parse("15.04.1995"), "5124991244", "AB2144", "Кассир", 999) };
		currencies = new List<Currency>() {
			new Currency("USD 840", "American Dollar"),
			new Currency("BYN 933", "Belarusian Ruble") };
		PrintEmployees(employees);
		PrintCurrencies(currencies);

		//пример обновления контракта
		UpdateContract(employees[1]);

		//примеер изменения информации о валюте
		currencies[0] = ChangeCurrency(currencies[0], "New Code", "New Name");

		Console.WriteLine("\nПосле применения методов: ");
		PrintEmployees(employees);
		PrintCurrencies(currencies);
		Console.ReadKey();
	}

	/// <summary>
	/// Метод обновления контракта сотрудника
	/// </summary>
	/// <param name="employee">Сотрудник передаётся по ссылке</param>
	static void UpdateContract(Employee employee) => employee.Contract = $"Контракт обновлён у {employee.FirstName}, " +
		$"{employee.LastName}. Дата и время: {DateTime.Now}";

	/// <summary>
	/// Метод, обновляющий сущность валюты
	/// </summary>
	/// <param name="t">Исходная сущность</param>
	/// <param name="newCode">Новое значение</param>
	/// <param name="newName">Новое значение</param>
	/// <returns>Обновлённая сущность</returns>
	static Currency ChangeCurrency(Currency t, string newCode, string newName)
	{
		t.CurrencyCode = newCode;
		t.Name = newName;
		return t;
	}

	/// <summary>
	/// Метод вывода сотрудников в консоль
	/// </summary>
	/// <param name="toPrint">Список сотрудников</param>
	static void PrintEmployees(List<Employee>? toPrint)
	{
		if (toPrint is not null)
		{
			Console.WriteLine($"\nСотрудники:");
			foreach (var item in toPrint) 
			{
				Console.WriteLine($"Сотрудник: {item.FirstName}, {item.LastName}, {item.Phone}" +
					$"\n\tКонтракт: {item.Contract}");
			}
		}
	}

	/// <summary>
	/// Метод вывода валют в консоль
	/// </summary>
	/// <param name="toPrint">Список валют</param>
	static void PrintCurrencies(List<Currency>? toPrint)
	{
		if (toPrint is not null)
		{
			Console.WriteLine($"\nВалюты:");
			foreach (var item in toPrint)
			{
				Console.WriteLine($"{item.CurrencyCode}, {item.Name}");
			}
		}
	}
}
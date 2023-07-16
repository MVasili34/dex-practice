using Models;

namespace PracticeWithTypes;

internal class Program
{
	static List<Employee>? employees;
	static List<Currency>? currencies;
	static void Main(string[] args)
	{
		employees = new List<Employee>() {
			new Employee("Агатов", "Пётр", new DateOnly(2001, 2, 21), "1993123311", "AB1331", "Директор", 199999),
			new Employee("Окотов", "Иван", new DateOnly(1995, 4, 15), "5124991244", "AB2144", "Кассир", 999) 
		};
		currencies = new List<Currency>() {
			new Currency("USD 840", "American Dollar"),
			new Currency("BYN 933", "Belarusian Ruble") 
		};
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
	static void UpdateContract(Employee employee) => employee.Contract = $"Контракт обновлён " +
		$"у {employee.FirstName}, {employee.LastName}. Дата и время: {DateTime.Now}";

	/// <summary>
	/// Метод, обновляющий сущность валюты
	/// </summary>
	/// <param name="currency">Исходная сущность</param>
	/// <param name="newCode">Новое значение</param>
	/// <param name="newName">Новое значение</param>
	/// <returns>Обновлённая сущность</returns>
	static Currency ChangeCurrency(Currency currency, string newCode, string newName)
	{
		currency.CurrencyCode = newCode;
		currency.CurrencyName = newName;
		return currency;
	}

	/// <summary>
	/// Метод вывода сотрудников в консоль
	/// </summary>
	/// <param name="employees">Коллекция сотрудников</param>
	static void PrintEmployees(IEnumerable<Employee>? employees)
	{
		if (employees is not null)
		{
			Console.WriteLine($"\nСотрудники:");
			foreach (var employee in employees) 
			{
				Console.WriteLine($"Сотрудник: {employee.FirstName}, {employee.LastName}, " +
					$"{employee.Phone} \n\tКонтракт: {employee.Contract}");
			}
		}
	}

	/// <summary>
	/// Метод вывода валют в консоль
	/// </summary>
	/// <param name="currencies">Коллекция валют</param>
	static void PrintCurrencies(IEnumerable<Currency>? currencies)
	{
		if (currencies is not null)
		{
			Console.WriteLine($"\nВалюты:");
			foreach (var currency in currencies)
			{
				Console.WriteLine($"{currency.CurrencyCode}, {currency.CurrencyName}");
			}
		}
	}
}
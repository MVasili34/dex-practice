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
			new Employee("Окотов", "Иван", DateOnly.Parse("15.04.1995"), "5124991244", "AB2144", "Кассир", 999) 
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
	static void UpdateContract(Employee employee) => employee.Contract = $"Контракт обновлён у {employee.FirstName}, " +
		$"{employee.LastName}. Дата и время: {DateTime.Now}";

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
	/// <param name="employees">Список сотрудников</param>
	static void PrintEmployees(List<Employee>? employees)
	{
		if (employees is not null)
		{
			Console.WriteLine($"\nСотрудники:");
			foreach (var employee in employees) 
			{
				Console.WriteLine($"Сотрудник: {employee.FirstName}, {employee.LastName}, {employee.Phone}" +
					$"\n\tКонтракт: {employee.Contract}");
			}
		}
	}

	/// <summary>
	/// Метод вывода валют в консоль
	/// </summary>
	/// <param name="currencies">Список валют</param>
	static void PrintCurrencies(List<Currency>? currencies)
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
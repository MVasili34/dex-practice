using Models;
using Services;

namespace PracticeWithCasting;

internal class Program
{
	static void Main(string[] args)
	{
		var owners = new List<Employee>() {
			new Employee("Агатов", "Пётр", new(2001, 02, 21), "1993123311", "AB234", "Владелец", 0),
			new Employee("Окотов", "Иван", new(1995, 04, 15), "5124991244", "AB233", "Владелец", 0)};
		PrintEmployees(owners);
		
		//вызов метода расчета зарплат владельцев банка
		foreach (var owner in owners)
		{
			owner.Salary = BankService.OwnerPayment(owners.Count());
		}
		Console.WriteLine($"\n***Новые зарплаты:");
		PrintEmployees(owners);

		Client someClient = new("Федотов", "Александр", new(1993, 02, 23), "1223331", "AB213", "CompanyNone", "AdressNone");
		Console.WriteLine($"\nКлиент: {someClient.FirstName}, {someClient.LastName}, {someClient.DateOfBirth}," +
			$" {someClient.Phone}, {someClient.Company}, {someClient.AdressInfo}");

		//тестирование кастинга из клиента в сотрудника (в классе Client определён explicit operator метод)
		Employee t = (Employee)someClient;

		Console.WriteLine($"\n***Кастинг:");
		Console.WriteLine($"Из клиента в сотрудника: {t.FirstName}, {t.LastName}, {t.DateOfBirth}," +
			$" {t.Phone}, {t.Position}, {t.Salary}, {t.Contract}");

		Console.ReadKey();
	}

    /// <summary>
    /// Метод вывода сотрудников в консоль
    /// </summary>
    /// <param name="toPrint">Список сотрудников</param>
    static void PrintEmployees(List<Employee> toPrint)
	{
		if (toPrint is not null)
		{
			Console.WriteLine($"Владельцы:");
			foreach (var item in toPrint)
			{
				Console.WriteLine($"Владелец: {item.FirstName}, {item.LastName}, {item.Phone}, ЗП: {item.Salary}" +
					$"\n\tКонтракт: {item.Contract}");
			}
		}
	}
}

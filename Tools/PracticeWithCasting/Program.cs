using Models;
using Services;

namespace PracticeWithCasting;

internal class Program
{
	static void Main(string[] args)
	{
		var owners = new List<Employee>() {
			new Employee("Агатов", "Пётр", new(2001, 02, 21), "1993123311", "AB234", "Владелец", 0),
			new Employee("Окотов", "Иван", new(1995, 04, 15), "5124991244", "AB233", "Владелец", 0)
		};
		PrintEmployees(owners);
		
		//вызов метода расчета зарплат владельцев банка
		foreach (var owner in owners)
		{
			owner.Salary = BankService.OwnerPayment(owners.Count());
		}
		Console.WriteLine($"\n***Новые зарплаты:");
		PrintEmployees(owners);

		Client client = new("Федотов", "Александр", new(1993, 02, 23), "1223331", "AB213", "CompanyNone", "AdressNone");
		Console.WriteLine($"\nКлиент: {client.FirstName}, {client.LastName}, {client.DateOfBirth}," +
			$" {client.Phone}, {client.Company}, {client.AddressInfo}");

		//тестирование кастинга из клиента в сотрудника (в классе Client определён explicit operator метод)
		Employee employee = (Employee)client;

		Console.WriteLine($"\n***Кастинг:");
		Console.WriteLine($"Из клиента в сотрудника: {employee.FirstName}, {employee.LastName}, " +
			$"{employee.DateOfBirth}, {employee.Phone}, {employee.Position}, {employee.Salary}, {employee.Contract}");

		Console.ReadKey();
	}

    /// <summary>
    /// Метод вывода сотрудников в консоль
    /// </summary>
    /// <param name="employees">Список сотрудников</param>
    static void PrintEmployees(List<Employee> employees)
	{
		if (employees is not null)
		{
			Console.WriteLine($"Владельцы:");
			foreach (var employee in employees)
			{
				Console.WriteLine($"Владелец: {employee.FirstName}, {employee.LastName}, {employee.Phone}, " +
					$"ЗП: {employee.Salary} \n\tКонтракт: {employee.Contract}");
			}
		}
	}
}

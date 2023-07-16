using Models;
using Services;

namespace PracticeWithCasting;

internal class Program
{
	static List<Employee>? owners;
	static Client? client;
	static void Main(string[] args)
	{
		owners = new List<Employee>() {
			new Employee("Агатов", "Пётр", new DateOnly(2001, 02, 21), "1993123311", "AB234", "Владелец", 0),
			new Employee("Окотов", "Иван", new DateOnly(1995, 04, 15), "5124991244", "AB233", "Владелец", 0)
		};
		PrintEmployees(owners);

        client = new Client("Федотов", "Александр", new DateOnly(1993, 02, 23),
			"1223331", "AB213", "CompanyNone", "AdressNone");
        Console.WriteLine($"\nКлиент: {client.FirstName}, {client.LastName}, " +
			$"{client.DateOfBirth}, {client.Phone}, {client.Company}, {client.AddressInfo}");

        //вызов метода расчета зарплат владельцев банка
        foreach (var owner in owners)
		{
			owner.Salary = BankService.OwnerPayment(owners.Count());
		}

		//тестирование кастинга из клиента в сотрудника
		//(в классе Client определён explicit operator метод)
		Employee employee = (Employee)client;

        Console.WriteLine($"\n***После применения методов:");

        PrintEmployees(owners);
        Console.WriteLine($"Кастинг из клиента в сотрудника: {employee.FirstName}, {employee.LastName}, " +
			$"{employee.DateOfBirth}, {employee.Position}, {employee.Salary}, {employee.Contract}");

		Console.ReadKey();
	}

    /// <summary>
    /// Метод вывода сотрудников в консоль
    /// </summary>
    /// <param name="employees">Коллекция сотрудников</param>
    static void PrintEmployees(IEnumerable<Employee>? employees)
	{
		if (employees is not null)
		{
			Console.WriteLine($"Владельцы:");
			foreach (var employee in employees)
			{
				Console.WriteLine($"Владелец: {employee.FirstName}, {employee.LastName}, " +
					$"{employee.Phone}, ЗП: {employee.Salary} \n\tКонтракт: {employee.Contract}");
			}
		}
	}
}

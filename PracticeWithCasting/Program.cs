using Models;
using Services;

namespace PracticeWithCasting
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var owners = new List<Employee>() {
				new Employee("Агатов", "Пётр", DateOnly.Parse("21.02.2001"), "1993123311", "AB234", "Владелец", 0),
				new Employee("Окотов", "Иван", DateOnly.Parse("15.04.1995"), "5124991244", "AB233", "Владелец", 0)};
			PrintEmployees(owners);

			//вызов метода расчета зарплат владельцев банка
			foreach (var owner in owners)
			{
				owner.Salary = BankService.OwnerPayment(owner, owners.Count());
			}
			Console.Write($"\n***Новые зарплаты:");
			PrintEmployees(owners);


			Client someClient = new("Федотов", "Александр", DateOnly.Parse("21.02.1993"), "1223331", "AB213", "CompanyNone", "AdressNone");
			Console.WriteLine($"\nКлиент: {someClient.FirstName}, {someClient.LastName}, {someClient.DateOfBirth}," +
				$" {someClient.Phone}, {someClient.Company}, {someClient.AdressInfo}");
			//тестирование кастинга из клиента в сотрудника (в классе Client определён explicit operator метод)
			Employee t = (Employee)someClient;

			Console.WriteLine($"\n***Кастинг:");
			Console.WriteLine($"Из клиента в сотрудника: {t.FirstName}, {t.LastName}, {t.DateOfBirth}," +
				$" {t.Phone}, {t.Position}, {t.Salary}, {t.Contract}");

			Console.ReadKey();
		}

		static void PrintEmployees(List<Employee> toPrint)
		{
			if (toPrint is not null)
			{
				Console.WriteLine($"\nВладельцы:");
				foreach (var item in toPrint)
				{
					Console.WriteLine($"Владелец: {item.FirstName}, {item.LastName}, {item.Phone}, ЗП: {item.Salary}" +
						$"\n\tКонтракт: {item.Contract}");
				}
			}
		}
	}
}
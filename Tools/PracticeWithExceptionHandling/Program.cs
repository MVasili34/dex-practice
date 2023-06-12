using Models;
using Services;
using System.Security.Principal;
using static System.Console;

namespace PracticeWithExceptionHandling
{
	internal class Program
	{
		static void Main(string[] args)
		{
			TestsWithClient();
			WriteLine("Для проложения нажмите любую кнопку...");
			ReadKey();
			TestsWithEmployee();
		}

		static void TestsWithClient()
		{
			try
			{
				ClientService service = new();

				//создаём тестовый образец клиента, которому меньше 18 лет и с пустыми пасспортом
				Client client = new("Имя1", "Имя2", DateOnly.Parse("21.02.2010"), "22344114", "", "NONE", "");

				WriteLine("Попытка поместить клиента в список клиентов банка:");
				service.Add(client);

				client.DateOfBirth = DateOnly.Parse("21.02.2005");
				WriteLine("Попытка поместить клиента в список клиентов банка");
				service.Add(client);
				WriteLine();

				//окончательно добавляем клиента, изменяя значения полей
				client.Passport = "AB12345678";

				//показываем, что дефолтный лицевой счёт создан
				service.Add(client);
				service.PrintOut();
				WriteLine();

				Account account = new(new Currency("BS212", "Usd."), 10);

				//добавляем счёт клиенту
				service.AddAccount(client, account);
				service.PrintOut();
				WriteLine();
				Write("Выберите номер счёта, который хотите изменить: ");
				int chosen = int.Parse(ReadLine()!) - 1;

				Account? clientAccount = service.RetrieveAllAccounts(client)?.ToList()[chosen];

				if (clientAccount is null)
					throw new ArgumentNullException(nameof(clientAccount));

				//тестируем метод обновления лицевого счёта
				Write("Новая сумма счёта: ");
				clientAccount.Amount = int.Parse(ReadLine()!);
				service.UpdateAccount(client, chosen, clientAccount);
				WriteLine();
				service.PrintOut();
			}
			catch (FormatException ex)
			{
				WriteLine(ex.Message);
			}
			catch (ArgumentNullException ex)
			{
				WriteLine(ex.Message);
			}
			catch (IndexOutOfRangeException ex)
			{
				WriteLine(ex.Message);
			}
			catch (Exception ex)
			{
				WriteLine(ex.Message);
			}
			ReadKey();
		}
		static void TestsWithEmployee()
		{
			try
			{
				EmployeeService service = new();

				//создаём тестовый образец клиента, которому меньше 18 лет и с пустыми контрактом
				Employee employee = new("Имя1", "Имя2", DateOnly.Parse("21.02.2010"), "22344114", "AB2331", "Уборщик", 10000, "");

				WriteLine("\nПопытка добавить сотрудника в список сотрудников банка:");
				service.AddEmployee(employee);

				employee.DateOfBirth = DateOnly.Parse("21.02.2000");
				WriteLine("Попытка добавить сотрудника в список сотрудников банка:");
				service.AddEmployee(employee);

				WriteLine("Попытка добавить сотрудника в список сотрудников банка:");
				employee.Contract = "Заключён контракт";
				service.AddEmployee(employee);
				WriteLine();

				service.PrintOut();
				WriteLine();

				Employee employeeEdited = new("Имя1", "Имя2", DateOnly.Parse("21.02.2000"), "22344114", 
					"AB2331", "Уборщик", -5, "Заключён контракт");

				//попытка редактирования
				service.UpdateEmployee(0, employeeEdited);
				employeeEdited.Salary = 10000;
				service.UpdateEmployee(0, employeeEdited);
				WriteLine();
				service.PrintOut();
			}
			catch (FormatException ex)
			{
				WriteLine(ex.Message);
			}
			catch (IndexOutOfRangeException ex)
			{
				WriteLine(ex.Message);
			}
			catch (Exception ex)
			{
				WriteLine(ex.Message);
			}
			ReadKey();
		}
	}
}
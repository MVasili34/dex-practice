using Models;

namespace Services
{
	public class BankService
	{
		public static decimal Income { get; set; } = 250_000;
		public static decimal Expense { get; set; } = 200_000;
		private static List<Client> blackListClient = new List<Client>(0);
		private static List<Employee> blackListEmployee = new List<Employee>(0);
		public BankService() { }

		public static int OwnerPayment(Employee employee, int amountofOwners) => amountofOwners != 0 ?
			(int)(Income - Expense) / amountofOwners : 0;
		public void AddBonus<T>(T person) where T : Person
		{
			try
			{
				if (person is null)
					throw new ArgumentNullException(nameof(person));
				if (person is Client)
				{
					Client? client = person as Client;
					if (client is not null)
					{
						client.AdressInfo += " BONUS";
					}
				}
				else
				{
					Employee? employee = person as Employee;
					if (employee is not null)
					{
						employee.Salary += 1000;
					}
				}
			}
			catch (ArgumentNullException ex)
			{
				Console.WriteLine($"{ex.Message}");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void AddToBlackList<T>(T person) where T : Person
		{
			try
			{
				if (person is null)
					throw new ArgumentNullException(nameof(person));
				if (person is Client)
				{
					Client? client = person as Client;
					if (client is not null)
					{
						blackListClient.Add(client);
					}
				}
				else
				{
					Employee? employee = person as Employee;
					if (employee is not null)
					{
						blackListEmployee.Add(employee);
					}
				}
			}
			catch (ArgumentNullException ex)
			{
				Console.WriteLine($"{ex.Message}");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public bool? IsPersonInBlackList<T>(T person) where T : Person
		{
			try
			{
				if (person is null)
					throw new ArgumentNullException(nameof(person));
				if (person is Client)
				{
					Client? client = person as Client;
					if (client is not null)
					{
						if (blackListClient.Contains(client))
							return true;
						else
							return false;
					}
				}
				else
				{
					Employee? employee = person as Employee;
					if (employee is not null)
					{
						if (blackListEmployee.Contains(employee))
							return true;
						else
							return false;
					}
				}
			}
			catch (ArgumentNullException ex)
			{
				Console.WriteLine($"{ex.Message}");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return null;
		}
	}
}
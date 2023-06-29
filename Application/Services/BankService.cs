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

        /// <summary>
        /// Метод расчета зарплаты владельцев банка
        /// </summary>
        /// <param name="amountofOwners">Количество владельцев</param>
        /// <returns>Зарплата владельца</returns>
        public static int OwnerPayment(int amountofOwners) => amountofOwners != 0 ?
			(int)(Income - Expense) / amountofOwners : 0;

		/// <summary>
		/// Метод добавления бонусу клиенту или сотруднику
		/// </summary>
		/// <typeparam name="T">Наследник класса Person</typeparam>
		/// <param name="person">Входная сущность</param>
		public void AddBonus<T>(T person) where T : Person
		{
			try
			{
				if (person is null)
					throw new ArgumentNullException(nameof(person));
				if (person is Client)
				{
					Client? client = person as Client;
					client!.AdressInfo += " BONUS";
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

        /// <summary>
        /// Метод добавления сущности в чёрный список
        /// </summary>
        /// <typeparam name="T">Наследник класса Person</typeparam>
        /// <param name="person">Входная сущность</param>
        public void AddToBlackList<T>(T person) where T : Person
		{
			try
			{
				if (person is null)
					throw new ArgumentNullException(nameof(person));
				if (person is Client)
				{
					Client? client = person as Client;
					blackListClient.Add(client!);
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

        /// <summary>
        /// Метод проверки нахождения сущности в чёрном списке
        /// </summary>
        /// <typeparam name="T">Наследник класса Person</typeparam>
        /// <param name="person">Входная сущность</param>
        /// <returns>Статус пребывания</returns>
        public bool? IsPersonInBlackList<T>(T person) where T : Person
		{
			try
			{
				if (person is null)
					throw new ArgumentNullException(nameof(person));
				if (person is Client)
				{
					Client? client = person as Client;
					if (blackListClient.Contains(client!))
					{
						return true;
					}
					return false;
				}
				else
				{
					Employee? employee = person as Employee;
					if (employee is not null)
					{
						if (blackListEmployee.Contains(employee))
						{
							return true;
						}
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
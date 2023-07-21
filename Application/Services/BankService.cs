using Models;

namespace Services;

public class BankService
{
    private readonly List<Client> _blackListClient = new(0);
    private readonly List<Employee> _blackListEmployee = new(0);

    public static decimal Income { get; set; } = 250_000;
    public static decimal Expense { get; set; } = 200_000;
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
        if (person is null)
            throw new ArgumentNullException(nameof(person));
        if (person is Client)
        {
            Client? client = person as Client;
            if (client is not null)
            {
                client.AddressInfo += " BONUS";
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

    /// <summary>
    /// Метод добавления сущности в чёрный список
    /// </summary>
    /// <typeparam name="T">Наследник класса Person</typeparam>
    /// <param name="person">Входная сущность</param>
    public void AddToBlackList<T>(T person) where T : Person
	{
        if (person is null)
            throw new ArgumentNullException(nameof(person));
        if (person is Client)
        {
            Client? client = person as Client;
            if (client is not null)
            {
                _blackListClient.Add(client);
            }
        }
        else
        {
            Employee? employee = person as Employee;
            if (employee is not null)
            {
                _blackListEmployee.Add(employee);
            }
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
        if (person is null)
            throw new ArgumentNullException(nameof(person));
        if (person is Client)
        {
            Client? client = person as Client;
            if (client is not null)
            {
                if (_blackListClient.Contains(client))
                {
                    return true;
                }
                return false;
            }
        }
        else
        {
            Employee? employee = person as Employee;
            if (employee is not null)
            {
                if (_blackListEmployee.Contains(employee))
                {
                    return true;
                }
                return false;
            }
        }
			return null;
	}
}
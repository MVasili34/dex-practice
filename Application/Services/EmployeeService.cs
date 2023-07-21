using Models;
using Services.Exceptions;
using Services.Storage;

namespace Services;

public class EmployeeService : IEmployeeStorage
{
	public List<Employee> Data { get; } = new();
	public EmployeeService() { }
	public EmployeeService(List<Employee> data)
	{
		Data = data;
	}

    /// <summary>
    /// Метод фильтрации сотрудников по соответствующим параметрам
    /// </summary>
    /// <param name="fName">Имя</param>
    /// <param name="lName">Фамилия</param>
    /// <param name="phone">Номер телефона</param>
    /// <param name="info">Паспорт</param>
    /// <param name="sDate">Начальная дата</param>
    /// <param name="eDate">Конечная дата</param>
    /// <returns>Отфильтрованная коллекция сотрудников</returns>
    public IEnumerable<Employee> FilterMethod(string fName, string lName, string phone, string passport,
		DateOnly sDate, DateOnly eDate) => Data?.Where(p => 
			p.FirstName.Contains(fName, StringComparison.OrdinalIgnoreCase) &&
			p.LastName.Contains(lName, StringComparison.OrdinalIgnoreCase) &&
			p.Phone.Contains(phone, StringComparison.OrdinalIgnoreCase) &&
			p.Passport.Contains(passport, StringComparison.OrdinalIgnoreCase) &&
			(p.DateOfBirth >= sDate && p.DateOfBirth <= eDate))!;

    /// <summary>
    /// Метод получения старейших сотрудников
	/// </summary>
    /// <returns>Словарь соответствующих сотрудников</returns>
    public IEnumerable<Employee> GetOldestEmployees() => Data?
		.Where(p => p.DateOfBirth == Data.Min(t => t.DateOfBirth))!;

	/// <summary>
    /// Метод получения самых молодых сотрудников
    /// </summary>
    /// <returns>Словарь соответствующих сотрудников</returns>
    public IEnumerable<Employee> GetYoungestEmployees() => Data?
		.Where(p => p.DateOfBirth == Data.Max(t => t.DateOfBirth))!;

    /// <summary>
    /// Метод получения среднего возраста сотрудников
    /// </summary>
    /// <returns>Средний сотрудников</returns>
    public int GetAvarageAge() => DateTime.Now.DayOfYear < Data?.Average(p => p.DateOfBirth.DayOfYear) ?
		DateTime.Now.Year - (int)Data.Average(p => p.DateOfBirth.Year) :
		DateTime.Now.Year - (int)Data?.Average(p => p.DateOfBirth.Year)! - 1;

	/// <summary>
    /// Метод добавления сотрудеика в хранилище
    /// </summary>
    /// <param name="employee">Сотрудник</param>
    /// <exception cref="Below18Exception"></exception>
    /// <exception cref="NullContractException"></exception>
    public void AddEmployee(Employee? employee)
	{
		if (employee is not null)
		{
			if ((employee.DateOfBirth.DayOfYear <= DateTime.Now.DayOfYear ? DateTime.Now.Year - employee.DateOfBirth.Year :
				DateTime.Now.Year - employee.DateOfBirth.Year - 1) < 18)
			{
				throw new Below18Exception("Сотружник не может быть младше 18 лет");
			}
			else if (string.IsNullOrEmpty(employee.Contract))
			{
				throw new NullContractException("У сотрудника нет контракта");
			}
			Data?.Add(employee);
		}
	}

	/// <summary>
    /// Метод обновления данных сотрудника
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <param name="employee">Сотрудник</param>
    /// <exception cref="IncorrectEmployeeException"></exception>
    public void UpdateEmployee(int employeeId, Employee employee)
	{
		if (Data is not null)
		{
			if (string.IsNullOrEmpty(employee.Contract) || string.IsNullOrEmpty(employee.Position) ||
				employee.Salary < 0)
			{
				throw new IncorrectEmployeeException("Некорректные данные сотрудника!");
			}
			Data[employeeId] = employee;
		}
	}

	/// <summary>
    /// Метод удаления сотрудика из хранилища
    /// </summary>
    /// <param name="client">Сотрудник</param>
    /// <exception cref="FailedToRemoveException"></exception>
    public void DeleteEmployee(Employee? employee)
	{
		if (employee is not null)
		{
			if (!Data.Remove(employee))
				throw new FailedToRemoveException("Невозможнло удалить сотрудника!");
		}
		else
			throw new FailedToRemoveException("Невозможнло удалить сотрудника!");
	}
}

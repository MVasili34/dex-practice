using EntityModels;
using Microsoft.EntityFrameworkCore;

namespace ServicesDb;

public class EmployeeService : IEmployeeService
{
	private readonly BankingServiceContext _bankContext;

    private readonly int _pageSize = 10;
    public EmployeeService(BankingServiceContext bankContext)
	{
		_bankContext = bankContext;
	}

    /// <summary>
    /// Метод добавления сотрудника в БД
    /// </summary>
    /// <param name="employee">Сотрудник</param>
    /// <returns>Сотрудник, если процесс добавления прошёл успешно</returns>
    public async Task<Employee?> AddEmployeeAsync(Employee employee)
	{
		await _bankContext.Employees.AddAsync(employee);
		int affected = await _bankContext.SaveChangesAsync();
		if (affected == 1)
		{
			return await RetrieveEmployeeAsync(employee.EmployeeId);
        }
		    return null;
	}

    /// <summary>
    /// Метод получения сотрудников из БД (с применением пагинации)
    /// </summary>
    /// <param name="page">Номер страницы</param>
    /// <returns>Коллекция сотрудников соответствующей страницы</returns>
    public async Task<IEnumerable<Employee>> RetrieveAllAsync(int? page) => await _bankContext.Employees
        .Skip(((page ?? 1) - 1) * _pageSize)
        .Take(_pageSize)
        .ToListAsync();

    /// <summary>
    /// Метод получения сотрудника по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Сотрудник из базы данных</returns>
    public async Task<Employee?> RetrieveEmployeeAsync(Guid id) => await _bankContext.Employees.FindAsync(id);

    /// <summary>
    /// Метод обновления данных сотрудника
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="employee">Сотрудник</param>
    /// <returns>Сотрудник, если успешно обновлён</returns>
    public async Task<Employee?> UpdateEmployeeAsync(Guid id, Employee employee)
	{
        Employee? existingEmployee = await RetrieveEmployeeAsync(id);
        if (existingEmployee is not null)
        {
            _bankContext.Entry(existingEmployee).CurrentValues.SetValues(employee);
            int affected = await _bankContext.SaveChangesAsync();
            if (affected == 1)
            {
                return await RetrieveEmployeeAsync(id);
            }
        }
            return null;
    }

    /// <summary>
    /// Метод удаления сотрудника по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Статус операции</returns>
    public async Task<bool?> DeleteEmployeeAsync(Guid id)
	{
		Employee? employee = await RetrieveEmployeeAsync(id);
        if (employee is null) 
            return null;
		_bankContext.Employees.Remove(employee);
		int affected = await _bankContext.SaveChangesAsync();
		if (affected == 1)
		{
            return true;
		}
			return null;
	}
}

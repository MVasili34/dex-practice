using EntityModels;
using Microsoft.EntityFrameworkCore;

namespace ServicesDb;

public class EmployeeService : IEmployeeService
{
	private BankingServiceContext _bankContext;
	public EmployeeService(BankingServiceContext _bankContext)
	{
		this._bankContext = _bankContext;
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
    /// Метод получения всех сотрудников из БД
    /// </summary>
    /// <returns>Коллекция сотрудников</returns>
    public async Task<IEnumerable<Employee>> RetrieveAllAsync() => await _bankContext.Employees.ToListAsync();

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

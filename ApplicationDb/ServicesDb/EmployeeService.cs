using EntityModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace ServicesDb;

public class EmployeeService : IEmployeeService
{

	private BankingServiceContext db;
	public EmployeeService(BankingServiceContext db)
	{
		this.db = db;
	}

    /// <summary>
    /// Метод добавления сотрудника в БД
    /// </summary>
    /// <param name="employee">Сотрудник</param>
    /// <returns>Сотрудник, если процесс добавления прошёл успешно</returns>
    public async Task<Employee?> AddEmployeeAsync(Employee employee)
	{
		await db.Employees.AddAsync(employee);
		int affected = await db.SaveChangesAsync();
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
    public async Task<IEnumerable<Employee>> RetrieveAllAsync() => await db.Employees.ToListAsync();

    /// <summary>
    /// Метод фильтрации сотрудников по дате рождения
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns>Отфильтрованная коллекция</returns>
    public async Task<IEnumerable<Employee>> GetFiltered(DateOnly startDate, DateOnly endDate) => await db.Employees
		.Where(p =>p.DateOfBirth > startDate && p.DateOfBirth < endDate)
		.OrderBy(p => p.DateOfBirth).ToListAsync();

    /// <summary>
    /// Метод получения сотрудника по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Сотрудник из базы данных</returns>
    public async Task<Employee?> RetrieveEmployeeAsync(Guid id) => await db.Employees.FindAsync(id);

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
            db.Entry(existingEmployee).CurrentValues.SetValues(employee);
            int affected = await db.SaveChangesAsync();
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
		db.Employees.Remove(employee);
		int affected = await db.SaveChangesAsync();
		if (affected == 1)
		{
            return true;
		}
			return null;
	}
}

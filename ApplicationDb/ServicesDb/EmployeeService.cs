using EntityModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesDb;

public class EmployeeService : IEmployeeService
{
    //потокобезопасный словрь для хранения сотрудников
    private static ConcurrentDictionary<Guid, Employee>? employeeCache;

	private BankingServiceContext db;
	public EmployeeService(BankingServiceContext injectedContext)
	{
		db = injectedContext;
		if (employeeCache is null)
		{
			employeeCache = new ConcurrentDictionary<Guid, Employee>(
			db.Employees.ToDictionary(c => c.EmployeeId));
		}
	}

    /// <summary>
    /// Метод добавления сотрудника в БД
    /// </summary>
    /// <param name="c">Сотрудник</param>
    /// <returns>Сотрудник, если процесс добавления прошёл успешно</returns>
    public async Task<Employee?> AddEmployeeAsync(Employee c)
	{
		EntityEntry<Employee> added = await db.Employees.AddAsync(c);
		int affected = await db.SaveChangesAsync();
		if (affected == 1)
		{
			if (employeeCache is null)
				return c;
			//нового работника в кэш, иначе вызываем UpdateCache
			return employeeCache.AddOrUpdate(c.EmployeeId, c, UpdateCache);
		}
		return null;
	}

    /// <summary>
    /// Метод получения всех сотрудников из кэша
    /// </summary>
    /// <returns>Содержимое кэша</returns>
    public Task<IEnumerable<Employee>> RetrieveAllAsync() => Task.FromResult(employeeCache is null ?
		Enumerable.Empty<Employee>() : employeeCache.Values);

    /// <summary>
    /// Метод фильтрации сотрудников по дате рождения
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns>Отфильтрованная коллекция</returns>
    public Task<IEnumerable<Employee>> GetFiltered(DateOnly startDate, DateOnly endDate) =>
		Task.FromResult(employeeCache is null ? Enumerable.Empty<Employee>() : employeeCache.Values.Where(p =>
		p.DateOfBirth > startDate && p.DateOfBirth < endDate).OrderBy(p => p.DateOfBirth));

    /// <summary>
    /// Метод получения сотрудника по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Сотрудник из кэша</returns>
    public Task<Employee?> RetrieveEmployeeAsync(Guid id)
	{
		if (employeeCache is null) return null!;
		employeeCache.TryGetValue(id, out Employee? employee);
		return Task.FromResult(employee);
	}

    /// <summary>
    /// Метод обновления кэша
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="c">Сотрудник</param>
    /// <returns>Сотрудник, если кэш обновлён</returns>
    private Employee UpdateCache(Guid id, Employee c)
	{
		Employee? old;
		if (employeeCache is not null)
		{
			if (employeeCache.TryGetValue(id, out old))
			{
				if (employeeCache.TryUpdate(id, c, old))
				{
					return c;
				}
			}
		}
		return null!;
	}

    /// <summary>
    /// Метод обновления данных сотрудника
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="c">Сотрудник</param>
    /// <returns>Сотрудник, если успешно обновлён</returns>
    public async Task<Employee?> UpdateEmployeeAsync(Guid id, Employee c)
	{
		//обновляем в базе
		db.Entry(c).State = EntityState.Modified;
		int affected = await db.SaveChangesAsync();
		if (affected == 1)
		{
			return UpdateCache(id, c);
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
		Employee? c = db.Employees.Find(id);
		if (c is null) return null;
		db.Employees.Remove(c);
		int affected = await db.SaveChangesAsync();
		if (affected == 1)
		{
			if (employeeCache is null) return null;
			return employeeCache.TryRemove(id, out c);
		}
		else
		{
			return null;
		}
	}
}

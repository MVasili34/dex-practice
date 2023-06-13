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


	public Employee? AddEmployee(Employee c)
	{
		EntityEntry<Employee> added = db.Employees.Add(c);
		int affected = db.SaveChanges();
		if (affected == 1)
		{
			if (employeeCache is null)
				return c;
			//нового работника в кэш, иначе вызываем UpdateCache
			return employeeCache.AddOrUpdate(c.EmployeeId, c, UpdateCache);
		}
		return null;
	}

	public IEnumerable<Employee> RetrieveAll() => employeeCache is null ?
		Enumerable.Empty<Employee>() : employeeCache.Values;

	public IEnumerable<Employee> GetFiltered(DateOnly startDate, DateOnly endDate) => employeeCache is null ?
		Enumerable.Empty<Employee>() : employeeCache.Values.Where(p =>
		p.DateOfBirth > startDate && p.DateOfBirth < endDate).OrderBy(p =>
		p.DateOfBirth);


	public Employee? RetrieveEmployeeById(Guid id)
	{
		if (employeeCache is null) return null!;
		employeeCache.TryGetValue(id, out Employee? employee);
		return employee;
	}

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

	public Employee? EditEmployee(Guid id, Employee c)
	{
		//обновляем в базе
		db.Entry(c).State = EntityState.Modified;
		int affected = db.SaveChanges();
		if (affected == 1)
		{
			return UpdateCache(id, c);
		}
		return null;
	}

	public bool? DeleteEmoployee(Guid id)
	{
		Employee? c = db.Employees.Find(id);
		if (c is null) return null;
		db.Employees.Remove(c);
		int affected = db.SaveChanges();
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

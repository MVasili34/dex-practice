using Models;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class EmployeeService
	{
		private List<Employee>? employeeStorage;
		public EmployeeService() 
		{
			employeeStorage = new();
		}
		public void AddEmployee(Employee? employee)
		{
			try
			{
				if (employee is not null)
				{
					if ((employee.DateOfBirth.DayOfYear <= DateTime.Now.DayOfYear ? DateTime.Now.Year - employee.DateOfBirth.Year :
						DateTime.Now.Year - employee.DateOfBirth.Year - 1) < 18)
					{
						throw new Below18Exception("Сотружник не может быть младше 18 лет");
					}
					else if (String.IsNullOrEmpty(employee.Contract))
					{
						throw new NullContractException("У сотрудника нет контракта");
					}
					else
					{
						employeeStorage?.Add(employee);
					}
				}
			}
			catch (NullContractException ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (Below18Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void EditEmployee(int empNumber, Employee employee)
		{
			try
			{
				if (employeeStorage is not null)
				{
					if (String.IsNullOrEmpty(employee.Contract) || String.IsNullOrEmpty(employee.Position) ||
						employee.Salary < 0)
					{
						throw new IncorrectEmployeeException("Некорректные данные сотрудника!");
					}
					else
					{
						employeeStorage[empNumber] = employee;
					}
				}
			}
			catch (IncorrectEmployeeException ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (IndexOutOfRangeException ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void PrintOut()
		{
			Console.WriteLine($"Список сотрудников:");
			for (int i = 0; i < employeeStorage?.Count; i++)
			{
				Console.WriteLine($"{i + 1}) FName: {employeeStorage[i].FirstName}, LName: {employeeStorage[i].LastName};" +
					$" ДР: {employeeStorage[i].DateOfBirth}; Должность: {employeeStorage[i].Position}; Контракт: {employeeStorage[i].Contract}");
			}
		}
	}
}

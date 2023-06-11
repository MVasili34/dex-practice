using Models;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Storage
{
	public class EmployeeStorage
	{
		public readonly List<Employee>? employees;

		public EmployeeStorage() => employees = TestDataGenerator.GenerateEmployees(10).ToList();

		public void AddEmployee(Employee? employee)
		{
			try
			{
				if (employee is not null)
				{
					if (DateTime.Now.Year - employee.DateOfBirth.Year < 18)
					{
						throw new Below18Exception("Сотружник не может быть младше 18 лет");
					}
					else if (String.IsNullOrEmpty(employee.Passport))
					{
						throw new NullContractException("У сотрудника нет контракта");
					}
					else
					{
						employees?.Add(employee);
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
	}
}

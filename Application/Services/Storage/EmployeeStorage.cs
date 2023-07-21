using Models;
using Services.Exceptions;

namespace Services.Storage;

public class EmployeeStorage
{
	public List<Employee>? Employees { get; }

	public EmployeeStorage()
	{
		Employees = TestDataGenerator.GenerateEmployees(10).ToList();
	}

	public void AddEmployee(Employee employee)
	{
		if (employee is not null)
		{
			if (DateTime.Now.Year - employee.DateOfBirth.Year < 18)
			{
				throw new Below18Exception("Сотружник не может быть младше 18 лет");
			}
			else if (string.IsNullOrEmpty(employee.Passport))
			{
				throw new NullContractException("У сотрудника нет контракта");
			}
			Employees?.Add(employee);
		}
	}
}

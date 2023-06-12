using Models;
using Services.Exceptions;
using Services.Storage;

namespace Services
{
	public class EmployeeService : IEmployeeStorage
	{
		public List<Employee> Data { get; }
		public EmployeeService() => Data = new();
		public EmployeeService(List<Employee>? employees) => Data = employees is not null ? employees : new();
		public IEnumerable<Employee> FilterMethod(string fName, string lName, string phone, string passport,
		  DateOnly sDate, DateOnly eDate) => Data?.Where(p => 
			p.FirstName.Contains(fName, StringComparison.OrdinalIgnoreCase) &&
		    p.LastName.Contains(lName, StringComparison.OrdinalIgnoreCase) &&
		    p.Phone.Contains(phone, StringComparison.OrdinalIgnoreCase) &&
		    p.Passport.Contains(passport, StringComparison.OrdinalIgnoreCase) &&
		    (p.DateOfBirth >= sDate && p.DateOfBirth <= eDate))!;
		public IEnumerable<Employee> GetOldestEmployees() => Data?.Where(p => p.DateOfBirth == Data.Min(t => t.DateOfBirth))!;

		public IEnumerable<Employee> GetYoungestEmployees() => Data?.Where(p => p.DateOfBirth == Data.Max(t => t.DateOfBirth))!;

		public int GetAvarageAge() => DateTime.Now.DayOfYear < Data?.Average(p => p.DateOfBirth.DayOfYear) ?
			DateTime.Now.Year - (int)Data.Average(p => p.DateOfBirth.Year) :
			DateTime.Now.Year - (int)Data?.Average(p => p.DateOfBirth.Year)! - 1;

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
						Data?.Add(employee);
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

		public void UpdateEmployee(int empNumber, Employee employee)
		{
			try
			{
				if (Data is not null)
				{
					if (String.IsNullOrEmpty(employee.Contract) || String.IsNullOrEmpty(employee.Position) ||
						employee.Salary < 0)
					{
						throw new IncorrectEmployeeException("Некорректные данные сотрудника!");
					}
					else
					{
						Data[empNumber] = employee;
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

		public void DeleteEmployee(Employee? employee)
		{
			try
			{
				if (employee is not null)
				{
					if (!Data.Remove(employee))
						throw new FailedToRemoveException("Невозможнло удалить сотрудника!");
				}
				else
					throw new FailedToRemoveException("Невозможнло удалить сотрудника!");
			}
			catch (FailedToRemoveException ex)
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
			for (int i = 0; i < Data?.Count; i++)
			{
				Console.WriteLine($"{i + 1}) FName: {Data[i].FirstName}, LName: {Data[i].LastName};" +
					$" ДР: {Data[i].DateOfBirth}; Пасспорт: {Data[i].Passport}; " +
					$"Должность: {Data[i].Position}; Контракт: {Data[i].Contract}");
			}
		}
		public void PrintOut(List<Employee>? employees)
		{
			if (employees is not null)
			{
				Console.WriteLine($"Список сотрудников:");
				for (int i = 0; i < employees?.Count; i++)
				{
					Console.WriteLine($"{i + 1}) FName: {employees?[i].FirstName}, LName: {employees?[i].LastName};" +
						$" ДР: {employees?[i].DateOfBirth}; Должность: {employees?[i].Position}; Контракт: {employees?[i].Contract}");
				}
			}
		}
	}
}

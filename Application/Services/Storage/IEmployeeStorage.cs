using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Storage
{
	public interface IEmployeeStorage
	{
		List<Employee> Data { get; }
		void AddEmployee(Employee employee);
		void UpdateEmployee(int empNumber, Employee employee);
		void DeleteEmployee(Employee employee);
	}
}

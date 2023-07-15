using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModels;

namespace ServicesDb;
public interface IEmployeeService
{
	Task<Employee?> AddEmployeeAsync(Employee employee);
	Task<IEnumerable<Employee>> RetrieveAllAsync();
	Task<Employee?> RetrieveEmployeeAsync(Guid id);
	Task<Employee?> UpdateEmployeeAsync(Guid id, Employee employee);
	Task<bool?> DeleteEmployeeAsync(Guid id);
}

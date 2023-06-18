using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModels;

namespace ServicesDb;
public interface IEmployeeService
{
	Task<Employee?> AddEmployee(Employee c);
	Task<IEnumerable<Employee>> RetrieveAll();
	Task<Employee?> RetrieveEmployeeById(Guid id);
	Task<Employee?> EditEmployee(Guid id, Employee c);
	Task<bool?> DeleteEmoployee(Guid id);
}

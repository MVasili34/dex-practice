using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModels;

namespace ServicesDb;
public interface IEmployeeService
{
	Employee? AddEmployee(Employee c);
	IEnumerable<Employee> RetrieveAll();
	Employee? RetrieveEmployeeById(Guid id);
	Employee? EditEmployee(Guid id, Employee c);
	bool? DeleteEmoployee(Guid id);
}

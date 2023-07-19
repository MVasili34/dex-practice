using Models;

namespace Services.Storage;

public interface IEmployeeStorage
{
	List<Employee> Data { get; }
	void AddEmployee(Employee employee);
	void UpdateEmployee(int employeeId, Employee employee);
	void DeleteEmployee(Employee employee);
}

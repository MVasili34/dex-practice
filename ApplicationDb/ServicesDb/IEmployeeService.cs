using EntityModels;

namespace ServicesDb;
public interface IEmployeeService
{
	Task<Employee?> AddEmployeeAsync(Employee employee);
	Task<IEnumerable<Employee>> RetrieveAllAsync(int? page);
	Task<Employee?> RetrieveEmployeeAsync(Guid id);
	Task<Employee?> UpdateEmployeeAsync(Guid id, Employee employee);
	Task<bool?> DeleteEmployeeAsync(Guid id);
}

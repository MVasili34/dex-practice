using Models;

namespace Services.Storage;

public interface IEmployeeStorage
{
	List<Employee> Data { get; }
    IEnumerable<Employee> FilterMethod(string fName, string lName, string phone, 
        string passport, DateOnly sDate, DateOnly eDate);
    IEnumerable<Employee> GetOldestEmployees();
    IEnumerable<Employee> GetYoungestEmployees();
    int GetAvarageAge();
    void AddEmployee(Employee employee);
	void UpdateEmployee(int employeeId, Employee employee);
	void DeleteEmployee(Employee employee);
}

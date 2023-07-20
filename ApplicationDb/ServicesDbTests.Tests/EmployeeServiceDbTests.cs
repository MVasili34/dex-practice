using EntityModels;
using Microsoft.Extensions.DependencyInjection;
using ServicesDb;

namespace ServicesDbTests.Tests;

public class EmployeeServiceDbTests
{
	private readonly IEmployeeService _employeeService;

	public EmployeeServiceDbTests()
	{
        _employeeService = DependencyContainer.Configure()
            .GetService<IEmployeeService>()!;
    }

    [Fact]
	public async void AddingEmployeeTest()
	{	
		Employee employee = DataGenerator.GenerateEmployee();

		Employee? added = await _employeeService.AddEmployeeAsync(employee);

        Assert.Equal(employee, added);
	}

	[Fact]
	public async void EditEmployeeByIdTest()
	{
        IEnumerable<Employee> employees = await _employeeService.RetrieveAllAsync(1);
        Employee employee = employees.First();

        employee.FirstName = DateTime.Now.ToString("T");
		Employee? expected = await _employeeService.UpdateEmployeeAsync(employee.EmployeeId, employee);

        Assert.Equal(employee, expected);
	}

	[Fact]
	public async void DeleteEmployeeTest()
	{
        Employee employee = DataGenerator.GenerateEmployee();

        await _employeeService.AddEmployeeAsync(employee);
		bool? status = await _employeeService.DeleteEmployeeAsync(employee.EmployeeId);

        Assert.True(status);
	}
	
}

using EntityModels;
using Microsoft.Extensions.DependencyInjection;
using ServicesDb;

namespace ServicesDbTests.Tests;

public class EmployeeServiceDbTests
{
	private readonly IEmployeeService _service;

	public EmployeeServiceDbTests()
	{
        _service = DependencyContainer.Configure()
            .GetService<IEmployeeService>()!;
    }

    [Fact]
	public async void AddingEmployeeTest()
	{	
		Employee employee = DataGenerator.GenerateEmployee();

		Employee? added = await _service.AddEmployeeAsync(employee);

        Assert.Equal(employee, added);
	}

	[Fact]
	public async void EditEmployeeByIdTest()
	{
        IEnumerable<Employee> employees = await _service.RetrieveAllAsync(1);
        Employee employee = employees.First();

        employee.FirstName = DateTime.Now.ToString("T");
		Employee? expected = await _service.UpdateEmployeeAsync(employee.EmployeeId, employee);

        Assert.Equal(employee, expected);
	}

	[Fact]
	public async void DeleteEmployeeTest()
	{
        Employee employee = DataGenerator.GenerateEmployee();

        await _service.AddEmployeeAsync(employee);
		bool? status = await _service.DeleteEmployeeAsync(employee.EmployeeId);

        Assert.True(status);
	}
	
}

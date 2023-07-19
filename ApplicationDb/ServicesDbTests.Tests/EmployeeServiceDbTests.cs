using EntityModels;
using Microsoft.Extensions.DependencyInjection;
using ServicesDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesDbTests.Tests;

public class EmployeeServiceDbTests
{
	private readonly IEmployeeService _service;

	public EmployeeServiceDbTests()
	{
        this._service = DependencyContainer.Configure()
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
		Employee employee = _service.RetrieveAllAsync().Result.First();

		employee.FirstName = "TEST";
		Employee? expected = await _service.UpdateEmployeeAsync(employee.EmployeeId, employee);

        Assert.Equal(employee, expected);
	}

	[Fact]
	public async void DeleteEmployeeTest()
	{
        var employee = DataGenerator.GenerateEmployee();

        await _service.AddEmployeeAsync(employee);
		bool? status = await _service.DeleteEmployeeAsync(employee.EmployeeId);

        Assert.True(status);
	}
	
}

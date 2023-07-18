using EntityModels;
using ServicesDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesDbTests.Tests;

public class EmployeeServiceDbTests
{
    private EmployeeService service = new(new BankingServiceContext());

    [Fact]
	public async void AddingEmployeeTest()
	{	
		var employee = DataGenerator.GenerateEmployee();

		await service.AddEmployeeAsync(employee);

		Assert.Equal(employee, service.RetrieveEmployeeAsync(employee.EmployeeId).Result);
	}

	[Fact]
	public void EditEmployeeByIdTest()
	{
		Employee client = service.RetrieveAllAsync().Result.First();
		client.FirstName = "TEST";

		Assert.Equal(client, service.UpdateEmployeeAsync(client.EmployeeId, client).Result);
	}

	[Fact]
	public async void DeleteEmployeeTest()
	{
        var employee = DataGenerator.GenerateEmployee();

        await service.AddEmployeeAsync(employee);

        Assert.True(service.DeleteEmployeeAsync(employee.EmployeeId).Result);
	}
	
}

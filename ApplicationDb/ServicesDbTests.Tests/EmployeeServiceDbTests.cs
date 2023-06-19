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
	[Fact]
	public async void AddingEmployeeTest()
	{
		EmployeeService db = new(new BankingServiceContext());
		var someEmployee = DataGenerator.GenerateEmployee();

		await db.AddEmployeeAsync(someEmployee);

		Assert.Contains(someEmployee, db.RetrieveAllAsync().Result);
	}
	[Fact]
	public void GetEmployeeByIdTest()
	{
		EmployeeService db = new(new BankingServiceContext());
		Assert.Equal(db.RetrieveAllAsync().Result.First().EmployeeId, 
			db.RetrieveEmployeeAsync(db.RetrieveAllAsync().Result.First().EmployeeId).Result?.EmployeeId);
	}

	[Fact]
	public void EditEmployeeByIdTest()
	{
		EmployeeService db = new(new BankingServiceContext());
		Employee client = db.RetrieveEmployeeAsync(db.RetrieveAllAsync().Result.First().EmployeeId).Result!;
		client.FirstName = "TEST";
		Assert.Equal(client, db.UpdateEmployeeAsync(db.RetrieveAllAsync().Result.First().EmployeeId, client).Result);
	}

	[Fact]
	public void DeleteEmployeeTest()
	{
		EmployeeService db = new(new BankingServiceContext());
		Assert.True(db.DeleteEmployeeAsync(db.RetrieveAllAsync().Result.Reverse().First().EmployeeId).Result);
	}
	
}

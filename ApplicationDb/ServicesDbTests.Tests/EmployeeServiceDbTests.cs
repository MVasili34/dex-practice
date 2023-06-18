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

		await db.AddEmployee(someEmployee);

		Assert.Contains(someEmployee, db.RetrieveAll().Result);
	}
	[Fact]
	public void GetEmployeeByIdTest()
	{
		EmployeeService db = new(new BankingServiceContext());
		Assert.Equal(db.RetrieveAll().Result.First().EmployeeId, 
			db.RetrieveEmployeeById(db.RetrieveAll().Result.First().EmployeeId).Result?.EmployeeId);
	}

	[Fact]
	public void EditEmployeeByIdTest()
	{
		EmployeeService db = new(new BankingServiceContext());
		Employee client = db.RetrieveEmployeeById(db.RetrieveAll().Result.First().EmployeeId).Result!;
		client.FirstName = "TEST";
		Assert.Equal(client, db.EditEmployee(db.RetrieveAll().Result.First().EmployeeId, client).Result);
	}

	[Fact]
	public void DeleteEmployeeTest()
	{
		EmployeeService db = new(new BankingServiceContext());
		Assert.True(db.DeleteEmoployee(db.RetrieveAll().Result.Reverse().First().EmployeeId).Result);
	}
	
}

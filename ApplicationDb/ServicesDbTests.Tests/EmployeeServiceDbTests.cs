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
	public void AddingEmployeeTest()
	{
		EmployeeService db = new(new BankingServiceContext());
		var someEmployee = DataGenerator.GenerateEmployee();

		db.AddEmployee(someEmployee);

		Assert.Contains(someEmployee, db.RetrieveAll());
	}
	[Fact]
	public void GetEmployeeByIdTest()
	{
		EmployeeService db = new(new BankingServiceContext());
		Assert.Equal(db.RetrieveAll().First().EmployeeId, 
			db.RetrieveEmployeeById(db.RetrieveAll().First().EmployeeId)?.EmployeeId);
	}

	[Fact]
	public void EditEmployeeByIdTest()
	{
		EmployeeService db = new(new BankingServiceContext());
		Employee client = db.RetrieveEmployeeById(db.RetrieveAll().First().EmployeeId)!;
		client.FirstName = "TEST";
		Assert.Equal(client, db.EditEmployee(db.RetrieveAll().First().EmployeeId, client));
	}

	[Fact]
	public void DeleteEmployeeTest()
	{
		EmployeeService db = new(new BankingServiceContext());
		Assert.True(db.DeleteEmoployee(db.RetrieveAll().Reverse().First().EmployeeId));
	}
	
}

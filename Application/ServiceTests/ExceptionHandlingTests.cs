﻿using Models;
using Services;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Services.TestDataGenerator;

namespace ServiceTests.Tests;

public class ExceptionHandlingTests
{
	[Fact]
	public static void ClientBelow18Test()
	{
		ClientService clientService = new();
		Client client = new("Имя1", "Имя2", new(2010, 02, 21), "22344114", "", "NONE", "");
		Assert.Throws<Below18Exception>(() => clientService.Add(client));
	}
	[Fact]
	public static void ClientNoPassportTest()
	{
		ClientService clientService = new();
		Client client = new("Имя1", "Имя2", new(2001, 02, 21), "22344114", "", "NONE", "");
		Assert.Throws<PassportNullException>(() => clientService.Add(client));
	}
	[Fact]
	public static void ClientDefaultAccountExistsTest()
	{
		ClientService clientService = new();
		Client client = GenerateClints(1).First();
		clientService.Add(client);
		Assert.Single(clientService.Data[client]);
	}
	[Fact]
	public static void ClientAddingAccountTest()
	{
		ClientService clientService = new();
		Client client = GenerateClints(1).First();
		clientService.Add(client);
		clientService.AddAccount(client, new(new Currency("BS212", "Usd."), 10));
		Assert.Equal(2, clientService.Data[client].Count());
	}
	[Fact]
	public static void ClientAddingAccountExceptionTest()
	{
		ClientService clientService = new();
		Client client = GenerateClints(1).First();
		clientService.Add(client);
		Assert.Throws<IncorrectAccountException>(() => clientService.AddAccount(client,
			new(new Currency("BS212", ""), 10)));
	}
	[Fact]
	public static void ClientRemoveExceptionTest()
	{
		ClientService clientService = new();
		Client client = GenerateClints(1).First();
		Assert.Throws<FailedToRemoveException>(() => clientService.Delete(client));
	}
	[Fact]
	public static void EmployeeBelow18Test()
	{
		EmployeeService employeeService = new();
		Employee employee = new("Имя1", "Имя2", new(2010, 03, 21), "22344114", "AB2331", "Уборщик", 10000, "");
		Assert.Throws<Below18Exception>(() => employeeService.AddEmployee(employee));
	}
	[Fact]
	public static void EmployeeNoPassportTest()
	{
		EmployeeService employeeService = new();
		Employee client = new("Имя1", "Имя2", new(2001, 03, 21), "22344114", "AB432", "Уборщик", 10000, "");
		Assert.Throws<NullContractException>(() => employeeService.AddEmployee(client));
	}
	[Fact]
	public static void EmployeeRemoveExceptionTest()
	{
		EmployeeService employeeService = new();
		Employee client = GenerateEmployees(1).First();
		Assert.Throws<FailedToRemoveException>(() => employeeService.DeleteEmployee(client));
	}
	[Fact]
	public static void EmployeeEditExceptionTest()
	{
		EmployeeService employeeService = new();
		Employee employee = GenerateEmployees(1).First();
		employee.Contract = "Something";
		employeeService.AddEmployee(employee);
		employee = employeeService.Data.First();
		employee.Contract = "";
		Assert.Throws<IncorrectEmployeeException>(() => employeeService.UpdateEmployee(0, employee));
	}
}
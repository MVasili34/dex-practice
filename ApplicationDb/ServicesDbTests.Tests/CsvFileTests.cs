﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExportTool;
using EntityModels;
using ServicesDb;
using Bogus;

namespace ServicesDbTests.Tests
{
	public class CsvFileTests
	{
		[Fact]
		public void ExportImportClientsTest()
		{
			ClientService db = new(new BankingServiceContext());
			ExportService<Client> exportService = new(Environment.CurrentDirectory, "expimpclient.csv");
			var collection = db.RetrieveAll().Result.ToList();
			exportService.ExportPersons(collection);
			Assert.Equal(collection, exportService.ImportPersons()!);
		}

		[Fact]
		public void ExportImportEmployeeTest()
		{
			EmployeeService db = new(new BankingServiceContext());
			ExportService<Employee> exportService = new(Environment.CurrentDirectory, "expimpemployee.csv");
			var collection = db.RetrieveAll().Result.ToList();
			exportService.ExportPersons(collection);
			Assert.Equal(collection, exportService.ImportPersons()!);
		}
	}
}

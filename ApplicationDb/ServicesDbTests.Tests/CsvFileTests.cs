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
			ExportService<Client> exportService = new(Environment.CurrentDirectory, "exportimport.csv");
			var collection = db.RetrieveAll().ToList();
			exportService.ExportClients(collection);
			Assert.Equal(collection, exportService.ImportClients()!);
		}
	}
}
